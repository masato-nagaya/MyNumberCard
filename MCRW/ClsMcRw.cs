using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using RingBuff;
using ComText;

namespace MCRW
{    
    public class MCRWInterface
    {
        private int TmrInterval = 10;
        private Timer MyTimerCom;
        private Timer MyTimerEvent;

        private int TimeOutValue = 5;   //コマンドのタイムアウト　5秒

        public event EvtHandler MagRecvDataEvent;       //磁気カードデータ受信イベント
        public event EvtHandler MagRecvStatus;          //ステータス受信イベント
        public event EvtHandler MagRecvVersionEvent;    //バージョン受信イベント

        public event EvtHandler MagComSendEvent;        //COM送信イベント
        public event EvtHandler MagComRecvEvent;        //COM受信イベント
                
        public enum ComResult
        {
            MAG_NOER = 0    // 正常終了（MagRecvData を除く）    
        , MAG_RSNO = -1     // RS-232C が初期化されていない、または RS-232C の初期化に失敗した 
        , MAG_RSOV = -2     // 受信バッファオーバーフロー        
        , MAG_NRDY = -3     // 送受信動作タイムアウト            
        , MAG_NOEX = -4     // 拡張 RS-232C ボードが未実装       
        , MAG_NOBF = -5     // 受信バッファが確保できない        
        , MAG_TOUT = -6     // コマンド応答タイムアウト          
        , MAG_RERR = -7     // 受信エラー                        
        , MAG_KEYA = -8     // [ESC] キー入力によりアボート      
        };

        public enum ComParity
        {
            MAG_NONE = 0    // パリティなし                      
        , MAG_EVEN = 1      // パリティ偶数                      
        , MAG_ODD = 2       // パリティ奇数                      
        };

        public enum JisCode : int
        {
            Jis8 = 8    /* JIS-8 モード     */
        , Jis7 = 7      /* JIS-7 モード     */
        , HexMode = 'H' /* Hex モード     */
        };

        public enum SpaceMode : int
        {
            SpcA0 = 0   // カ(A0)ナ 
        , Spc20 = 1     // カ(SI)(20)(SO)ナ 
        , SpcKeep = 2   // 変更しない 
        };

        public enum JisLrcCheck : int
        {
            ChkOff = 0    /* チェックしない */
        , ChkOn = 1     /* チェックする */
        , ChkKeep = 2   /* 変更しない */
        };

        public enum ReadMode : int
        {
            ReadCont = 0  /* 連続読み出しモード */
        , ReadOne = 1   /* １回のみ読み出しモード（カードは内部停止）*/
        };

        public enum LampCtrl : int
        {
            LmpOff = 0  /* ランプ消灯     */
        , LmpOn = 1     /* ランプ点灯     */
        , LmpKeep = 2   /* ランプ現状維持 */
        };

        public enum WriteOut : int
        {
            WOut = 0    //書き込んだ後、磁気カードを排出する。
        , WStop = 1     //書き込んだ後、磁気カードを排出しない。
        };


        private enum CodeChar
        {
            Nul = 0x00  //制御文字 NULL
        , Stx = 0x02    //制御文字 STX
        , Etx = 0x03    //制御文字 ETX
        , ENQ = 0x05    //制御文字 ENQ
        , Ack = 0x06    //制御文字 ACK
        , Nak = 0x15    //制御文字 NAK	
        };

        private enum ComPhase
        {
            NoAct   // 動作なし
        , WaitAck   // Ack待ち
        , RecNak    // Nak受信
        , WaitStx   // Stx待ち
        , WaitData  // Data待ち
        , WaitEtx   // Etx待ち
        , WaitLrc   // LRC待ち
        };

        private enum RecMode
        {
            NoAct    //モードなし
         , Status   //ステータス受信モード
         , ReadData //読取りデータ受信モード
        }

        private static int BuffSize = 512;

        private static SerialPort ComPort;

        private byte[] SndBfr = new byte[BuffSize];  //コマンドの送信バッファ
        private byte[] RecBfr = new byte[BuffSize];  //コマンドの受信バッファ

        private byte[] RecBfrVersion = new byte[BuffSize];  //バージョン受信バッファ
        private bool FlgRecVerion = false;

        private byte[] RecBfrData = new byte[BuffSize];     //磁気データ受信バッファ
        private bool FlgRecData = false;

        private byte[] RecBfrStatus = new byte[BuffSize];   //ステータス受信バッファ
        private int RecBfrStatusCosde = 0;
        private bool FlgRecStatus = false;

        private byte[] BfrComStrings = new byte[BuffSize]; //通信イベントバッファ
        private bool FlgComStrings = false;


        byte[] CmdBfr = new byte[BuffSize];
        int CmdLen = 0;

        int RetryCnt = 0;

        int RetryMax = 3;

        private static int RecBfrPtr;

        private static ClsRingBuff ReadBuff;    //受信バッファの実体化

        private static ClsComText CommunicationLog;

        private static ComResult ComPortHdle;

        private static ComPhase ComSeq;     //進行中の通信シーケンス

        private static byte RecLrc;         //LRCの計算値

        private static RecMode ComMode;     //データ受信のモード(基本はN/A)

        private static bool VerMode;        //バージョン受信モード

        private static Stopwatch stopWatch = new Stopwatch();

        System.Diagnostics.FileVersionInfo pMyVer;

        public MCRWInterface()
        {
            ComPort = new SerialPort(); //COMポートのインスタンス生成

            //シリアルポートの受信イベントのハンドラを設定
            ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            //タイマのハンドラを設定

            MyTimerCom = new System.Threading.Timer(new TimerCallback(MyTimerCom_Tick));
            MyTimerEvent = new System.Threading.Timer(new TimerCallback(MyTimerEvent_Tick));

            ComSeq = ComPhase.NoAct;
            ReadBuff = new ClsRingBuff();

            CommunicationLog = new ClsComText();

            ComPortHdle = ComResult.MAG_RSNO;

            ComMode = RecMode.NoAct;

            VerMode = false;

            pMyVer = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

        }

        public string LibVersion
        {
            get
            {
                return pMyVer.FileVersion;
            }
        }

        /****************************************************************************/
        /*	名称	:	通信ポートの初期化											*/
        /*	概要	:	ＲＳ－２３２Ｃポートを初期化します。						*/
        /*	注意	:	ポート名を番号と文字列でオーバーロードする					*/
        /*				実体PorcMagOpenを呼び出す									*/
        /****************************************************************************/
        public ComResult MagOpen
        (
          string PortName       //COMポートの名称(COM1,COM2,COM3～)
        , int Baud              //ボーレート
        , ComParity ParityType  //パリティ　0:None,1:Even,2:Odd
        )
        {
            return PorcMagOpen(PortName, Baud, ParityType);
        }
        public ComResult MagOpen
        (
          int PortNum           //COMポートの番号(1,2,3～)
        , int Baud              //ボーレート
        , ComParity ParityType  //パリティ　0:None,1:Even,2:Odd
        )
        {
            string PortName = "COM" + PortNum.ToString();  //ポート番号から名称を作成

            return PorcMagOpen(PortName, Baud, ParityType);
        }
        private ComResult PorcMagOpen
        (
        String PortName         //COMポートの番号(1,2,3～)
        , int Baud              //ボーレート
        , ComParity ParityType  //パリティ　0:None,1:Even,2:Odd
        )
        {
            switch (Baud)
            {
                case 1200:
                case 2400:
                case 4800:
                case 9600:
                case 19200:
                    ComPort.BaudRate = Baud;
                    break;
                default:
                    ComPort.BaudRate = 9600;
                    break;
            }

            ComPort.BaudRate = Baud;

            switch (ParityType)
            {
                case ComParity.MAG_NONE: ComPort.Parity = Parity.None; break;
                case ComParity.MAG_ODD: ComPort.Parity = Parity.Odd; break;
                case ComParity.MAG_EVEN: ComPort.Parity = Parity.Even; break;
            }

            ComPort.StopBits = StopBits.One;    //Stop Bitは1固定
            ComPort.DataBits = 8;               //データビット数は8固定

            //COMポートをオープンする。
            try
            {
                ComPort.PortName = PortName;        //ポート名称の設定
                ComPort.Open();
                ComPortHdle = ComResult.MAG_NOER;   //Openに成功したのでNOERに設定
                MyTimerCom.Change(TmrInterval, TmrInterval);
                MyTimerEvent.Change(TmrInterval, TmrInterval);

            }
            catch
            {
                ComPort.Close();
                ComPortHdle = ComResult.MAG_RSNO;   //Openに失敗したのでRSNOに設定
            }
            return ComPortHdle;
        }

        /****************************************************************************/
        /*	名称	:	通信ポートの使用終了										*/
        /*	概要	:	通信ポートの使用終了処理を行います。						*/
        /*	注意	:																*/
        /****************************************************************************/
        public void MagClose()
        {
            if (ComPortHdle == ComResult.MAG_NOER)
            {
                try
                {
                    ComPort.Close();                    //Comポートをクローズ
                }
                catch { };

                ComPortHdle = ComResult.MAG_RSNO;   //ComポートをCloseしたので未使用にする
                ComSeq = ComPhase.NoAct;

                MyTimerCom.Change(-1, TmrInterval);    //タイマの停止
                MyTimerEvent.Change(-1, TmrInterval);    //タイマの停止
            }
        }

        /****************************************************************************/
        /*	名称	:	バージョン問い合わせ										*/
        /*	概要	:	ＰＤＣ－２１０のバージョンを問い合わせます。				*/
        /*	注意	:																*/
        /****************************************************************************/
        public ComResult MagVersion()
        {
            ComResult Res;

            SndBfr[0] = (byte)'V';    //バージョン取得コマンド
            SndBfr[1] = (byte)'\0';

            VerMode = true; //バージョン取得モード

            Res = SendCmd(ref SndBfr);

            return Res;
        }

        /****************************************************************************/
        /*	名称	:	データ転送モード指定										*/
        /*	概要	:	書き込みコマンド、読み取りデータの転送モードを指定します。	*/
        /*	注意	:																*/
        /****************************************************************************/
        public ComResult MagMode
        (
        JisCode Mode        // データの転送モード
        )
        {
            ComResult Res;

            SndBfr[0] = (byte)'M';

            if (Mode == JisCode.Jis7)
            {
                SndBfr[1] = (byte)'7';                // JIS 7 bit mode
            }
            else if (Mode == JisCode.Jis8)
            {
                SndBfr[1] = (byte)'8';                // JIS 8 bit mode
            }
            else if (Mode == JisCode.HexMode)
            {
                SndBfr[1] = (byte)'H';                // Hex mode
            }
            SndBfr[2] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード読み取り												*/
        /*	概要	:	磁気カード読み取りコマンドを送信します。						*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagRead
        (
        ReadMode Mode    //読取モード
        )
        {
            ComResult Res;

            if (Mode == ReadMode.ReadCont)
            {
                SndBfr[0] = (byte)'R';
            }
            else
            {
                SndBfr[0] = (byte)'O';
            }

            SndBfr[1] = (byte)'\0';

            VerMode = false;

            Res = SendCmd(ref SndBfr);

            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カードテスト												*/
        /*	概要	:	磁気カードのテストを行います。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagCardTest()
        {
            ComResult Res;

            SndBfr[0] = (byte)'T';
            SndBfr[1] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード書き込み												*/
        /*	概要	:	磁気カードに書き込むデータを送信します。						*/
        /*	注意	:	SO/SI 以外のｺﾝﾄﾛｰﾙｺｰﾄﾞを書く場合は16進ﾓｰﾄﾞを利用して下さい。	*/
        /********************************************************************************/
        public ComResult MagWrite
        (
          WriteOut Mode  //書込みのモード
        , byte[] Str    //書込みデータ
        )
        {
            ComResult Res;

            if (Mode == WriteOut.WOut)
            {
                SndBfr[0] = (byte)'w';                // 自動排出
            }
            else
            {
                SndBfr[0] = (byte)'W';                // 内部停止
            }

            StrCpy(ref SndBfr, 1, Str, 0);

            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	直前に送ったコマンドのキャンセル								*/
        /*	概要	:	直前に送ったコマンドをキャンセルする。							*/
        /*	注意	:	処理済みのコマンドはキャンセルできない。						*/
        /********************************************************************************/
        public ComResult MagCancel()
        {
            ComResult Res;

            SndBfr[0] = (byte)'C';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード挿入													*/
        /*	概要	:	磁気カード挿入コマンド送信。。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagCardIn()
        {
            ComResult Res;

            SndBfr[0] = (byte)'I';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード排出													*/
        /*	概要	:	磁気カードを手前に排出する。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagCardOut()
        {
            ComResult Res;

            SndBfr[0] = (byte)'E';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード後部排出												*/
        /*	概要	:	磁気カードを後部より排出する。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagBackOut()
        {
            ComResult Res;

            SndBfr[0] = (byte)'B';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	磁気カード強制排出												*/
        /*	概要	:	磁気カードを強制的に排出する。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagForcedOut()
        {
            ComResult Res;

            SndBfr[0] = (byte)'F';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	ロータリースイッチの設定値を読み出す							*/
        /*	概要	:	設定値を読み出してステータスを返送する。						*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagLBAuto()
        {
            ComResult Res;

            SndBfr[0] = (byte)'A';
            SndBfr[1] = (byte)'\0';
            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	ブザー制御														*/
        /*	概要	:	ＰＤＣ－２１０本体内臓のブザーを制御します。					*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagBeep
        (
          int onTime    //ブザーのON時間
        , int ofTime    //ブザーのON時間
        , int Times     //鳴動回数
        )
        {
            ComResult Res;

            SndBfr[0] = (byte)'L';
            SndBfr[1] = (byte)'-';
            SndBfr[2] = (byte)'-';
            SndBfr[3] = (byte)'-';
            if ((0 <= onTime) && (onTime <= 9))
            {
                SndBfr[4] = (byte)(0x30 + onTime);  //0x30 →'0'
            }
            else
            {
                SndBfr[4] = (byte)'-';
            }

            if ((0 <= ofTime) && (ofTime <= 9))
            {
                SndBfr[5] = (byte)(0x30 + ofTime);//0x30 →'0'
            }
            else
            {
                SndBfr[5] = (byte)'-';
            }
            if ((0 <= Times) && (Times <= 9))
            {
                SndBfr[6] = (byte)(0x30 + Times);//0x30 →'0'
            }
            else
            {
                SndBfr[6] = (byte)'-';
            }
            SndBfr[7] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	ランプ制御														*/
        /*	概要	:	ＰＤＣ－２１０本体のランプをＯＮ／ＯＦＦ制御します。			*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagLamp
        (
          LampCtrl RDlamp   // 読み出しＬＡＭＰ
        , LampCtrl WRlamp   // 書き込みＬＡＭＰ
        , LampCtrl ERlamp   // エラーＬＡＭＰ
        , LampCtrl INSlamp   // 入口ＬＡＭＰ

        )
        {
            ComResult Res;

            SndBfr[0] = (byte)'L';
            switch (RDlamp)
            {                   // 読み出しＬＡＭＰ
                case LampCtrl.LmpOff: SndBfr[1] = (byte)'0'; break;
                case LampCtrl.LmpOn: SndBfr[1] = (byte)'1'; break;
                default: SndBfr[1] = (byte)'-'; break;
            }
            switch (WRlamp)
            {                   // 書き込みＬＡＭＰ
                case LampCtrl.LmpOff: SndBfr[2] = (byte)'0'; break;
                case LampCtrl.LmpOn: SndBfr[2] = (byte)'1'; break;
                default: SndBfr[2] = (byte)'-'; break;
            }
            switch (ERlamp)
            {                   // エラーＬＡＭＰ
                case LampCtrl.LmpOff: SndBfr[3] = (byte)'0'; break;
                case LampCtrl.LmpOn: SndBfr[3] = (byte)'1'; break;
                default: SndBfr[3] = (byte)'-'; break;
            }

            SndBfr[4] = (byte)'-';                    // ブザー指定
            SndBfr[5] = (byte)'-';
            SndBfr[6] = (byte)'-';

            switch (INSlamp)
            {                   // 読み出しＬＡＭＰ
                case LampCtrl.LmpOff: SndBfr[7] = (byte)'0'; break;
                case LampCtrl.LmpOn: SndBfr[7] = (byte)'1'; break;
                default: SndBfr[7] = (byte)'-'; break;
            }

            SndBfr[8] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;
        }

        /********************************************************************************/
        /*	名称	:	モーター制御     												*/
        /*	概要	:	ＰＤＣ－２１０本体のモーターを動作させます。     				*/
        /*          :   argParam,F:正転,R:逆転:,S:停止									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagMotDrive
        (
        byte argParam
        )
        {
            ComResult Res;

            SndBfr[0] = (byte)'Z';
            SndBfr[1] = (byte)'M';
            SndBfr[2] = argParam;
            SndBfr[3] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;

        }

        /********************************************************************************/
        /*	名称	:	モーター制御     												*/
        /*	概要	:	ＰＤＣ－２１０本体のモーターを動作させます。     				*/
        /*          :   argParam,F:正転,R:逆転:,S:停止									*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagContDrive
        (
         byte[] argStart    //開始データ
        , byte[] argTimes    //回数データ
        )
        {
            ComResult Res;

            SndBfr[0] = (byte)'Z';
            SndBfr[1] = (byte)'C';

            StrCpy(ref SndBfr, 2, argStart, 0);
            SndBfr[2 + argStart.Length - 1] = (byte)',';
            StrCpy(ref SndBfr, (2 + argStart.Length - 1) + 1, argTimes, 0);

            Res = SendCmd(ref SndBfr);
            return Res;

        }


        /********************************************************************************/
        /*	名称	:	モーター制御     												*/
        /*	概要	:	ＰＤＣ－２１０本体のモーターを動作させます。     				*/
        /*          :																	*/
        /*	注意	:																	*/
        /********************************************************************************/
        public ComResult MagPhotSens()
        {
            ComResult Res;

            SndBfr[0] = (byte)'Z';
            SndBfr[1] = (byte)'S';
            SndBfr[2] = (byte)'\0';

            Res = SendCmd(ref SndBfr);
            return Res;

        }
        /********************************************************************************/
        /*	名称	:	コマンドの送信処理												*/
        /*	概要	:	指定された文字列に STX,ETX,LRC を付加し送信します。				*/
        /*	注意	:																	*/
        /********************************************************************************/
        private ComResult SendCmd
            (
            ref byte[] argStr
            )
        {
            int Lrc;

            if (ComPortHdle != (int)ComResult.MAG_NOER)
            {
                return ComResult.MAG_RSNO;              // ポートがオープンがされていない。
            }

            RetryCnt = 0;

            if ((argStr[0] != (byte)CodeChar.Ack) && (argStr[0] != (byte)CodeChar.Nak))
            {

                CmdBfr[0] = (byte)CodeChar.Stx;

                Lrc = (byte)CodeChar.Etx;       // LRCの計算 CMDからETXまで
                for (CmdLen = 0; CmdLen < BuffSize && (argStr[CmdLen] != (byte)CodeChar.Nul); CmdLen++)
                {
                    if (argStr[CmdLen] == (byte)CodeChar.Etx)
                    {
                        break;
                    }
                    Lrc ^= (CmdBfr[CmdLen + 1] = argStr[CmdLen]);
                }
                CmdLen++;
                CmdBfr[CmdLen] = (byte)CodeChar.Etx;
                CmdLen++;
                CmdBfr[CmdLen] = (byte)(Lrc & 0xFF);

                try
                {
                    ComSeq = ComPhase.WaitAck;
                    ComPortWrite(CmdBfr, 0, (CmdLen + 1));  //コマンド送信
                }
                catch
                {
                    return ComResult.MAG_RSNO;
                }

            }
            else
            {
                //ACK返信
                try
                {
                    ComSeq = ComPhase.WaitStx;
                    ComPortWrite(argStr, 0, 1);
                    return ComResult.MAG_NOER;      //正常終了
                }
                catch
                {
                    return ComResult.MAG_RSNO;
                }
            }

            stopWatch.Stop();   //タイムアウトタイマ起動
            stopWatch.Reset();
            stopWatch.Start();

            while (ComSeq == ComPhase.WaitAck)
            {
                //Ackが返ってくるまで待つ
                //タイムアウト、リトライアウトになるとComSeqがWaitAckでなくなるので抜ける
            }

            return ComResult.MAG_NOER;      //正常終了
        }


        /********************************************************************************/
        /*	名称	:	文字列の複写													*/
        /*	概要	:	NUL で終わる文字列を複写します。								*/
        /*	注意	:																	*/
        /********************************************************************************/
        private void StrCpy
        (
          ref byte[] argDest    //コピー先
        , int DestOffset
        , byte[] argSrc         //コピー元
        , int SrcOffset
        )
        {
            for (int i = 0; i < (argSrc.Length - SrcOffset); i++)
            {
                argDest[DestOffset + i] = argSrc[SrcOffset + i];
                if (argSrc[i] == '\0')
                {
                    break;
                }
            }
        }


        /********************************************************************************/
        /*	名称	:	COMポ―ト出力													*/
        /*	概要	:	出力後送信イベントを発行する。									*/
        /*	注意	:																	*/
        /********************************************************************************/
        private void ComPortWrite
        (
         byte[] argAry
        , int Offset
        , int Count
        )
        {
            ComPort.Write(argAry, Offset, Count);   //　コマンドの送信

            Array.Resize(ref BfrComStrings, Count + 1);

            BfrComStrings[0] = (byte)'S';
            Array.Copy(argAry, 0, BfrComStrings, 1, Count);

            CommunicationLog.SetComText(BfrComStrings, BfrComStrings.Length);

            FlgComStrings = true;
        }

        /********************************************************************************/
        /*	名称	:	タイマハンドラ													*/
        /*	概要	:	プロトコルを実行する											*/
        /*	注意	:																	*/
        /********************************************************************************/
        private void MyTimerCom_Tick(object state)
        {
            int num;
            byte ReadData;
            ComResult Res;
            bool ResTimeOut = false;
            TimeSpan tsPastTime = stopWatch.Elapsed;    //タイムアウトタイマ値

            MyTimerCom.Change(-1, TmrInterval);          //タイマの停止

            num = ReadBuff.DateNum();   //受信済みデータのバイト数

            if (num != 0)
            {
                switch (ComSeq)
                {
                    case ComPhase.WaitAck:
                        ReadData = ReadBuff.GetRecBuff();
                        if (ReadData == (byte)CodeChar.Ack)
                        {
                            //ACK受信
                            if (VerMode != true)
                            {
                                stopWatch.Stop();
                                stopWatch.Reset();
                            }
                            ComSeq = ComPhase.WaitStx;
                        }
                        else if (ReadData == (byte)CodeChar.Nak)
                        {
                            //NAK受信
                            stopWatch.Stop();
                            stopWatch.Reset();
                            RetryCnt++;
                            if (RetryCnt < RetryMax)
                            {
                                //リトライ再送信
                                try
                                {
                                    ComPortWrite(CmdBfr, 0, (CmdLen + 1));
                                    stopWatch.Start();
                                }
                                catch
                                {
                                    if (VerMode == true)
                                    {
                                        VerMode = false;
                                        Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                                        RecBfrPtr = 0;
                                        FlgRecVerion = true;
                                    }
                                }
                            }
                            else
                            {
                                //リトライアウト
                                RetryCnt = 0;

                                if (VerMode == true)
                                {
                                    VerMode = false;
                                    Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                                    RecBfrPtr = 0;
                                    FlgRecVerion = true;
                                }
                                ComSeq = ComPhase.WaitStx;
                            }
                        }
                        if (tsPastTime.Seconds > TimeOutValue)
                        {
                            ResTimeOut = true;
                        }

                        break;

                    case ComPhase.WaitStx:
                        ReadData = ReadBuff.GetRecBuff();
                        if (ReadData == (byte)CodeChar.Stx)
                        {
                            RecLrc = (byte)0;
                            RecBfrPtr = 0;
                            Array.Clear(RecBfr, 0, RecBfr.Length);

                            ComSeq = ComPhase.WaitData;

                        }
                        else if (ReadData == (byte)CodeChar.Nak)
                        {
                            //NAK受信
                            stopWatch.Stop();
                            stopWatch.Reset();
                            RetryCnt++;
                            if (RetryCnt < RetryMax)
                            {
                                //リトライ再送信
                                try
                                {
                                    ComPortWrite(CmdBfr, 0, (CmdLen + 1));
                                    stopWatch.Start();
                                }
                                catch
                                {
                                    if (VerMode == true)
                                    {
                                        VerMode = false;
                                        Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                                        RecBfrPtr = 0;
                                        FlgRecVerion = true;
                                    }
                                }
                            }
                            else
                            {
                                //リトライアウト
                                RetryCnt = 0;

                                if (VerMode == true)
                                {
                                    VerMode = false;
                                    Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                                    RecBfrPtr = 0;
                                    FlgRecVerion = true;
                                }
                                ComSeq = ComPhase.WaitStx;
                            }

                        }

                        if (tsPastTime.Seconds > TimeOutValue)
                        {
                            ResTimeOut = true;
                        }

                        break;

                    case ComPhase.WaitData:
                        ReadData = ReadBuff.GetRecBuff();
                        if (ReadData == (byte)'D')
                        {                //データ
                            RecLrc = ReadData;      // LRC 初期値
                            ComSeq = ComPhase.WaitEtx;
                            ComMode = RecMode.ReadData;

                        }
                        else if ((ReadData == (byte)'S'))
                        {       //ステータス
                            RecLrc = ReadData;      // LRC 初期値
                            ComSeq = ComPhase.WaitEtx;
                            ComMode = RecMode.Status;

                        }

                        if (tsPastTime.Seconds > TimeOutValue)
                        {
                            ResTimeOut = true;
                        }
                        break;

                    case ComPhase.WaitEtx:
                        for (int i = 0; i < num; i++)
                        {
                            ReadData = ReadBuff.GetRecBuff();
                            RecLrc ^= ReadData; // LRC 計算

                            if (ReadData != (byte)CodeChar.Etx)
                            {
                                RecBfr[RecBfrPtr] = ReadData;
                                RecBfrPtr++;
                            }
                            else
                            {
                                RecBfr[RecBfrPtr] = (byte)0;
                                ComSeq = ComPhase.WaitLrc;
                                break;
                            }
                        }

                        if (tsPastTime.Seconds > TimeOutValue)
                        {
                            ResTimeOut = true;
                        }
                        break;

                    case ComPhase.WaitLrc:
                        ReadData = ReadBuff.GetRecBuff();

                        if (RecLrc == ReadData)
                        {

                            SndBfr[0] = (byte)CodeChar.Ack;
                            SndBfr[1] = (byte)'\0';
                            Res = SendCmd(ref SndBfr);

                            ComSeq = ComPhase.WaitStx;

                            if (VerMode == true)
                            {
                                VerMode = false;
                                Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                                Array.Copy(RecBfr, 0, RecBfrVersion, 0, RecBfrPtr);
                                FlgRecVerion = true;

                            }
                            else
                            {
                                if (ComMode == RecMode.ReadData)
                                {
                                    Array.Clear(RecBfrData, 0, RecBfrData.Length);
                                    Array.Copy(RecBfr, 0, RecBfrData, 0, RecBfrPtr);
                                    FlgRecData = true;

                                }
                                else if (ComMode == RecMode.Status)
                                {
                                    Array.Clear(RecBfrStatus, 0, RecBfrStatus.Length);
                                    Array.Resize(ref RecBfrStatus, RecBfrPtr + 1);
                                    Array.Copy(RecBfr, 0, RecBfrStatus, 0, RecBfrPtr);

                                    string text = System.Text.Encoding.UTF8.GetString(RecBfrStatus);

                                    RecBfrStatusCosde = int.Parse(text);

                                    stopWatch.Stop();
                                    stopWatch.Reset();
                                    ComMode = RecMode.NoAct;
                                    FlgRecStatus = true;
                                }
                            }
                            stopWatch.Stop();
                            stopWatch.Reset();
                            ComMode = RecMode.NoAct;
                        }
                        else
                        {
                            SndBfr[0] = (byte)CodeChar.Nak;
                            SndBfr[1] = (byte)'\0';

                            stopWatch.Stop();
                            stopWatch.Reset();
                            stopWatch.Start();

                            Res = SendCmd(ref SndBfr);
                            ComSeq = ComPhase.WaitStx;
                        }
                        break;
                }
            }
            else
            {
                if (tsPastTime.Seconds > TimeOutValue)
                {
                    ResTimeOut = true;
                }
            }

            if (ResTimeOut == true)
            {
                //タイムアウトした場合、バージョンコマンド中なら空文字を送信
                if (VerMode == true)
                {
                    VerMode = false;
                    Array.Clear(RecBfrVersion, 0, RecBfrVersion.Length);
                    RecBfrPtr = 0;
                    FlgRecVerion = true;
                }

                ResTimeOut = false;
                stopWatch.Stop();
                stopWatch.Reset();
                ComSeq = ComPhase.WaitStx;

                RecBfrStatusCosde = 10; //タイムアウトステータスを発行
                FlgRecStatus = true;
            }

            MyTimerCom.Change(TmrInterval, TmrInterval);

        }

        /********************************************************************************/
        /*	名称	:	タイマハンドラ													*/
        /*	概要	:	イベントを発行する												*/
        /*	注意	:																	*/
        /********************************************************************************/
        private void MyTimerEvent_Tick(object state)
        {
            MyTimerEvent.Change(-1, TmrInterval);    //タイマの停止

            if (FlgRecVerion == true)
            {
                FlgRecVerion = false;
                RecVersion(RecBfrVersion, RecBfrPtr, 0);    //バージョンイベントの発行
            }

            if (FlgRecData == true)
            {
                FlgRecData = false;
                RecReadData(RecBfrData, RecBfrPtr, 0);      //磁気データ受信イベントの発行
            }

            if (FlgRecStatus == true)
            {
                FlgRecStatus = false;
                RecStatus(RecBfrStatus, RecBfrPtr, RecBfrStatusCosde);  //ステータスイベントの発行
            }

            if (FlgComStrings == true)
            {
                int DataNum = CommunicationLog.DateNum();
                bool FlgSend = false;
                byte[] Res = new byte[BuffSize];

                if (DataNum != 0)
                {
                    Res = CommunicationLog.GetComText();

                    if (Res[0] == (byte)'S')
                    {
                        FlgSend = true;
                    }
                    else
                    {
                        FlgSend = false;
                    }
                    for (int j = 1; j < Res.Length; j++)
                    {
                        if (Res[j] == 0)
                        {
                            Array.Copy(Res, 1, Res, 0, Res.Length - 1);
                            Array.Resize(ref Res, j - 1);
                            break;
                        }
                    }
                    if (Res.Length != 0)
                    {
                        if (FlgSend == true)
                        {
                            ComSendData(Res, Res.Length, 0);    //送信イベントの発行
                        }
                        else
                        {
                            ComRecvData(Res, Res.Length, 0);    //受信イベントの発行
                        }
                    }
                }
            }

            MyTimerEvent.Change(TmrInterval, TmrInterval);    //タイマの起動
        }

        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            int DataNum = sp.BytesToRead;       //受信データサイズの取り出し

            byte[] bRecTmp = new byte[DataNum]; //バッファの初期化
            sp.Read(bRecTmp, 0, DataNum);       //データの取り出し

            ReadBuff.SetRecBuff(bRecTmp, bRecTmp.Length);   //リングバッファへの格納


            Array.Resize(ref BfrComStrings, DataNum + 1);

            BfrComStrings[0] = (byte)'R';
            Array.Copy(bRecTmp, 0, BfrComStrings, 1, bRecTmp.Length);

            CommunicationLog.SetComText(BfrComStrings, BfrComStrings.Length);

            FlgComStrings = true;
        }

        public delegate void EvtHandler(EventArg e);

        private void RecVersion(byte[] argAry, int num, int StatusCode)
        {
            if (MagRecvVersionEvent != null)
            {
                MagRecvVersionEvent(new EventArg(argAry, num, StatusCode));
            }
        }

        private void RecReadData(byte[] argAry, int num, int StatusCode)
        {
            if (MagRecvDataEvent != null)
            {
                MagRecvDataEvent(new EventArg(argAry, num, StatusCode));
            }
        }

        private void RecStatus(byte[] argAry, int num, int StatusCode)
        {
            if (MagRecvStatus != null)
            {
                MagRecvStatus(new EventArg(argAry, num, StatusCode));
            }
        }

        private void ComSendData(byte[] argAry, int num, int StatusCode)
        {
            if (MagComSendEvent != null)
            {
                MagComSendEvent(new EventArg(argAry, num, StatusCode));
            }
        }

        private void ComRecvData(byte[] argAry, int num, int StatusCode)
        {
            if (MagComRecvEvent != null)
            {
                MagComRecvEvent(new EventArg(argAry, num, StatusCode));
            }
        }
        
    }

    public class EventArg : EventArgs
    {
        private byte[] PrivateAry = { 0 };
        private int PrivateNum = 1;
        private int PrivateStatCode = 0;

        public EventArg(byte[] ArgAry, int ArgNum, int ArgStatusCode)
        {
            Array.Resize(ref PrivateAry, ArgNum);
            PrivateAry = ArgAry;
            PrivateNum = ArgNum;
            PrivateStatCode = ArgStatusCode;
        }
        public byte[] ArgData { get { return PrivateAry; } }
        public int ArgNum { get { return PrivateNum; } }
        public int ArgStatusCode { get { return PrivateStatCode; } }
    }

}