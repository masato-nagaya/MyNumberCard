using Utils.Form;

namespace MyNumberCard {
	partial class IssueForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueForm));
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MunicipalCodeTextBox = new System.Windows.Forms.TextBox();
            this.NumberingTextBox = new Utils.Form.NumericTextBox();
            this.BranchOfficeTextBox = new Utils.Form.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusPictureBox = new System.Windows.Forms.PictureBox();
            this.NumberingButton = new System.Windows.Forms.Button();
            this.ManualInputButton = new System.Windows.Forms.Button();
            this.IssueButton = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuOptionCom = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutBoxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPictureBox)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 129);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "印鑑登録番号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MunicipalCodeTextBox);
            this.groupBox2.Controls.Add(this.NumberingTextBox);
            this.groupBox2.Controls.Add(this.BranchOfficeTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 48);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.groupBox2.Size = new System.Drawing.Size(438, 190);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "書込みデータ設定";
            // 
            // MunicipalCodeTextBox
            // 
            this.MunicipalCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MunicipalCodeTextBox.Location = new System.Drawing.Point(132, 37);
            this.MunicipalCodeTextBox.Name = "MunicipalCodeTextBox";
            this.MunicipalCodeTextBox.ReadOnly = true;
            this.MunicipalCodeTextBox.Size = new System.Drawing.Size(283, 25);
            this.MunicipalCodeTextBox.TabIndex = 0;
            this.MunicipalCodeTextBox.TabStop = false;
            // 
            // NumberingTextBox
            // 
            this.NumberingTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberingTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NumberingTextBox.Location = new System.Drawing.Point(132, 127);
            this.NumberingTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.NumberingTextBox.Name = "NumberingTextBox";
            this.NumberingTextBox.ReadOnly = true;
            this.NumberingTextBox.Size = new System.Drawing.Size(283, 25);
            this.NumberingTextBox.TabIndex = 0;
            this.NumberingTextBox.TabStop = false;
            // 
            // BranchOfficeTextBox
            // 
            this.BranchOfficeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BranchOfficeTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BranchOfficeTextBox.Location = new System.Drawing.Point(132, 83);
            this.BranchOfficeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BranchOfficeTextBox.Name = "BranchOfficeTextBox";
            this.BranchOfficeTextBox.ReadOnly = true;
            this.BranchOfficeTextBox.Size = new System.Drawing.Size(283, 25);
            this.BranchOfficeTextBox.TabIndex = 0;
            this.BranchOfficeTextBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "拠点コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "ヘッダー";
            // 
            // StatusPictureBox
            // 
            this.StatusPictureBox.Image = global::MyNumberCard.Properties.Resources.White;
            this.StatusPictureBox.Location = new System.Drawing.Point(493, 48);
            this.StatusPictureBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.StatusPictureBox.Name = "StatusPictureBox";
            this.StatusPictureBox.Size = new System.Drawing.Size(156, 163);
            this.StatusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StatusPictureBox.TabIndex = 4;
            this.StatusPictureBox.TabStop = false;
            // 
            // NumberingButton
            // 
            this.NumberingButton.Location = new System.Drawing.Point(14, 274);
            this.NumberingButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.NumberingButton.Name = "NumberingButton";
            this.NumberingButton.Size = new System.Drawing.Size(149, 82);
            this.NumberingButton.TabIndex = 5;
            this.NumberingButton.TabStop = false;
            this.NumberingButton.Text = "印鑑登録番号 採番";
            this.NumberingButton.UseVisualStyleBackColor = true;
            this.NumberingButton.Click += new System.EventHandler(this.NumberingButton_Click);
            // 
            // ManualInputButton
            // 
            this.ManualInputButton.Location = new System.Drawing.Point(181, 274);
            this.ManualInputButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ManualInputButton.Name = "ManualInputButton";
            this.ManualInputButton.Size = new System.Drawing.Size(148, 82);
            this.ManualInputButton.TabIndex = 5;
            this.ManualInputButton.TabStop = false;
            this.ManualInputButton.Text = "印鑑登録号 入力";
            this.ManualInputButton.UseVisualStyleBackColor = true;
            this.ManualInputButton.Click += new System.EventHandler(this.ManualInputButton_Click);
            // 
            // IssueButton
            // 
            this.IssueButton.Location = new System.Drawing.Point(532, 274);
            this.IssueButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.IssueButton.Name = "IssueButton";
            this.IssueButton.Size = new System.Drawing.Size(148, 82);
            this.IssueButton.TabIndex = 5;
            this.IssueButton.Text = "発行";
            this.IssueButton.UseVisualStyleBackColor = true;
            this.IssueButton.Click += new System.EventHandler(this.IssueButton_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolMenuItem,
            this.HelpMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.MenuStrip.Size = new System.Drawing.Size(686, 25);
            this.MenuStrip.TabIndex = 5;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuOptionCom,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(67, 19);
            this.FileMenuItem.Text = "ファイル(&F)";
            // 
            // MnuOptionCom
            // 
            this.MnuOptionCom.Name = "MnuOptionCom";
            this.MnuOptionCom.Size = new System.Drawing.Size(131, 22);
            this.MnuOptionCom.Text = "通信設定...";
            this.MnuOptionCom.Click += new System.EventHandler(this.MnuOptionCom_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(131, 22);
            this.ExitMenuItem.Text = "終了(&X)";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ToolMenuItem
            // 
            this.ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingMenuItem});
            this.ToolMenuItem.Name = "ToolMenuItem";
            this.ToolMenuItem.Size = new System.Drawing.Size(60, 19);
            this.ToolMenuItem.Text = "ツール(&T)";
            this.ToolMenuItem.Visible = false;
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(112, 22);
            this.SettingMenuItem.Text = "設定(&S)";
            this.SettingMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SettingMenuItem.Click += new System.EventHandler(this.SettingMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutBoxMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 19);
            this.HelpMenuItem.Text = "ヘルプ(&H)";
            // 
            // AboutBoxMenuItem
            // 
            this.AboutBoxMenuItem.Name = "AboutBoxMenuItem";
            this.AboutBoxMenuItem.Size = new System.Drawing.Size(158, 22);
            this.AboutBoxMenuItem.Text = "バージョン情報(&A)";
            this.AboutBoxMenuItem.Click += new System.EventHandler(this.AboutBoxMenuItem_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(376, 274);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(148, 82);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.TabStop = false;
            this.CancelButton.Text = "中断";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            this.CancelButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CancelButton_KeyPress);
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 368);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusStrip.Size = new System.Drawing.Size(686, 22);
            this.StatusStrip.TabIndex = 5;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(10, 17);
            this.StatusLabel.Text = " ";
            // 
            // IssueForm
            // 
            this.AcceptButton = this.IssueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.IssueButton);
            this.Controls.Add(this.ManualInputButton);
            this.Controls.Add(this.NumberingButton);
            this.Controls.Add(this.StatusPictureBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "IssueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "印鑑登録番号書込処理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IssueForm_FormClosing);
            this.Load += new System.EventHandler(this.IssueForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IssueForm_KeyPress);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPictureBox)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox StatusPictureBox;
		private System.Windows.Forms.Button ManualInputButton;
		private System.Windows.Forms.Button IssueButton;
		private System.Windows.Forms.Button NumberingButton;
		private NumericTextBox NumberingTextBox;
		private NumericTextBox BranchOfficeTextBox;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ToolMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
		private new System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutBoxMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.TextBox MunicipalCodeTextBox;
        private System.Windows.Forms.ToolStripMenuItem MnuOptionCom;
    }
}