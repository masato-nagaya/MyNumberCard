using Utils.Form;

namespace ConfigFile {
	partial class ConfigForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BranchOfficeTextBox = new System.Windows.Forms.TextBox();
            this.MunicipalCodeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MakeFileButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.RightPaddingSpaceRadioButton = new System.Windows.Forms.RadioButton();
            this.RightPaddingZeroRadioButton = new System.Windows.Forms.RadioButton();
            this.PaddingCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.LeftPaddingSpaceRadioButton = new System.Windows.Forms.RadioButton();
            this.LeftPaddingZeroRadioButton = new System.Windows.Forms.RadioButton();
            this.MunicipalCodeLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.SealRegistrationLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.BranchOfficeLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SaveConfigFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NumberingMasterTextBox = new Utils.Form.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NumberingMasterButton = new System.Windows.Forms.Button();
            this.SaveNumberingMasterFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunicipalCodeLengthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SealRegistrationLengthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchOfficeLengthUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BranchOfficeTextBox);
            this.groupBox2.Controls.Add(this.MunicipalCodeTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(258, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "登録データ設定";
            // 
            // BranchOfficeTextBox
            // 
            this.BranchOfficeTextBox.Location = new System.Drawing.Point(86, 49);
            this.BranchOfficeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.BranchOfficeTextBox.MaxLength = global::ConfigFile.Properties.Settings.Default.BranchOffice_Length;
            this.BranchOfficeTextBox.Name = "BranchOfficeTextBox";
            this.BranchOfficeTextBox.Size = new System.Drawing.Size(188, 19);
            this.BranchOfficeTextBox.TabIndex = 4;
            this.BranchOfficeTextBox.TabStop = false;
            this.BranchOfficeTextBox.Text = global::ConfigFile.Properties.Settings.Default.BranchOffice_Value;
            // 
            // MunicipalCodeTextBox
            // 
            this.MunicipalCodeTextBox.Location = new System.Drawing.Point(86, 22);
            this.MunicipalCodeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.MunicipalCodeTextBox.MaxLength = global::ConfigFile.Properties.Settings.Default.MunicipalCode_Length;
            this.MunicipalCodeTextBox.Name = "MunicipalCodeTextBox";
            this.MunicipalCodeTextBox.Size = new System.Drawing.Size(188, 19);
            this.MunicipalCodeTextBox.TabIndex = 4;
            this.MunicipalCodeTextBox.TabStop = false;
            this.MunicipalCodeTextBox.Text = global::ConfigFile.Properties.Settings.Default.MunicipalCode_Value;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = global::ConfigFile.Properties.Settings.Default.BranchOffice_ValueLabel;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = global::ConfigFile.Properties.Settings.Default.MunicipalCode_ValueLabel;
            // 
            // MakeFileButton
            // 
            this.MakeFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MakeFileButton.Location = new System.Drawing.Point(337, 182);
            this.MakeFileButton.Name = "MakeFileButton";
            this.MakeFileButton.Size = new System.Drawing.Size(186, 69);
            this.MakeFileButton.TabIndex = 3;
            this.MakeFileButton.TabStop = false;
            this.MakeFileButton.Text = "設定ファイル 作成";
            this.MakeFileButton.UseVisualStyleBackColor = true;
            this.MakeFileButton.Click += new System.EventHandler(this.MakeFileButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.MunicipalCodeLengthUpDown);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.SealRegistrationLengthUpDown);
            this.groupBox4.Controls.Add(this.BranchOfficeLengthUpDown);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(6, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(237, 246);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "出力フォーマット";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.RightPaddingSpaceRadioButton);
            this.groupBox6.Controls.Add(this.RightPaddingZeroRadioButton);
            this.groupBox6.Controls.Add(this.PaddingCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(17, 164);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(194, 74);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            // 
            // RightPaddingSpaceRadioButton
            // 
            this.RightPaddingSpaceRadioButton.AutoSize = true;
            this.RightPaddingSpaceRadioButton.Location = new System.Drawing.Point(19, 46);
            this.RightPaddingSpaceRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.RightPaddingSpaceRadioButton.Name = "RightPaddingSpaceRadioButton";
            this.RightPaddingSpaceRadioButton.Size = new System.Drawing.Size(121, 16);
            this.RightPaddingSpaceRadioButton.TabIndex = 2;
            this.RightPaddingSpaceRadioButton.Text = "右  ’  ’（空白）埋め";
            this.RightPaddingSpaceRadioButton.UseVisualStyleBackColor = true;
            // 
            // RightPaddingZeroRadioButton
            // 
            this.RightPaddingZeroRadioButton.AutoSize = true;
            this.RightPaddingZeroRadioButton.Checked = true;
            this.RightPaddingZeroRadioButton.Location = new System.Drawing.Point(19, 26);
            this.RightPaddingZeroRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.RightPaddingZeroRadioButton.Name = "RightPaddingZeroRadioButton";
            this.RightPaddingZeroRadioButton.Size = new System.Drawing.Size(114, 16);
            this.RightPaddingZeroRadioButton.TabIndex = 1;
            this.RightPaddingZeroRadioButton.TabStop = true;
            this.RightPaddingZeroRadioButton.Text = "右  ’0’（セロ）埋め";
            this.RightPaddingZeroRadioButton.UseVisualStyleBackColor = true;
            // 
            // PaddingCheckBox
            // 
            this.PaddingCheckBox.AutoSize = true;
            this.PaddingCheckBox.Location = new System.Drawing.Point(5, 5);
            this.PaddingCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.PaddingCheckBox.Name = "PaddingCheckBox";
            this.PaddingCheckBox.Size = new System.Drawing.Size(166, 16);
            this.PaddingCheckBox.TabIndex = 0;
            this.PaddingCheckBox.Text = "印鑑登録番号 右 データ埋め";
            this.PaddingCheckBox.UseVisualStyleBackColor = true;
            this.PaddingCheckBox.CheckedChanged += new System.EventHandler(this.PaddingCheckBox_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.LeftPaddingSpaceRadioButton);
            this.groupBox5.Controls.Add(this.LeftPaddingZeroRadioButton);
            this.groupBox5.Location = new System.Drawing.Point(17, 93);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(194, 66);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = global::ConfigFile.Properties.Settings.Default.SealRegistration_PaddingLabel;
            // 
            // LeftPaddingSpaceRadioButton
            // 
            this.LeftPaddingSpaceRadioButton.AutoSize = true;
            this.LeftPaddingSpaceRadioButton.Location = new System.Drawing.Point(19, 40);
            this.LeftPaddingSpaceRadioButton.Name = "LeftPaddingSpaceRadioButton";
            this.LeftPaddingSpaceRadioButton.Size = new System.Drawing.Size(121, 16);
            this.LeftPaddingSpaceRadioButton.TabIndex = 0;
            this.LeftPaddingSpaceRadioButton.Text = "左  ’  ’（空白）埋め";
            this.LeftPaddingSpaceRadioButton.UseVisualStyleBackColor = true;
            // 
            // LeftPaddingZeroRadioButton
            // 
            this.LeftPaddingZeroRadioButton.AutoSize = true;
            this.LeftPaddingZeroRadioButton.Checked = true;
            this.LeftPaddingZeroRadioButton.Location = new System.Drawing.Point(19, 18);
            this.LeftPaddingZeroRadioButton.Name = "LeftPaddingZeroRadioButton";
            this.LeftPaddingZeroRadioButton.Size = new System.Drawing.Size(114, 16);
            this.LeftPaddingZeroRadioButton.TabIndex = 0;
            this.LeftPaddingZeroRadioButton.TabStop = true;
            this.LeftPaddingZeroRadioButton.Text = "左  ’0’（セロ）埋め";
            this.LeftPaddingZeroRadioButton.UseVisualStyleBackColor = true;
            // 
            // MunicipalCodeLengthUpDown
            // 
            this.MunicipalCodeLengthUpDown.Location = new System.Drawing.Point(117, 20);
            this.MunicipalCodeLengthUpDown.Maximum = global::ConfigFile.Properties.Settings.Default.MunicipalCode_LengthMax;
            this.MunicipalCodeLengthUpDown.Minimum = global::ConfigFile.Properties.Settings.Default.MunicipalCode_LengthMin;
            this.MunicipalCodeLengthUpDown.Name = "MunicipalCodeLengthUpDown";
            this.MunicipalCodeLengthUpDown.Size = new System.Drawing.Size(74, 19);
            this.MunicipalCodeLengthUpDown.TabIndex = 1;
            this.MunicipalCodeLengthUpDown.TabStop = false;
            this.MunicipalCodeLengthUpDown.Value = global::ConfigFile.Properties.Settings.Default.MunicipalCode_Length;
            this.MunicipalCodeLengthUpDown.ValueChanged += new System.EventHandler(this.MunicipalCodeLengthUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = global::ConfigFile.Properties.Settings.Default.SealRegistration_LengthLabel;
            // 
            // SealRegistrationLengthUpDown
            // 
            this.SealRegistrationLengthUpDown.Location = new System.Drawing.Point(117, 69);
            this.SealRegistrationLengthUpDown.Maximum = global::ConfigFile.Properties.Settings.Default.SealRegistration_LengthMax;
            this.SealRegistrationLengthUpDown.Minimum = global::ConfigFile.Properties.Settings.Default.SealRegistration_LengthMin;
            this.SealRegistrationLengthUpDown.Name = "SealRegistrationLengthUpDown";
            this.SealRegistrationLengthUpDown.Size = new System.Drawing.Size(74, 19);
            this.SealRegistrationLengthUpDown.TabIndex = 1;
            this.SealRegistrationLengthUpDown.TabStop = false;
            this.SealRegistrationLengthUpDown.Value = global::ConfigFile.Properties.Settings.Default.SealRegistration_Length;
            this.SealRegistrationLengthUpDown.ValueChanged += new System.EventHandler(this.SealRegistrationLengthUpDown_ValueChanged);
            // 
            // BranchOfficeLengthUpDown
            // 
            this.BranchOfficeLengthUpDown.Location = new System.Drawing.Point(117, 45);
            this.BranchOfficeLengthUpDown.Maximum = global::ConfigFile.Properties.Settings.Default.BranchOffice_LengthMax;
            this.BranchOfficeLengthUpDown.Minimum = global::ConfigFile.Properties.Settings.Default.BranchOffice_LengthMin;
            this.BranchOfficeLengthUpDown.Name = "BranchOfficeLengthUpDown";
            this.BranchOfficeLengthUpDown.Size = new System.Drawing.Size(74, 19);
            this.BranchOfficeLengthUpDown.TabIndex = 1;
            this.BranchOfficeLengthUpDown.TabStop = false;
            this.BranchOfficeLengthUpDown.Value = global::ConfigFile.Properties.Settings.Default.BranchOffice_Length;
            this.BranchOfficeLengthUpDown.ValueChanged += new System.EventHandler(this.BranchOfficeLengthUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = global::ConfigFile.Properties.Settings.Default.MunicipalCode_LengthLabel;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = global::ConfigFile.Properties.Settings.Default.BranchOffice_LengthLabel;
            // 
            // SaveConfigFileDialog
            // 
            this.SaveConfigFileDialog.FileName = "Config.xml";
            this.SaveConfigFileDialog.Filter = "\"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*\";";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.MakeFileButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 271);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "設定ファイル作成";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.NumberingMasterTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.NumberingMasterButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(548, 101);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "採番マスタファイル作成";
            // 
            // NumberingMasterTextBox
            // 
            this.NumberingMasterTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NumberingMasterTextBox.Location = new System.Drawing.Point(133, 45);
            this.NumberingMasterTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NumberingMasterTextBox.MaxLength = global::ConfigFile.Properties.Settings.Default.SealRegistration_Length;
            this.NumberingMasterTextBox.Name = "NumberingMasterTextBox";
            this.NumberingMasterTextBox.Size = new System.Drawing.Size(145, 19);
            this.NumberingMasterTextBox.TabIndex = 3;
            this.NumberingMasterTextBox.TabStop = false;
            this.NumberingMasterTextBox.Text = global::ConfigFile.Properties.Settings.Default.SealRegistration_Value;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = global::ConfigFile.Properties.Settings.Default.SealRegistration_ValueLabel;
            // 
            // NumberingMasterButton
            // 
            this.NumberingMasterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberingMasterButton.Location = new System.Drawing.Point(337, 18);
            this.NumberingMasterButton.Name = "NumberingMasterButton";
            this.NumberingMasterButton.Size = new System.Drawing.Size(186, 69);
            this.NumberingMasterButton.TabIndex = 3;
            this.NumberingMasterButton.TabStop = false;
            this.NumberingMasterButton.Text = "採番マスタファイル 作成";
            this.NumberingMasterButton.UseVisualStyleBackColor = true;
            this.NumberingMasterButton.Click += new System.EventHandler(this.NumberingMasterButton_Click);
            // 
            // SaveNumberingMasterFileDialog
            // 
            this.SaveNumberingMasterFileDialog.FileName = "NumberingMaster.xml";
            this.SaveNumberingMasterFileDialog.Filter = "\"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*\";";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 407);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "設定ファイル・採番マスタファイル作成";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunicipalCodeLengthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SealRegistrationLengthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchOfficeLengthUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown SealRegistrationLengthUpDown;
		private System.Windows.Forms.NumericUpDown BranchOfficeLengthUpDown;
		private System.Windows.Forms.NumericUpDown MunicipalCodeLengthUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button MakeFileButton;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton LeftPaddingSpaceRadioButton;
		private System.Windows.Forms.RadioButton LeftPaddingZeroRadioButton;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.SaveFileDialog SaveConfigFileDialog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button NumberingMasterButton;
		private System.Windows.Forms.SaveFileDialog SaveNumberingMasterFileDialog;
		private NumericTextBox NumberingMasterTextBox;
		private System.Windows.Forms.TextBox MunicipalCodeTextBox;
        private System.Windows.Forms.TextBox BranchOfficeTextBox;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox PaddingCheckBox;
		private System.Windows.Forms.RadioButton RightPaddingSpaceRadioButton;
		private System.Windows.Forms.RadioButton RightPaddingZeroRadioButton;
	}
}