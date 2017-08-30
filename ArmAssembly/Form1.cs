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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Visualizer Vis = new Visualizer();
			Vis.MdiParent = this;
			Vis.Show();

			LssAnalyzer Analyzer = new LssAnalyzer();
			Analyzer.MdiParent = this;
			Analyzer.Show();
		}
	}
}
