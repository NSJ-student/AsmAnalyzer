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
			this.dgvSymbolAsm = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgvSymbolAsm)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvSymbolAsm
			// 
			this.dgvSymbolAsm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSymbolAsm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvSymbolAsm.Location = new System.Drawing.Point(0, 0);
			this.dgvSymbolAsm.Name = "dgvSymbolAsm";
			this.dgvSymbolAsm.RowTemplate.Height = 27;
			this.dgvSymbolAsm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvSymbolAsm.Size = new System.Drawing.Size(567, 384);
			this.dgvSymbolAsm.TabIndex = 0;
			// 
			// ViewSymbolAsm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 384);
			this.Controls.Add(this.dgvSymbolAsm);
			this.Name = "ViewSymbolAsm";
			this.ShowIcon = false;
			this.Text = "ViewSymbolAsm";
			((System.ComponentModel.ISupportInitialize)(this.dgvSymbolAsm)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvSymbolAsm;
	}
}