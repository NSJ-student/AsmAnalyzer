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
		public delegate void ProgressBarUpdate(object obj, ProgressChangedEventArgs arg);
		public delegate int AddSymbolASM(LssContainer Source, int StartIndex, int EndIndex);
		public delegate bool IsTableExists(string Symbol);
		BackgroundWorker LssLoadWorker;
		BackgroundWorker MapLoadWorker;
		BindingSource MapBindingSource;
		LssContainer LssList;
		MapContainer MapList;

		ProgressBarUpdate UpdateProgressBar;
		AddSymbolASM AddSymbol;
		IsTableExists IsSymbolExist;

		public AddSymbolASM AddSymbolTable
		{
			set
			{
				AddSymbol = value;
			}
		}
		public IsTableExists IsTableExist
		{
			set
			{
				IsSymbolExist = value;
			}
		}

		public LssAnalyzer(ProgressBarUpdate progress = null)
		{
			InitializeComponent();
			
			btnLoadLssFile.Enabled = false;

			MapBindingSource = new BindingSource();

			LssLoadWorker = new BackgroundWorker();
			LssLoadWorker.WorkerReportsProgress = true;
			LssLoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AddLssComponentsComplete);
			LssLoadWorker.DoWork += new DoWorkEventHandler(AddLssComponents);

			MapLoadWorker = new BackgroundWorker();
			MapLoadWorker.WorkerReportsProgress = true;
			MapLoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AddMapComponentsComplete);
			MapLoadWorker.DoWork += new DoWorkEventHandler(AddMapComponents);

			UpdateProgressBar = progress;
			if (progress != null)
			{
				LssLoadWorker.ProgressChanged += new ProgressChangedEventHandler(progress);
				MapLoadWorker.ProgressChanged += new ProgressChangedEventHandler(progress);
			}
		}

		private void AddLssComponents(object obj, DoWorkEventArgs arg)
		{
			//			LssContainer lss = (LssContainer)arg.Argument;
			if (UpdateProgressBar != null)
			{
				LssList = new LssContainer(txtLssFileName.Text, new LssContainer.UpdateProgress(LssLoadWorker.ReportProgress));
			}
			else
			{
				LssList = new LssContainer(txtLssFileName.Text);
			}
		}

		public void AddLssComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			//dgvLssList.DataSource = null;
			//dgvLssList.DataSource = LssList.ElementList;
			MessageBox.Show("Load Lss File Completed!");
		}
		private void btnLoadLssFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "*.lss";
			dialog.Filter = "LSS File|*.lss|ALL File|*.*";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtLssFileName.Text = dialog.FileName;
				LssLoadWorker.RunWorkerAsync(LssList);
			}
		}
		private void AddMapComponents(object obj, DoWorkEventArgs arg)
		{
			//			MapContainer lss = (MapContainer)arg.Argument;
			if (UpdateProgressBar != null)
			{
				MapList = new MapContainer(txtMapFileName.Text, new MapContainer.UpdateProgress(MapLoadWorker.ReportProgress));
			}
			else
			{
				MapList = new MapContainer(txtMapFileName.Text);
			}
		}
		public void AddMapComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			MapBindingSource.DataSource = MapList.MapDataSet;
			MapBindingSource.DataMember = MapList.MapDataSet.Tables[0].TableName;
			dgvMapList.DataSource = null;
			dgvMapList.DataSource = MapBindingSource;
			dgvMapList.AutoGenerateColumns = true;
			btnLoadLssFile.Enabled = true;
			MessageBox.Show("Load Map File Completed!");
		}
		private void btnLoadMapFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "*.map";
			dialog.Filter = "MAP File|*.map|ALL File|*.*";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtMapFileName.Text = dialog.FileName;
				MapLoadWorker.RunWorkerAsync(MapList);
			}
		}
		
		private void dgvMapList_DoubleClick(object sender, EventArgs e)
		{
			if (dgvMapList.SelectedRows.Count == 0)
			{
				MessageBox.Show("No Symbol Selected!");
				return;
			}
			DataGridViewSelectedRowCollection test = dgvMapList.SelectedRows;
			string[] symbols = new string[test.Count];
			int index = 0;

			foreach (DataGridViewRow item in test)
			{
				string ItemSymbol;
				string ItemAddress;
				try
				{
					DataRowView view = (DataRowView)item.DataBoundItem;
					ItemSymbol = (string)view.Row.ItemArray[2];
					ItemAddress = (string)view.Row.ItemArray[3];
				}
				catch
				{
					MapElements element = (MapElements)item.DataBoundItem;
					ItemSymbol = element.Symbol;
					ItemAddress = element.Address;
				}

				if (Convert.ToInt32(ItemAddress, 16) == 0)
				{
					return;
				}

				if (IsSymbolExist(ItemSymbol) == true)
				{
					MessageBox.Show("<" + ItemSymbol + "> Already Exists!");
					return;
				}
				symbols[index] = ItemSymbol;

				try
				{
					int SymbolIndex = LssList.SymbolList.FindIndex(x => x.Memory.Equals(ItemAddress, StringComparison.OrdinalIgnoreCase));
					if(SymbolIndex < 0)
					{
						throw new NullReferenceException();
					}
					int StartIndex, EndIndex;
					StartIndex = LssList.ElementList.FindIndex(x =>
							!string.IsNullOrEmpty(x.Memory) && x.Memory.Equals(LssList.SymbolList[SymbolIndex].Memory, StringComparison.OrdinalIgnoreCase));
					if (SymbolIndex != LssList.SymbolList.Count - 1)
					{
						EndIndex = LssList.ElementList.FindIndex(x =>
							!string.IsNullOrEmpty(x.Memory) && x.Memory.Equals(LssList.SymbolList[SymbolIndex + 1].Memory, StringComparison.OrdinalIgnoreCase));
					}
					else
					{
						EndIndex = LssList.ElementList.Count;
					}
					
					AddSymbol?.Invoke(LssList, StartIndex, EndIndex);
				}
				catch (NullReferenceException arg)
				{
					MessageBox.Show("No Reference!");
					return;
				}

				index++;
			}
		}
		
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
				return true;
			}
			catch
			{
				dgvMapList.BackgroundColor = Color.Pink;
				return false;
			}
		}

	}
}
