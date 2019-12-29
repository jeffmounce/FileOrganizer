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
			this.progressStart = new System.Windows.Forms.ProgressBar();
			this.cancelOperation = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstFolders
			// 
			this.lstFolders.AllowDrop = true;
			this.lstFolders.BackColor = System.Drawing.SystemColors.Window;
			this.lstFolders.FormattingEnabled = true;
			this.lstFolders.Location = new System.Drawing.Point(5, 5);
			this.lstFolders.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.lstFolders.Name = "lstFolders";
			this.lstFolders.Size = new System.Drawing.Size(918, 394);
			this.lstFolders.TabIndex = 0;
			this.lstFolders.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstFolders_ItemCheck);
			this.lstFolders.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFolders_DragDrop);
			this.lstFolders.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFolders_DragEnter);
			// 
			// startOperation
			// 
			this.startOperation.FlatAppearance.BorderSize = 2;
			this.startOperation.Location = new System.Drawing.Point(5, 403);
			this.startOperation.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.startOperation.Name = "startOperation";
			this.startOperation.Size = new System.Drawing.Size(293, 32);
			this.startOperation.TabIndex = 1;
			this.startOperation.Text = "Start";
			this.startOperation.UseVisualStyleBackColor = true;
			this.startOperation.Click += new System.EventHandler(this.startOperation_Click);
			// 
			// txtLog
			// 
			this.txtLog.BackColor = System.Drawing.SystemColors.Info;
			this.txtLog.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.txtLog.Location = new System.Drawing.Point(5, 5);
			this.txtLog.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(918, 394);
			this.txtLog.TabIndex = 5;
			this.txtLog.Visible = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(305, 403);
			this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(96, 32);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Clear List";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// chkShowLog
			// 
			this.chkShowLog.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkShowLog.AutoSize = true;
			this.chkShowLog.Location = new System.Drawing.Point(481, 403);
			this.chkShowLog.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.chkShowLog.Name = "chkShowLog";
			this.chkShowLog.Size = new System.Drawing.Size(65, 23);
			this.chkShowLog.TabIndex = 3;
			this.chkShowLog.Text = "Show Log";
			this.chkShowLog.UseVisualStyleBackColor = true;
			this.chkShowLog.CheckedChanged += new System.EventHandler(this.chkShowLog_CheckedChanged);
			// 
			// progressStart
			// 
			this.progressStart.Location = new System.Drawing.Point(482, 427);
			this.progressStart.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
			this.progressStart.Name = "progressStart";
			this.progressStart.Size = new System.Drawing.Size(440, 8);
			this.progressStart.TabIndex = 6;
			this.progressStart.Visible = false;
			// 
			// cancelOperation
			// 
			this.cancelOperation.Location = new System.Drawing.Point(858, 403);
			this.cancelOperation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.cancelOperation.Name = "cancelOperation";
			this.cancelOperation.Size = new System.Drawing.Size(65, 23);
			this.cancelOperation.TabIndex = 7;
			this.cancelOperation.Text = "Cancel";
			this.cancelOperation.UseVisualStyleBackColor = true;
			this.cancelOperation.Visible = false;
			this.cancelOperation.Click += new System.EventHandler(this.cancelOperation_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(926, 437);
			this.Controls.Add(this.cancelOperation);
			this.Controls.Add(this.progressStart);
			this.Controls.Add(this.chkShowLog);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.startOperation);
			this.Controls.Add(this.lstFolders);
			this.Controls.Add(this.txtLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
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
		private System.Windows.Forms.ProgressBar progressStart;
		private System.Windows.Forms.Button cancelOperation;
	}
}

