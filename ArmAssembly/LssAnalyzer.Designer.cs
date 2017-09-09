namespace ArmAssembly
{
	partial class LssAnalyzer
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
			this.txtLssFileName = new System.Windows.Forms.TextBox();
			this.btnLoadLssFile = new System.Windows.Forms.Button();
			this.dgvMapList = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnLoadMapFile = new System.Windows.Forms.Button();
			this.txtMapFileName = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dgvMapList)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLssFileName
			// 
			this.txtLssFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.txtLssFileName, 3);
			this.txtLssFileName.Location = new System.Drawing.Point(471, 3);
			this.txtLssFileName.Name = "txtLssFileName";
			this.txtLssFileName.ReadOnly = true;
			this.txtLssFileName.Size = new System.Drawing.Size(307, 25);
			this.txtLssFileName.TabIndex = 0;
			// 
			// btnLoadLssFile
			// 
			this.btnLoadLssFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadLssFile.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnLoadLssFile.Location = new System.Drawing.Point(393, 3);
			this.btnLoadLssFile.Name = "btnLoadLssFile";
			this.btnLoadLssFile.Size = new System.Drawing.Size(72, 24);
			this.btnLoadLssFile.TabIndex = 1;
			this.btnLoadLssFile.Text = "LSS";
			this.btnLoadLssFile.UseVisualStyleBackColor = true;
			this.btnLoadLssFile.Click += new System.EventHandler(this.btnLoadLssFile_Click);
			// 
			// dgvMapList
			// 
			this.dgvMapList.AllowUserToOrderColumns = true;
			this.dgvMapList.AllowUserToResizeRows = false;
			this.dgvMapList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvMapList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dgvMapList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.tableLayoutPanel1.SetColumnSpan(this.dgvMapList, 6);
			this.dgvMapList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgvMapList.Location = new System.Drawing.Point(3, 33);
			this.dgvMapList.Name = "dgvMapList";
			this.dgvMapList.ReadOnly = true;
			this.dgvMapList.RowHeadersVisible = false;
			this.dgvMapList.RowTemplate.Height = 27;
			this.dgvMapList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvMapList.Size = new System.Drawing.Size(775, 377);
			this.dgvMapList.TabIndex = 3;
			this.dgvMapList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMapList_MouseClick);
			this.dgvMapList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvMapList_MouseDoubleClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.Controls.Add(this.dgvMapList, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnLoadMapFile, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnLoadLssFile, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtMapFileName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtLssFileName, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 413);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// btnLoadMapFile
			// 
			this.btnLoadMapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadMapFile.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnLoadMapFile.Location = new System.Drawing.Point(3, 3);
			this.btnLoadMapFile.Name = "btnLoadMapFile";
			this.btnLoadMapFile.Size = new System.Drawing.Size(72, 24);
			this.btnLoadMapFile.TabIndex = 4;
			this.btnLoadMapFile.Text = "MAP";
			this.btnLoadMapFile.UseVisualStyleBackColor = true;
			this.btnLoadMapFile.Click += new System.EventHandler(this.btnLoadMapFile_Click);
			// 
			// txtMapFileName
			// 
			this.txtMapFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMapFileName.Location = new System.Drawing.Point(81, 3);
			this.txtMapFileName.Name = "txtMapFileName";
			this.txtMapFileName.ReadOnly = true;
			this.txtMapFileName.Size = new System.Drawing.Size(306, 25);
			this.txtMapFileName.TabIndex = 5;
			// 
			// LssAnalyzer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 413);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LssAnalyzer";
			this.ShowIcon = false;
			this.Text = "LssAnalyzer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LssAnalyzer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dgvMapList)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtLssFileName;
		private System.Windows.Forms.Button btnLoadLssFile;
		private System.Windows.Forms.DataGridView dgvMapList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnLoadMapFile;
		private System.Windows.Forms.TextBox txtMapFileName;
	}
}