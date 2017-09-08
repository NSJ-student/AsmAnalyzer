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
	public partial class FindRows : Form
	{
		public delegate bool AdjustFilter(string Filter);
		AdjustFilter Filter;

		public FindRows(AdjustFilter FilterFunction, DataGridView refGrid)
		{
			InitializeComponent();

			Filter = FilterFunction;

			cbCmpSrc.Items.Clear();
			foreach (DataGridViewColumn item in refGrid.Columns)
			{
				cbCmpSrc.Items.Add(item.Name);
			}
			cbCmpSrc.Text = (string)cbCmpSrc.Items[0];
		}

		private void btnFindOK_Click(object sender, EventArgs e)
		{
			string strFilter;

			if(cbOperator.Text.Equals("LIKE"))
			{
				strFilter = "[" + (string)cbCmpSrc.SelectedItem + "]"
							+ " LIKE '%" + txtMapFilter.Text + "%'";
			}
			else
			{
				strFilter = (string)cbCmpSrc.SelectedItem
							+ " " + cbOperator.Text + " '" + txtMapFilter.Text + "'";
			}

			if (Filter(strFilter))
				txtMapFilter.BackColor = Color.White;
			else
				txtMapFilter.BackColor = Color.Pink;

			Close();
		}

		private void btnStringFind_Click(object sender, EventArgs e)
		{
			if (Filter(txtStringFilter.Text))
				txtMapFilter.BackColor = Color.White;
			else
				txtMapFilter.BackColor = Color.Pink;

			Close();
		}

		private void cbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			cbOperator.Text = (string)cbOperator.Items[0];
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			if (Filter(""))
				txtMapFilter.BackColor = Color.White;
			else
				txtMapFilter.BackColor = Color.Pink;

			Close();
		}
	}
}
