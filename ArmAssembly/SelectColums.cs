using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmAssembly
{
	public partial class SelectColums : Form
	{
		public static SelectColums Instance;
		DataGridView refGrid;

		public static SelectColums CreateSelectColums(DataGridView grid)
		{
			if (Instance == null)
			{
				Instance = new SelectColums(grid);
				return Instance;
			}
			else
			{
				if (!Instance.Visible)
				{
					Instance.Dispose();
					Instance = new SelectColums(grid);
					return Instance;
				}
				else
					return null;
			}
		}
		private SelectColums(DataGridView grid)
		{
			InitializeComponent();

			refGrid = grid;
			clbShowList.Items.Clear();
			foreach (DataGridViewColumn item in refGrid.Columns)
			{
				if(item.Visible)
				{
					clbShowList.Items.Add(item.Name, true);
				}
				else
				{
					clbShowList.Items.Add(item.Name, false);
				}
			}
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			for (int index = 0; index < clbShowList.Items.Count; index++)
			{
				if (clbShowList.GetItemChecked(index))
				{
					refGrid.Columns[(string)clbShowList.Items[index]].Visible = true;
				}
				else
				{
					refGrid.Columns[(string)clbShowList.Items[index]].Visible = false;
				}
			}

			Instance = null;
			//Close();
			Dispose();
		}

		private void SelectColums_FormClosed(object sender, FormClosedEventArgs e)
		{
			Instance = null;
			//Close();
			Dispose();
		}
	}
}
