namespace ArmAssembly
{
	partial class Form1
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
			this.msMenu = new System.Windows.Forms.MenuStrip();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSymbolLoader = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAsmAnalyzer = new System.Windows.Forms.ToolStripMenuItem();
			this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAnalyzeRun = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNext = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.msMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// msMenu
			// 
			this.msMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.analyzeToolStripMenuItem});
			this.msMenu.Location = new System.Drawing.Point(0, 0);
			this.msMenu.Name = "msMenu";
			this.msMenu.Size = new System.Drawing.Size(546, 28);
			this.msMenu.TabIndex = 1;
			this.msMenu.Text = "menuStrip1";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSymbolLoader,
            this.tsmiAsmAnalyzer});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// tsmiSymbolLoader
			// 
			this.tsmiSymbolLoader.Name = "tsmiSymbolLoader";
			this.tsmiSymbolLoader.Size = new System.Drawing.Size(185, 26);
			this.tsmiSymbolLoader.Text = "Symbol Loader";
			this.tsmiSymbolLoader.Click += new System.EventHandler(this.tsmiSymbolLoader_Click);
			// 
			// tsmiAsmAnalyzer
			// 
			this.tsmiAsmAnalyzer.Name = "tsmiAsmAnalyzer";
			this.tsmiAsmAnalyzer.Size = new System.Drawing.Size(185, 26);
			this.tsmiAsmAnalyzer.Text = "Asm Analyzer";
			this.tsmiAsmAnalyzer.Click += new System.EventHandler(this.tsmiAsmAnalyzer_Click);
			// 
			// analyzeToolStripMenuItem
			// 
			this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAnalyzeRun,
            this.tsmiNext});
			this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
			this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
			this.analyzeToolStripMenuItem.Text = "Analyze";
			// 
			// tsmiAnalyzeRun
			// 
			this.tsmiAnalyzeRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsmiAnalyzeRun.Name = "tsmiAnalyzeRun";
			this.tsmiAnalyzeRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.tsmiAnalyzeRun.Size = new System.Drawing.Size(170, 26);
			this.tsmiAnalyzeRun.Text = "Run";
			this.tsmiAnalyzeRun.Click += new System.EventHandler(this.tsmiAnalyzeRun_Click);
			// 
			// tsmiNext
			// 
			this.tsmiNext.Enabled = false;
			this.tsmiNext.Name = "tsmiNext";
			this.tsmiNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmiNext.Size = new System.Drawing.Size(170, 26);
			this.tsmiNext.Text = "Next";
			this.tsmiNext.Click += new System.EventHandler(this.tsmiNext_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 28);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(546, 350);
			this.splitContainer1.SplitterDistance = 212;
			this.splitContainer1.TabIndex = 3;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer2.Size = new System.Drawing.Size(330, 350);
			this.splitContainer2.SplitterDistance = 140;
			this.splitContainer2.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(546, 378);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.msMenu);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.msMenu;
			this.Name = "Form1";
			this.Text = "Arm Assembly";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.msMenu.ResumeLayout(false);
			this.msMenu.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip msMenu;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiSymbolLoader;
		private System.Windows.Forms.ToolStripMenuItem tsmiAsmAnalyzer;
		private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeRun;
		private System.Windows.Forms.ToolStripMenuItem tsmiNext;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
	}
}

