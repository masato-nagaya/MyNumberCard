﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConfigFile.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ヘッダー桁数")]
        public string MunicipalCode_LengthLabel {
            get {
                return ((string)(this["MunicipalCode_LengthLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int MunicipalCode_Length {
            get {
                return ((int)(this["MunicipalCode_Length"]));
            }
            set {
                this["MunicipalCode_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ヘッダー")]
        public string MunicipalCode_ValueLabel {
            get {
                return ((string)(this["MunicipalCode_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("000000          1421")]
        public string MunicipalCode_Value {
            get {
                return ((string)(this["MunicipalCode_Value"]));
            }
            set {
                this["MunicipalCode_Value"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MunicipalCode_PaddingLabel {
            get {
                return ((string)(this["MunicipalCode_PaddingLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public global::EncodeData.PaddingStat MunicipalCode_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["MunicipalCode_PaddingStatus"]));
            }
            set {
                this["MunicipalCode_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char MunicipalCode_PaddingCharacter {
            get {
                return ((char)(this["MunicipalCode_PaddingCharacter"]));
            }
            set {
                this["MunicipalCode_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("拠点コード桁数")]
        public string BranchOffice_LengthLabel {
            get {
                return ((string)(this["BranchOffice_LengthLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public int BranchOffice_Length {
            get {
                return ((int)(this["BranchOffice_Length"]));
            }
            set {
                this["BranchOffice_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("拠点コード")]
        public string BranchOffice_ValueLabel {
            get {
                return ((string)(this["BranchOffice_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4000")]
        public string BranchOffice_Value {
            get {
                return ((string)(this["BranchOffice_Value"]));
            }
            set {
                this["BranchOffice_Value"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string BranchOffice_PaddingLabel {
            get {
                return ((string)(this["BranchOffice_PaddingLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public global::EncodeData.PaddingStat BranchOffice_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["BranchOffice_PaddingStatus"]));
            }
            set {
                this["BranchOffice_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char BranchOffice_PaddingCharacter {
            get {
                return ((char)(this["BranchOffice_PaddingCharacter"]));
            }
            set {
                this["BranchOffice_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("印鑑登録番号桁数")]
        public string SealRegistration_LengthLabel {
            get {
                return ((string)(this["SealRegistration_LengthLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int SealRegistration_Length {
            get {
                return ((int)(this["SealRegistration_Length"]));
            }
            set {
                this["SealRegistration_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("印鑑登録番号 初期値")]
        public string SealRegistration_ValueLabel {
            get {
                return ((string)(this["SealRegistration_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50000001")]
        public string SealRegistration_Value {
            get {
                return ((string)(this["SealRegistration_Value"]));
            }
            set {
                this["SealRegistration_Value"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SealRegistration_PaddingLabel {
            get {
                return ((string)(this["SealRegistration_PaddingLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char SealRegistration_PaddingCharacter {
            get {
                return ((char)(this["SealRegistration_PaddingCharacter"]));
            }
            set {
                this["SealRegistration_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Padding_LengthLabel {
            get {
                return ((string)(this["Padding_LengthLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int Padding_Length {
            get {
                return ((int)(this["Padding_Length"]));
            }
            set {
                this["Padding_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Padding_ValueLabel {
            get {
                return ((string)(this["Padding_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Padding_Value {
            get {
                return ((string)(this["Padding_Value"]));
            }
            set {
                this["Padding_Value"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("印鑑登録番号 右 データ埋め")]
        public string Padding_PaddingLabel {
            get {
                return ((string)(this["Padding_PaddingLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Right")]
        public global::EncodeData.PaddingStat Padding_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["Padding_PaddingStatus"]));
            }
            set {
                this["Padding_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char Padding_PaddingCharacter {
            get {
                return ((char)(this["Padding_PaddingCharacter"]));
            }
            set {
                this["Padding_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("higashiomi")]
        public string AppName {
            get {
                return ((string)(this["AppName"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int StartPosition_Length {
            get {
                return ((int)(this["StartPosition_Length"]));
            }
            set {
                this["StartPosition_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal MunicipalCode_LengthMin {
            get {
                return ((decimal)(this["MunicipalCode_LengthMin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public decimal MunicipalCode_LengthMax {
            get {
                return ((decimal)(this["MunicipalCode_LengthMax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal BranchOffice_LengthMin {
            get {
                return ((decimal)(this["BranchOffice_LengthMin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public decimal BranchOffice_LengthMax {
            get {
                return ((decimal)(this["BranchOffice_LengthMax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public decimal SealRegistration_LengthMin {
            get {
                return ((decimal)(this["SealRegistration_LengthMin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public decimal SealRegistration_LengthMax {
            get {
                return ((decimal)(this["SealRegistration_LengthMax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal Padding_LengthMin {
            get {
                return ((decimal)(this["Padding_LengthMin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("69")]
        public decimal Padding_LengthMax {
            get {
                return ((decimal)(this["Padding_LengthMax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("開始位置")]
        public string StartPosition_ValueLabel {
            get {
                return ((string)(this["StartPosition_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal StartPosition_LengthMin {
            get {
                return ((decimal)(this["StartPosition_LengthMin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public decimal StartPosition_LengthMax {
            get {
                return ((decimal)(this["StartPosition_LengthMax"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char StartPosition_PaddingCharacter {
            get {
                return ((char)(this["StartPosition_PaddingCharacter"]));
            }
            set {
                this["StartPosition_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public global::EncodeData.PaddingStat StartPosition_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["StartPosition_PaddingStatus"]));
            }
            set {
                this["StartPosition_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int PaddingL_Length {
            get {
                return ((int)(this["PaddingL_Length"]));
            }
            set {
                this["PaddingL_Length"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PaddingL_LengthLabel {
            get {
                return ((string)(this["PaddingL_LengthLabel"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("69")]
        public decimal PaddingL_LengthMax {
            get {
                return ((decimal)(this["PaddingL_LengthMax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal PaddingL_LengthMin {
            get {
                return ((decimal)(this["PaddingL_LengthMin"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public char PaddingL_PaddingCharacter {
            get {
                return ((char)(this["PaddingL_PaddingCharacter"]));
            }
            set {
                this["PaddingL_PaddingCharacter"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("印鑑登録番号 左 データ埋め")]
        public string PaddingL_PaddingLabel {
            get {
                return ((string)(this["PaddingL_PaddingLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PaddingL_Value {
            get {
                return ((string)(this["PaddingL_Value"]));
            }
            set {
                this["PaddingL_Value"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PaddingL_ValueLabel {
            get {
                return ((string)(this["PaddingL_ValueLabel"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("開始位置桁数")]
        public string StartPosition_LengthLabel {
            get {
                return ((string)(this["StartPosition_LengthLabel"]));
            }
            set {
                this["StartPosition_LengthLabel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string StartPosition_PaddingLabel {
            get {
                return ((string)(this["StartPosition_PaddingLabel"]));
            }
            set {
                this["StartPosition_PaddingLabel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string StartPosition_Value {
            get {
                return ((string)(this["StartPosition_Value"]));
            }
            set {
                this["StartPosition_Value"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Left")]
        public global::EncodeData.PaddingStat PaddingL_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["PaddingL_PaddingStatus"]));
            }
            set {
                this["PaddingL_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public global::EncodeData.PaddingStat SealRegistration_PaddingStatus {
            get {
                return ((global::EncodeData.PaddingStat)(this["SealRegistration_PaddingStatus"]));
            }
            set {
                this["SealRegistration_PaddingStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ConfigFilePath {
            get {
                return ((string)(this["ConfigFilePath"]));
            }
            set {
                this["ConfigFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string NumberingMasterPath {
            get {
                return ((string)(this["NumberingMasterPath"]));
            }
            set {
                this["NumberingMasterPath"] = value;
            }
        }
    }
}
