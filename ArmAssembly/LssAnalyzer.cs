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
		enum LoadType{
			MAP_LOADER,
			LSS_LOADER
		};
		BackgroundWorker LoadWorker;
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

			LoadWorker = new BackgroundWorker();
			LoadWorker.WorkerReportsProgress = true;
			LoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MakeComponentsComplete);
			LoadWorker.DoWork += new DoWorkEventHandler(MakeComponents);
			
			UpdateProgressBar = progress;
			if (progress != null)
			{
				LoadWorker.ProgressChanged += new ProgressChangedEventHandler(progress);
			}
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
		/// lss 데이터 중에서 특정 메모리관련 정보를 object[]로 리턴
		/// </summary>
		/// <param name="memory"></param>
		/// <returns></returns>
		public object[] GetMemoryRow(string memory)
		{
			LssElements data = LssList.ElementList.Find(x => (x.Memory != null) && (x.Memory.Equals(memory)));

			if (data != null)
			{
				object[] retValue = new object[10];
				retValue[0] = data.Index; retValue[1] = data.Area;
				retValue[2] = data.Memory; retValue[3] = data.Symbol;
				retValue[4] = data.HexInstruction; retValue[5] = data.Instruction;
				retValue[6] = data.Parameter; retValue[7] = data.CommentValue;
				retValue[8] = data.StringLine; retValue[9] = data.ElementType;
				return retValue;
			}
			else
				return null;
		}

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
				LoadWorker.RunWorkerAsync(LoadType.LSS_LOADER);
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
				LoadWorker.RunWorkerAsync(LoadType.MAP_LOADER);
			}
		}
		private void MakeComponents(object obj, DoWorkEventArgs arg)
		{
			if (UpdateProgressBar != null)
			{
				if ((LoadType)arg.Argument == LoadType.MAP_LOADER)
					MapList = new MapContainer(txtMapFileName.Text, new MapContainer.UpdateProgress(LoadWorker.ReportProgress));
				if ((LoadType)arg.Argument == LoadType.LSS_LOADER)
					LssList = new LssContainer(txtLssFileName.Text, new LssContainer.UpdateProgress(LoadWorker.ReportProgress));
			}
			else
			{
				if ((LoadType)arg.Argument == LoadType.MAP_LOADER)
					MapList = new MapContainer(txtMapFileName.Text);
				if ((LoadType)arg.Argument == LoadType.LSS_LOADER)
					LssList = new LssContainer(txtLssFileName.Text);
			}
			arg.Result = arg.Argument;
		}
		public void MakeComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			if ((LoadType)arg.Result == LoadType.MAP_LOADER)
			{
				MapBindingSource.DataSource = MapList.MapDataSet;
				MapBindingSource.DataMember = MapList.MapDataSet.Tables[0].TableName;
				dgvMapList.DataSource = null;
				dgvMapList.DataSource = MapBindingSource;
				dgvMapList.AutoGenerateColumns = true;
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
						MessageBox.Show("<" + ItemSymbol + "> Already Exists!", "Duplicate Symbol",
							MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
						return;
					}
					symbols[index] = ItemSymbol;

					try
					{
						int SymbolIndex = LssList.SymbolList.FindIndex(x => x.Memory.Equals(ItemAddress, StringComparison.OrdinalIgnoreCase));
						if (SymbolIndex < 0)
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
						MessageBox.Show("Matching Symbol doesn't exist", "No Reference!", 
							MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
						return;
					}

					index++;
				}
			}
		}
	}
}
