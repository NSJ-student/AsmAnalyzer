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
	public partial class LssAnalyzer : Form
	{
		public delegate void MakeSymbolTab(uint TargetAddress);
		public delegate bool IsTableExists(string Symbol);
		public event MakeSymbolTab DoMakeSymbolTab;
		enum LoadType{
			MAP_LOADER,
			LSS_LOADER
		};
		BackgroundWorker LoadWorker;
		BindingSource MapBindingSource;
		
		IsTableExists IsSymbolExist;
		
		public IsTableExists IsTableExist
		{
			set
			{
				IsSymbolExist = value;
			}
		}

		public LssAnalyzer(BackgroundWorker worker, BindingSource datasrc)
		{
			InitializeComponent();
			
			btnLoadLssFile.Enabled = false;

			MapBindingSource = datasrc;

			LoadWorker = worker;
			LoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MakeComponentsComplete);
		}

		// Source[10]
		// 0 index
		// 1 area
		// 2 memory
		// 3 symbol
		// 4 hexinst
		// 5 asciiinst
		// 6 param
		// 7 comment
		// 8 allstring
		// 9 element type
		
		/// <summary>
		/// map, lss 파일 불러오기 작업
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLoadLssFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "*.lss";
			dialog.Filter = "LSS File|*.lss|ALL File|*.*";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtLssFileName.Text = dialog.FileName;
				LoadWorker.RunWorkerAsync(dialog.FileName);
			}
		}
		private void btnLoadMapFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "*.map";
			dialog.Filter = "MAP File|*.map|ALL File|*.*";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtMapFileName.Text = dialog.FileName;
				LoadWorker.RunWorkerAsync(dialog.FileName);
			}
		}
		public void MakeComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			if ((LoadType)arg.Result == LoadType.MAP_LOADER)
			{
				dgvMapList.DataSource = null;
				dgvMapList.DataSource = MapBindingSource;
				dgvMapList.AutoGenerateColumns = true;
				dgvMapList.Columns["Memory"].DefaultCellStyle.Format = "X04";
				btnLoadLssFile.Enabled = true;
				MessageBox.Show("Load Map File Completed!");
			}
			if ((LoadType)arg.Result == LoadType.LSS_LOADER)
			{

				MessageBox.Show("Load Lss File Completed!");
			}
		}

		/// <summary>
		/// 폼이 닫힐 때 destroy가 아니라 hide로
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LssAnalyzer_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}
		
		/// <summary>
		/// Right Click Menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMapList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ContextMenu m = MakeContextMenu();

				m.Show(dgvMapList, new Point(e.X, e.Y));
			}
		}
		private ContextMenu MakeContextMenu()
		{
			ContextMenu m = new ContextMenu();

			MenuItem item1 = new MenuItem("Select Columns");
			MenuItem item2 = new MenuItem("Find");

			item1.Click += new EventHandler(dgvMapList_SelectColumn);
			item2.Click += new EventHandler(dgvMapList_FindRow);

			m.MenuItems.Add(item1);
			m.MenuItems.Add(item2);
			/*
			int currentMouseOverRow = dgvMapList.HitTest(e.X, e.Y).RowIndex;

			if (currentMouseOverRow >= 0)
			{
				m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
			}
			*/
			return m;
		}
		private void dgvMapList_SelectColumn(object sender, EventArgs e)
		{
			SelectColums select = SelectColums.CreateSelectColums(dgvMapList);
			if ((select != null) && (!select.Visible))
				select.Show();
		}
		private void dgvMapList_FindRow(object sender, EventArgs e)
		{
			FindRows find = FindRows.CreateFindRows(new FindRows.AdjustFilter(dgvMapList_Filter), dgvMapList);
			if ((find != null) && (!find.Visible))
				find.Show();
		}
		private bool dgvMapList_Filter(string filter)
		{
			try
			{
				MapBindingSource.Filter = filter;
				dgvMapList.DataSource = null;
				dgvMapList.DataSource = MapBindingSource;
				dgvMapList.BackgroundColor = SystemColors.ControlLightLight;
				dgvMapList.Columns["Memory"].DefaultCellStyle.Format = "X04";
				return true;
			}
			catch
			{
				dgvMapList.BackgroundColor = Color.Pink;
				return false;
			}
		}

		/// <summary>
		/// 더블클릭한 항목의 lss를 불러온다
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMapList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(dgvMapList.HitTest(e.X, e.Y).RowIndex >= 0)
			{
				if (dgvMapList.SelectedRows.Count == 0)
				{
					MessageBox.Show("No Symbol Selected!","Error", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
					return;
				}
				DataGridViewSelectedRowCollection test = dgvMapList.SelectedRows;
				int index = 0;

				foreach (DataGridViewRow item in test)
				{
					string ItemSymbol;
					uint ItemAddress;
					try
					{
						DataRowView view = (DataRowView)item.DataBoundItem;
						ItemSymbol = (string)view.Row.ItemArray[2];
						ItemAddress = (uint)view.Row.ItemArray[3];
					}
					catch
					{
						MapElements element = (MapElements)item.DataBoundItem;
						ItemSymbol = element.Symbol;
						ItemAddress = element.Memory;
					}

					if (ItemAddress == 0)
					{
						return;
					}

					if (IsSymbolExist(ItemSymbol) == true)
					{
						MessageBox.Show("<" + ItemSymbol + "> Already Exists!", "Duplicate Symbol",
							MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
						return;
					}
					
					DoMakeSymbolTab(ItemAddress);

					index++;
				}
			}
		}
	}
}
