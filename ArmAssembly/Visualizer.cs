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
	public partial class Visualizer : Form
	{
		RegisterControl[] RegArray;
		public Visualizer(string SymbolName)
		{
			InitializeComponent();
			RegArray = new RegisterControl[17];
			RegArray[0] = new RegisterControl(btnFormatR0, txtR0);
			RegArray[1] = new RegisterControl(btnFormatR1, txtR1);
			RegArray[2] = new RegisterControl(btnFormatR2, txtR2);
			RegArray[3] = new RegisterControl(btnFormatR3, txtR3);
			RegArray[4] = new RegisterControl(btnFormatR4, txtR4);
			RegArray[5] = new RegisterControl(btnFormatR5, txtR5);
			RegArray[6] = new RegisterControl(btnFormatR6, txtR6);
			RegArray[7] = new RegisterControl(btnFormatR7, txtR7);
			RegArray[8] = new RegisterControl(btnFormatR8, txtR8);
			RegArray[9] = new RegisterControl(btnFormatR9, txtR9);
			RegArray[10] = new RegisterControl(btnFormatR10, txtR10);
			RegArray[11] = new RegisterControl(btnFormatR11, txtR11);
			RegArray[12] = new RegisterControl(btnFormatR12, txtR12);
			RegArray[13] = new RegisterControl(btnFormatR13, txtR13);
			RegArray[14] = new RegisterControl(btnFormatR14, txtR14);
			RegArray[15] = new RegisterControl(btnFormatR15, txtR15);
			RegArray[16] = new RegisterControl(btnFormatAll, null);

			this.Text += " - " + SymbolName;
		}

		public void SetInput(DataGridViewRow row)
		{
			DataRowView view = (DataRowView)row.DataBoundItem;
			object[] input = view.Row.ItemArray;

			if(input[1].Equals("assembly"))
			{
				try
				{
					txtInst.Text = (string)input[5];
					string[] Par = AsmInterpreter.SplitParam((string)input[6]);
					for(int cnt=0; cnt<Par.Length; cnt++)
					{
						AsmInterpreter.ParamType type = AsmInterpreter.ParamType.None;
						RegArray[cnt].txtValue.Text = AsmInterpreter.ParseToHex(Par[cnt], RegArray, ref type);
						RegArray[15].txtValue.Text = type.ToString();
					}

					txtInputAll.Text = (string)input[8];
				}
				catch
				{
					txtInst.Clear();
					txtParam1.Clear();
					txtParam2.Clear();
					txtInputAll.Text = "Error";
				}
			}
			else
			{
				txtInst.Clear();
				txtParam1.Clear();
				txtParam2.Clear();
				txtInputAll.Text = (string)input[8];
			}

		}

		private void ChangeRegisterFormat(object sender, EventArgs e)
		{
			Button ClickedButton = (Button)sender;

			if(ClickedButton.Equals(btnFormatAll))
			{
				String SetStr;

				if (ClickedButton.Text.Equals("HEX"))
				{
					SetStr = "DEC";
				}
				else
				{
					SetStr = "HEX";
				}

				for (int cnt=0; cnt<17; cnt++)
				{
					RegArray[cnt].btnFormat.Text = SetStr;
				}
			}
			else
			{
				if (ClickedButton.Text.Equals("HEX"))
				{
					ClickedButton.Text = "DEC";
				}
				else
				{
					ClickedButton.Text = "HEX";
				}
			}
		}
	}
	public class RegisterControl
	{
		public Button btnFormat;
		public TextBox txtValue;

		public RegisterControl(Button btn, TextBox text)
		{
			btnFormat = btn;
			txtValue = text;
		}

		public void ToggleFormat()
		{
			if (btnFormat.Text.Equals("HEX"))
			{
				btnFormat.Text = "DEC";
			}
			else
			{
				btnFormat.Text = "HEX";
			}
		}
	}
}
