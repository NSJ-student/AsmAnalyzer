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
		BackgroundWorker LssLoadWorker;
		BackgroundWorker MapLoadWorker;
		LssContainer LssList;
		MapContainer MapList;
		public LssAnalyzer()
		{
			InitializeComponent();

			pbLssLoadRate.Visible = false;
			btnLoadLssFile.Enabled = false;

			LssLoadWorker = new BackgroundWorker();
			LssLoadWorker.WorkerReportsProgress = true;
			LssLoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AddLssComponentsComplete);
			LssLoadWorker.DoWork += new DoWorkEventHandler(AddLssComponents);
			LssLoadWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);

			MapLoadWorker = new BackgroundWorker();
			MapLoadWorker.WorkerReportsProgress = true;
			MapLoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AddMapComponentsComplete);
			MapLoadWorker.DoWork += new DoWorkEventHandler(AddMapComponents);
			MapLoadWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);
		}

		private void AddLssComponents(object obj, DoWorkEventArgs arg)
		{
//			LssContainer lss = (LssContainer)arg.Argument;
			LssList = new LssContainer(txtLssFileName.Text, new LssContainer.UpdateProgress(LssLoadWorker.ReportProgress));
		}

		public void UpdateProgressBar(object obj, ProgressChangedEventArgs arg)
		{
			pbLssLoadRate.Value = arg.ProgressPercentage;
		}
		public void AddLssComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			//dgvLssList.DataSource = null;
			//dgvLssList.DataSource = LssList.ElementList;
			pbLssLoadRate.Visible = false;
			MessageBox.Show("Load Lss File Completed!");
		}
		private void btnLoadLssFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "*.lss";
			dialog.Filter = "LSS File|*.lss|ALL File|*.*";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				pbLssLoadRate.Visible = true;
				txtLssFileName.Text = dialog.FileName;
				LssLoadWorker.RunWorkerAsync(LssList);
			}
		}
		private void AddMapComponents(object obj, DoWorkEventArgs arg)
		{
//			MapContainer lss = (MapContainer)arg.Argument;
			MapList = new MapContainer(txtMapFileName.Text, new MapContainer.UpdateProgress(MapLoadWorker.ReportProgress));
		}
		public void AddMapComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			dgvMapList.DataSource = null;
			dgvMapList.DataSource = MapList.ElementList;
			pbLssLoadRate.Visible = false;
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
				pbLssLoadRate.Visible = true;
				txtMapFileName.Text = dialog.FileName;
				MapLoadWorker.RunWorkerAsync(MapList);
			}
		}

		private void btnOpenMatchSymbol_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection test = dgvMapList.SelectedRows;
			string[] symbols = new string[test.Count];
			int index = 0;

			foreach (DataGridViewRow item in test)
			{
				MapElements element = (MapElements)item.DataBoundItem;
				if(Convert.ToInt32(element.Address, 16) == 0)
				{
					return;
				}
				symbols[index] = element.Symbol;
				
				int SymbolIndex = LssList.SymbolList.FindIndex(x => x.Memory.Equals(element.Address, StringComparison.OrdinalIgnoreCase));
				int StartIndex, EndIndex;
				StartIndex = LssList.ElementList.FindIndex(x => 
						!string.IsNullOrEmpty(x.Memory) && x.Memory.Equals(LssList.SymbolList[SymbolIndex].Memory, StringComparison.OrdinalIgnoreCase));
				if (SymbolIndex != LssList.SymbolList.Count - 1)
				{
					EndIndex = LssList.ElementList.FindIndex(x =>
						!string.IsNullOrEmpty(x.Memory) && x.Memory.Equals(LssList.SymbolList[SymbolIndex+1].Memory, StringComparison.OrdinalIgnoreCase));
				}
				else
				{
					EndIndex = LssList.ElementList.Count;
				}

				ViewSymbolAsm TestWindow = new ViewSymbolAsm(LssList, StartIndex, EndIndex);
				TestWindow.Show();

				index++;
			}
		}
	}
}
