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
	public partial class ViewSymbolAsm : Form
	{
		List<AsmDataGridView> ControlList;
		public ViewSymbolAsm()
		{
			InitializeComponent();
			ControlList = new List<AsmDataGridView>();
		}

		/// <summary>
		/// 입력된 lss정보로 새로운 탭을 만들어 추가
		/// </summary>
		/// <param name="Source"></param>
		/// <param name="StartIndex"></param>
		/// <param name="EndIndex"></param>
		/// <returns></returns>
		public int AddAsmTab(LssContainer Source, int StartIndex, int EndIndex)
		{
			TabPage newtab  = new TabPage();
			tabSymbolAsm.Controls.Add(newtab);
			tabSymbolAsm.SelectedTab = newtab;
			
			AsmDataGridView control = new AsmDataGridView(newtab, Source, StartIndex, EndIndex);
			ControlList.Add(control);

			this.tabSymbolAsm.SuspendLayout();

			return 0;
		}
		/// <summary>
		/// 현재 선택된 탭의 심볼 이름을 반환
		/// </summary>
		/// <returns></returns>
		public string GetCurrnetSymbol()
		{
			if(tabSymbolAsm.SelectedTab != null)
			{
				return tabSymbolAsm.SelectedTab.Name;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// 테이블 가장 첫번째 행을 선택
		/// </summary>
		/// <returns></returns>
		public bool ResetPointer()
		{
			DataGridView data;
			try
			{
				data = ControlList.Find(x => x.Tab.Equals(tabSymbolAsm.SelectedTab)).GridViewControl;
			}
			catch
			{
				return false;
			}

			if (data.Rows.Count == 0)
				return false;

			data.Rows[0].Selected = true;
			return true;
		}
		/// <summary>
		/// 다음 행으로 이동
		/// </summary>
		/// <returns></returns>
		public bool ToNextRow()
		{
			DataGridView data = ControlList.Find(x => x.Tab.Equals(tabSymbolAsm.SelectedTab)).GridViewControl;

			if ((data.Rows.Count - 2) == data.SelectedRows[0].Index)
				return false;

			data.Rows[data.SelectedRows[0].Index + 1].Selected = true;
			data.CurrentCell = data.SelectedRows[0].Cells[0];

			return true;
		}
		/// <summary>
		/// 현재 선택된 행의 데이터를 object[]로 반환
		/// </summary>
		/// <returns></returns>
		public object[] GetCurrentLssRow()
		{
			DataGridView data = ControlList.Find(x => x.Tab.Equals(tabSymbolAsm.SelectedTab)).GridViewControl;
			DataRowView view = (DataRowView)data.SelectedRows[0].DataBoundItem;
			return view.Row.ItemArray;
		}

		/// <summary>
		/// Symbol과 일치하는 탭이 존재하는지 확인
		/// </summary>
		/// <param name="Symbol"></param>
		/// <returns></returns>
		public bool IsTableExist(string Symbol)
		{
			try
			{
				AsmDataGridView item =  ControlList.Find(x => x.Name.Equals(Symbol, StringComparison.OrdinalIgnoreCase));
				if (item == null)
					return false;
				else
					return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 폼이 닫힐 때 destroy가 아니라 hide로
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ViewSymbolAsm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		/// <summary>
		/// 생성된 탭을 닫기 위한 우클릭 메뉴 생성 및 동작
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabSymbolAsm_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (tabSymbolAsm.TabPages.Count == 0)
					return;

				ContextMenu m = new ContextMenu();
				MenuItem item1 = new MenuItem("Close All");
				item1.Click += new EventHandler(tabSymbolAsm_CloseAll);
				m.MenuItems.Add(item1);

				if (tabSymbolAsm.GetTabRect(tabSymbolAsm.SelectedIndex).Contains(e.Location))
				{
					MenuItem item2 = new MenuItem("Close");
					item2.Click += new EventHandler(tabSymbolAsm_Close);
					m.MenuItems.Add(item2);
				}

				m.Show(tabSymbolAsm, e.Location);
			}
		}
		private void tabSymbolAsm_CloseAll(object sender, EventArgs e)
		{
			foreach(AsmDataGridView item in ControlList)
			{
				item.Dispose();
			}
			ControlList.Clear();
		}
		private void tabSymbolAsm_Close(object sender, EventArgs e)
		{
			AsmDataGridView item = ControlList.Find(x => x.Tab.Equals(tabSymbolAsm.SelectedTab));
			item.Dispose();
			ControlList.Remove(item);
		}
	}
	public class AsmDataGridView : IDisposable
	{
		static int RefNumber = 0;
		string SymbolName;
		TabPage tabContainer;
		DataGridView ldgvData;
		BindingSource lssBindingSource;
		DataSet lssDataSet;

		public TabPage Tab
		{
			get
			{
				return tabContainer;
			}
		}
		public DataGridView GridViewControl
		{
			get
			{
				return ldgvData;
			}
		}
		public string Name
		{
			get
			{
				return SymbolName;
			}
		}

		public AsmDataGridView(TabPage newtab, LssContainer Source, int StartIndex, int EndIndex)
		{
			SymbolName = Source.ElementList[StartIndex].Symbol;

			LssElements[] BaseAsm = new LssElements[EndIndex - StartIndex];
			ldgvData = new DataGridView();
			tabContainer = newtab;
			
			tabContainer.Controls.Add(ldgvData);
			tabContainer.Location = new System.Drawing.Point(4, 25);
			tabContainer.Name = Source.ElementList[StartIndex].Symbol;
			tabContainer.Size = new Size(559, 355);
			tabContainer.TabIndex = RefNumber;
			tabContainer.Text = Source.ElementList[StartIndex].Symbol;
			tabContainer.Show();

			((System.ComponentModel.ISupportInitialize)(this.ldgvData)).BeginInit();
			this.ldgvData.AllowUserToOrderColumns = true;
			this.ldgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ldgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ldgvData.Location = new System.Drawing.Point(3, 63);
			this.ldgvData.Name = "ldgvData";
			this.ldgvData.RowTemplate.Height = 20;
			this.ldgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ldgvData.TabIndex = 3;
			this.ldgvData.Dock = DockStyle.Fill;
			this.ldgvData.RowHeadersVisible = false;
			this.ldgvData.AllowUserToResizeRows = false;
			this.ldgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.ldgvData.MultiSelect = false;
			this.ldgvData.MouseClick += new MouseEventHandler(ldgvData_MouseClick);
			((System.ComponentModel.ISupportInitialize)(this.ldgvData)).EndInit();

			for (int cnt = 0; cnt < BaseAsm.Count<LssElements>(); cnt++)
			{
				BaseAsm[cnt] = Source.ElementList[StartIndex + cnt];
			}

			lssDataSet = new DataSet();
			lssDataSet.Tables.Add(UserConvert.ToDataTable(BaseAsm, "LssTable" + RefNumber.ToString()));

			lssBindingSource = new BindingSource();
			lssBindingSource.DataSource = lssDataSet;
			lssBindingSource.DataMember = lssDataSet.Tables[0].TableName;
			
			ldgvData.DataSource = null;
			ldgvData.DataSource = lssBindingSource;
			ldgvData.Columns["Memory"].DefaultCellStyle.Format = "X04";

			RefNumber++;
		}

		~AsmDataGridView()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			tabContainer.Dispose();
			ldgvData.Dispose();
			lssBindingSource.Dispose();
			lssDataSet.Dispose();
		}

		/// <summary>
		/// Right Click Menu on DataGridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ldgvData_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ContextMenu m = MakeContextMenu();

				m.Show(ldgvData, new Point(e.X, e.Y));
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
			SelectColums select = SelectColums.CreateSelectColums(ldgvData);
			if ((select != null) && (!select.Visible))
				select.Show();
		}
		private void dgvMapList_FindRow(object sender, EventArgs e)
		{
			FindRows find = FindRows.CreateFindRows(new FindRows.AdjustFilter(dgvMapList_Filter), ldgvData);
			if ((find != null) && (!find.Visible))
				find.Show();
		}
		private bool dgvMapList_Filter(string filter)
		{
			try
			{
				lssBindingSource.Filter = filter;
				ldgvData.DataSource = null;
				ldgvData.DataSource = lssBindingSource;
				ldgvData.Columns["Memory"].DefaultCellStyle.Format = "X04";
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
