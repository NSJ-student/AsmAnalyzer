using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crom.Controls;

namespace ArmAssembly
{
	public partial class Form1 : Form
	{
		Visualizer Vis;
		ViewSymbolAsm Sym;
		LssAnalyzer Analyzer;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Sym = new ViewSymbolAsm();
			Sym.MdiParent = this;
			Sym.VisibleChanged += new EventHandler(FormVisibleStateChanged);

			Analyzer = new LssAnalyzer();
			Analyzer.MdiParent = this;
			Analyzer.VisibleChanged += new EventHandler(FormVisibleStateChanged);
			Analyzer.AddSymbolTable = new LssAnalyzer.AddSymbolASM(Sym.AddAsmTab);
			Analyzer.IsTableExist = new LssAnalyzer.IsTableExists(Sym.IsTableExist);
		}

		private void FormVisibleStateChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem item;
			if (sender.Equals(Analyzer))
			{
				item = (ToolStripMenuItem)tsmiSymbolLoader;
			}
			else if (sender.Equals(Sym))
			{
				item = (ToolStripMenuItem)tsmiAsmAnalyzer;
			}
			else
			{
				return;
			}

			Form form = (Form)sender;
			if(form.Visible)
			{
				item.Checked = true;
			}
			else
			{
				item.Checked = false;
			}
		}

		private void tsmiSymbolLoader_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)tsmiSymbolLoader;

			if(item.Checked)
			{
				Analyzer.Hide();
			}
			else
			{
				Analyzer.Show();
			}
		}

		private void tsmiAsmAnalyzer_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)tsmiAsmAnalyzer;

			if (item.Checked)
			{
				Sym.Hide();
			}
			else
			{
				Sym.Show();
			}
		}
		
		private void tsmiAnalyzeRun_Click(object sender, EventArgs e)
		{
			if (Sym.ResetPointer())
			{
				Vis = new Visualizer(Sym.GetCurrnetSymbol());
				Vis.MdiParent = this;
				Vis.Show();

				tsmiAnalyzeRun.Enabled = false;
				tsmiNext.Enabled = true;
			}
		}

		private void tsmiNext_Click(object sender, EventArgs e)
		{
			if(Sym.ToNextRow())
			{
				Vis.SetInput(Sym.GetCurrentRow());
			}
			else
			{
				Vis.Dispose();
				tsmiAnalyzeRun.Enabled = true;
				tsmiNext.Enabled = false;
			}
		}
	}
}
