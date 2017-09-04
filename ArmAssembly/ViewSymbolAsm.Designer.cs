namespace ArmAssembly
{
	partial class ViewSymbolAsm
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
			this.tabSymbolAsm = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// tabSymbolAsm
			// 
			this.tabSymbolAsm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabSymbolAsm.Location = new System.Drawing.Point(0, 0);
			this.tabSymbolAsm.Name = "tabSymbolAsm";
			this.tabSymbolAsm.SelectedIndex = 0;
			this.tabSymbolAsm.Size = new System.Drawing.Size(567, 384);
			this.tabSymbolAsm.TabIndex = 1;
			this.tabSymbolAsm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabSymbolAsm_MouseClick);
			// 
			// ViewSymbolAsm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 384);
			this.Controls.Add(this.tabSymbolAsm);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ViewSymbolAsm";
			this.ShowIcon = false;
			this.Text = "ViewSymbolAsm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewSymbolAsm_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabControl tabSymbolAsm;
	}
}