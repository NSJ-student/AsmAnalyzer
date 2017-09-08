namespace ArmAssembly
{
	partial class FindRows
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
			this.tabFind = new System.Windows.Forms.TabControl();
			this.tpFind = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbCmpSrc = new System.Windows.Forms.ComboBox();
			this.cbOperator = new System.Windows.Forms.ComboBox();
			this.txtMapFilter = new System.Windows.Forms.TextBox();
			this.lbFilter = new System.Windows.Forms.Label();
			this.btnFindOK = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.tpFindString = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lbStringFilter = new System.Windows.Forms.Label();
			this.txtStringFilter = new System.Windows.Forms.TextBox();
			this.btnStringFind = new System.Windows.Forms.Button();
			this.tabFind.SuspendLayout();
			this.tpFind.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tpFindString.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabFind
			// 
			this.tabFind.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabFind.Controls.Add(this.tpFind);
			this.tabFind.Controls.Add(this.tpFindString);
			this.tabFind.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabFind.Location = new System.Drawing.Point(0, 0);
			this.tabFind.Name = "tabFind";
			this.tabFind.SelectedIndex = 0;
			this.tabFind.Size = new System.Drawing.Size(433, 101);
			this.tabFind.TabIndex = 0;
			// 
			// tpFind
			// 
			this.tpFind.Controls.Add(this.tableLayoutPanel1);
			this.tpFind.Location = new System.Drawing.Point(4, 4);
			this.tpFind.Name = "tpFind";
			this.tpFind.Padding = new System.Windows.Forms.Padding(3);
			this.tpFind.Size = new System.Drawing.Size(425, 72);
			this.tpFind.TabIndex = 0;
			this.tpFind.Text = "Find";
			this.tpFind.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Controls.Add(this.cbCmpSrc, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbOperator, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtMapFilter, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbFilter, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnFindOK, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnReset, 3, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(419, 66);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// cbCmpSrc
			// 
			this.cbCmpSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbCmpSrc.FormattingEnabled = true;
			this.cbCmpSrc.Location = new System.Drawing.Point(86, 4);
			this.cbCmpSrc.Name = "cbCmpSrc";
			this.cbCmpSrc.Size = new System.Drawing.Size(77, 23);
			this.cbCmpSrc.TabIndex = 1;
			// 
			// cbOperator
			// 
			this.cbOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbOperator.FormattingEnabled = true;
			this.cbOperator.Items.AddRange(new object[] {
            "LIKE",
            "=",
            "<=",
            ">=",
            "<",
            ">"});
			this.cbOperator.Location = new System.Drawing.Point(169, 4);
			this.cbOperator.Name = "cbOperator";
			this.cbOperator.Size = new System.Drawing.Size(77, 23);
			this.cbOperator.TabIndex = 2;
			// 
			// txtMapFilter
			// 
			this.txtMapFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.txtMapFilter, 2);
			this.txtMapFilter.Location = new System.Drawing.Point(252, 3);
			this.txtMapFilter.Name = "txtMapFilter";
			this.txtMapFilter.Size = new System.Drawing.Size(164, 25);
			this.txtMapFilter.TabIndex = 3;
			// 
			// lbFilter
			// 
			this.lbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFilter.AutoSize = true;
			this.lbFilter.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbFilter.Location = new System.Drawing.Point(3, 8);
			this.lbFilter.Name = "lbFilter";
			this.lbFilter.Size = new System.Drawing.Size(77, 15);
			this.lbFilter.TabIndex = 4;
			this.lbFilter.Text = "Filter:";
			// 
			// btnFindOK
			// 
			this.btnFindOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btnFindOK.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnFindOK.Location = new System.Drawing.Point(338, 35);
			this.btnFindOK.Name = "btnFindOK";
			this.btnFindOK.Size = new System.Drawing.Size(75, 28);
			this.btnFindOK.TabIndex = 0;
			this.btnFindOK.Text = "Find";
			this.btnFindOK.UseVisualStyleBackColor = true;
			this.btnFindOK.Click += new System.EventHandler(this.btnFindOK_Click);
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btnReset.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnReset.Location = new System.Drawing.Point(253, 35);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 28);
			this.btnReset.TabIndex = 7;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// tpFindString
			// 
			this.tpFindString.Controls.Add(this.tableLayoutPanel2);
			this.tpFindString.Location = new System.Drawing.Point(4, 4);
			this.tpFindString.Name = "tpFindString";
			this.tpFindString.Padding = new System.Windows.Forms.Padding(3);
			this.tpFindString.Size = new System.Drawing.Size(425, 72);
			this.tpFindString.TabIndex = 1;
			this.tpFindString.Text = "String Filter";
			this.tpFindString.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel2.Controls.Add(this.lbStringFilter, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.txtStringFilter, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnStringFind, 3, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(419, 66);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// lbStringFilter
			// 
			this.lbStringFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lbStringFilter.AutoSize = true;
			this.lbStringFilter.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbStringFilter.Location = new System.Drawing.Point(3, 9);
			this.lbStringFilter.Name = "lbStringFilter";
			this.lbStringFilter.Size = new System.Drawing.Size(77, 15);
			this.lbStringFilter.TabIndex = 0;
			this.lbStringFilter.Text = "Filter: ";
			// 
			// txtStringFilter
			// 
			this.txtStringFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.txtStringFilter, 3);
			this.txtStringFilter.Location = new System.Drawing.Point(86, 4);
			this.txtStringFilter.Name = "txtStringFilter";
			this.txtStringFilter.Size = new System.Drawing.Size(330, 25);
			this.txtStringFilter.TabIndex = 1;
			// 
			// btnStringFind
			// 
			this.btnStringFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btnStringFind.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnStringFind.Location = new System.Drawing.Point(296, 36);
			this.btnStringFind.Name = "btnStringFind";
			this.btnStringFind.Size = new System.Drawing.Size(75, 27);
			this.btnStringFind.TabIndex = 2;
			this.btnStringFind.Text = "Find";
			this.btnStringFind.UseVisualStyleBackColor = true;
			this.btnStringFind.Click += new System.EventHandler(this.btnStringFind_Click);
			// 
			// FindRows
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 101);
			this.Controls.Add(this.tabFind);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindRows";
			this.ShowIcon = false;
			this.Text = "FindRows";
			this.tabFind.ResumeLayout(false);
			this.tpFind.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tpFindString.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabFind;
		private System.Windows.Forms.TabPage tpFind;
		private System.Windows.Forms.TabPage tpFindString;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnFindOK;
		private System.Windows.Forms.ComboBox cbCmpSrc;
		private System.Windows.Forms.ComboBox cbOperator;
		private System.Windows.Forms.TextBox txtMapFilter;
		private System.Windows.Forms.Label lbFilter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lbStringFilter;
		private System.Windows.Forms.TextBox txtStringFilter;
		private System.Windows.Forms.Button btnStringFind;
		private System.Windows.Forms.Button btnReset;
	}
}