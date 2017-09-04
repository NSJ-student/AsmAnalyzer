namespace ArmAssembly
{
	partial class SelectColums
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
			this.clbShowList = new System.Windows.Forms.CheckedListBox();
			this.btnSelect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// clbShowList
			// 
			this.clbShowList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.clbShowList.FormattingEnabled = true;
			this.clbShowList.Location = new System.Drawing.Point(12, 12);
			this.clbShowList.Name = "clbShowList";
			this.clbShowList.Size = new System.Drawing.Size(216, 124);
			this.clbShowList.TabIndex = 0;
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnSelect.Location = new System.Drawing.Point(91, 142);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(72, 32);
			this.btnSelect.TabIndex = 1;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// SelectColums
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(240, 179);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.clbShowList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectColums";
			this.ShowIcon = false;
			this.Text = "SelectColums";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckedListBox clbShowList;
		private System.Windows.Forms.Button btnSelect;
	}
}