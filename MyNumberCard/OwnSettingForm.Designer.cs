namespace MyNumberCard {
	partial class OwnSettingForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnSettingForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenConfigFileDialogButton = new System.Windows.Forms.Button();
            this.ConfigPathTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OpenIssueLogFileButton = new System.Windows.Forms.Button();
            this.IssueLogPathTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OpenNumberingMasterFileButton = new System.Windows.Forms.Button();
            this.NumberingMasterPathTextBox = new System.Windows.Forms.TextBox();
            this.OpenConfigFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenNumberingMasterFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.IssueLogFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OpenConfigFileDialogButton);
            this.groupBox1.Controls.Add(this.ConfigPathTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共有設定 設定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "共有設定 ファイル";
            // 
            // OpenConfigFileDialogButton
            // 
            this.OpenConfigFileDialogButton.Location = new System.Drawing.Point(410, 16);
            this.OpenConfigFileDialogButton.Name = "OpenConfigFileDialogButton";
            this.OpenConfigFileDialogButton.Size = new System.Drawing.Size(24, 23);
            this.OpenConfigFileDialogButton.TabIndex = 1;
            this.OpenConfigFileDialogButton.Text = "…";
            this.OpenConfigFileDialogButton.UseVisualStyleBackColor = true;
            this.OpenConfigFileDialogButton.Click += new System.EventHandler(this.OpenConfigFileDialogButton_Click);
            // 
            // ConfigPathTextBox
            // 
            this.ConfigPathTextBox.Location = new System.Drawing.Point(146, 19);
            this.ConfigPathTextBox.Name = "ConfigPathTextBox";
            this.ConfigPathTextBox.Size = new System.Drawing.Size(258, 19);
            this.ConfigPathTextBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.OpenIssueLogFileButton);
            this.groupBox3.Controls.Add(this.IssueLogPathTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 53);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "発行ログフォルダ 設定";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "発行ログ格納 フォルダ";
            // 
            // OpenIssueLogFileButton
            // 
            this.OpenIssueLogFileButton.Location = new System.Drawing.Point(410, 15);
            this.OpenIssueLogFileButton.Name = "OpenIssueLogFileButton";
            this.OpenIssueLogFileButton.Size = new System.Drawing.Size(24, 23);
            this.OpenIssueLogFileButton.TabIndex = 1;
            this.OpenIssueLogFileButton.Text = "…";
            this.OpenIssueLogFileButton.UseVisualStyleBackColor = true;
            this.OpenIssueLogFileButton.Click += new System.EventHandler(this.OpenIssueLogFileButton_Click);
            // 
            // IssueLogPathTextBox
            // 
            this.IssueLogPathTextBox.Location = new System.Drawing.Point(146, 18);
            this.IssueLogPathTextBox.Name = "IssueLogPathTextBox";
            this.IssueLogPathTextBox.Size = new System.Drawing.Size(258, 19);
            this.IssueLogPathTextBox.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(264, 179);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(370, 179);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.OpenNumberingMasterFileButton);
            this.groupBox2.Controls.Add(this.NumberingMasterPathTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "採番マスタ 設定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "採番マスタ ファイル";
            // 
            // OpenNumberingMasterFileButton
            // 
            this.OpenNumberingMasterFileButton.Location = new System.Drawing.Point(410, 15);
            this.OpenNumberingMasterFileButton.Name = "OpenNumberingMasterFileButton";
            this.OpenNumberingMasterFileButton.Size = new System.Drawing.Size(24, 23);
            this.OpenNumberingMasterFileButton.TabIndex = 1;
            this.OpenNumberingMasterFileButton.Text = "…";
            this.OpenNumberingMasterFileButton.UseVisualStyleBackColor = true;
            this.OpenNumberingMasterFileButton.Click += new System.EventHandler(this.OpenNumberingMasterFileButton_Click);
            // 
            // NumberingMasterPathTextBox
            // 
            this.NumberingMasterPathTextBox.Location = new System.Drawing.Point(146, 18);
            this.NumberingMasterPathTextBox.Name = "NumberingMasterPathTextBox";
            this.NumberingMasterPathTextBox.Size = new System.Drawing.Size(258, 19);
            this.NumberingMasterPathTextBox.TabIndex = 0;
            // 
            // OpenConfigFileDialog
            // 
            this.OpenConfigFileDialog.DefaultExt = "xml";
            this.OpenConfigFileDialog.FileName = "Config.xml";
            this.OpenConfigFileDialog.Filter = "\"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*\";";
            this.OpenConfigFileDialog.Title = "設定ファイルの選択";
            // 
            // OpenNumberingMasterFileDialog
            // 
            this.OpenNumberingMasterFileDialog.DefaultExt = "xml";
            this.OpenNumberingMasterFileDialog.FileName = "NumberingMaster.xml";
            this.OpenNumberingMasterFileDialog.Filter = "\"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*\";";
            // 
            // OwnSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 215);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OwnSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参照先設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OpenConfigFileDialogButton;
		private System.Windows.Forms.TextBox ConfigPathTextBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button OpenIssueLogFileButton;
		private System.Windows.Forms.TextBox IssueLogPathTextBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button OpenNumberingMasterFileButton;
		private System.Windows.Forms.TextBox NumberingMasterPathTextBox;
		private System.Windows.Forms.OpenFileDialog OpenConfigFileDialog;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		private System.Windows.Forms.OpenFileDialog OpenNumberingMasterFileDialog;
		private System.Windows.Forms.FolderBrowserDialog IssueLogFolderBrowserDialog;
	}
}