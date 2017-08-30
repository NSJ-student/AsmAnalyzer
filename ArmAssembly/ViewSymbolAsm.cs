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
		LssElements[] BaseAsm;
		public ViewSymbolAsm()
		{
			InitializeComponent();
		}
		public ViewSymbolAsm(LssContainer Source, int StartIndex, int EndIndex)
		{
			InitializeComponent();

			BaseAsm = new LssElements[EndIndex - StartIndex];

			for(int cnt=0; cnt < BaseAsm.Count<LssElements>(); cnt++)
			{
				BaseAsm[cnt] = Source.ElementList[StartIndex + cnt];
			}

			dgvSymbolAsm.DataSource = null;
			dgvSymbolAsm.DataSource = BaseAsm;
		}
	}
}
