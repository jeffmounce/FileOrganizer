namespace FileOrganizer
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstFolders = new System.Windows.Forms.CheckedListBox();
			this.startOperation = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.chkShowLog = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lstFolders
			// 
			this.lstFolders.AllowDrop = true;
			this.lstFolders.FormattingEnabled = true;
			this.lstFolders.Location = new System.Drawing.Point(13, 12);
			this.lstFolders.Name = "lstFolders";
			this.lstFolders.Size = new System.Drawing.Size(2443, 895);
			this.lstFolders.TabIndex = 0;
			this.lstFolders.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstFolders_ItemCheck);
			this.lstFolders.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFolders_DragDrop);
			this.lstFolders.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFolders_DragEnter);
			// 
			// startOperation
			// 
			this.startOperation.FlatAppearance.BorderSize = 2;
			this.startOperation.Location = new System.Drawing.Point(12, 923);
			this.startOperation.Name = "startOperation";
			this.startOperation.Size = new System.Drawing.Size(781, 77);
			this.startOperation.TabIndex = 1;
			this.startOperation.Text = "Start";
			this.startOperation.UseVisualStyleBackColor = true;
			this.startOperation.Click += new System.EventHandler(this.startOperation_Click);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(13, 13);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(2443, 894);
			this.txtLog.TabIndex = 2;
			this.txtLog.Visible = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(810, 923);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(451, 77);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Clear List";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// chkShowLog
			// 
			this.chkShowLog.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkShowLog.AutoSize = true;
			this.chkShowLog.Location = new System.Drawing.Point(1283, 940);
			this.chkShowLog.Name = "chkShowLog";
			this.chkShowLog.Size = new System.Drawing.Size(151, 42);
			this.chkShowLog.TabIndex = 5;
			this.chkShowLog.Text = "Show Log";
			this.chkShowLog.UseVisualStyleBackColor = true;
			this.chkShowLog.CheckedChanged += new System.EventHandler(this.chkShowLog_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(2468, 1012);
			this.Controls.Add(this.chkShowLog);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.startOperation);
			this.Controls.Add(this.lstFolders);
			this.Controls.Add(this.txtLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Organizer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox lstFolders;
		private System.Windows.Forms.Button startOperation;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.CheckBox chkShowLog;
	}
}

