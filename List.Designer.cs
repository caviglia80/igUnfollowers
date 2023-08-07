namespace UnFollowers
{
	partial class List
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
			lb1 = new ListBox();
			SuspendLayout();
			// 
			// lb1
			// 
			lb1.Dock = DockStyle.Fill;
			lb1.FormattingEnabled = true;
			lb1.ItemHeight = 15;
			lb1.Location = new Point(0, 0);
			lb1.Name = "lb1";
			lb1.Size = new Size(195, 376);
			lb1.TabIndex = 0;
			// 
			// List
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(195, 376);
			Controls.Add(lb1);
			Name = "List";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "List";
			TopMost = true;
			Load += List_Load;
			ResumeLayout(false);
		}

		#endregion

		private ListBox lb1;
	}
}