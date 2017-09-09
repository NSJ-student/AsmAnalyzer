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
		AsmInterpreter.GetMatchedRow GetMemRow;
		RegisterControl[] RegArray;
		public Visualizer(string SymbolName, AsmInterpreter.GetMatchedRow callback)
		{
			InitializeComponent();

			GetMemRow = callback;

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

		public void SetInput(object[] input)
		{
			if(input[1].Equals("assembly"))
			{
				try
				{
					string MemAddr = (string)input[2];
					string Instruction = (string)input[5];
					string Parameter = (string)input[6];
					string[] Par = AsmInterpreter.SplitParam(Parameter);
					/*
					for(int cnt=0; cnt<Par.Length; cnt++)
					{
						AsmInterpreter.ParamType type = AsmInterpreter.ParamType.None;
						RegArray[cnt].txtValue.Text = AsmInterpreter.ParseToHexString(MemAddr, Par[cnt], RegArray, ref type);
						RegArray[15].txtValue.Text = type.ToString();
					}
					*/

					AsmInterpreter.ParamType type = AsmInterpreter.ParamType.None;
					txtInst.Text = Instruction;
					if (Par.Length > 0)
					{
						txtParam1.Text = Par[0];
						txtParam1ToHex.Text = AsmInterpreter.ParseToHexString(MemAddr, Par[0], RegArray, ref type);
					}
					if (Par.Length > 1)
					{
						txtParam2.Text = Par[1];
						txtParam2ToHex.Text = AsmInterpreter.ParseToHexString(MemAddr, Par[1], RegArray, ref type);
					}
					if (Par.Length > 2)
					{
						txtParam3.Text = Par[2];
						txtParam3ToHex.Text = AsmInterpreter.ParseToHexString(MemAddr, Par[2], RegArray, ref type);
					}
					txtInputAll.Text = (string)input[8];

					AsmInterpreter.ParseInstruction(input, RegArray, GetMemRow);
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
				string SetStr;
				int fromNum;
				string toNum;

				if (ClickedButton.Text.Equals("HEX"))
				{
					SetStr = "DEC";
					fromNum = 16;
					toNum = "";
				}
				else
				{
					SetStr = "HEX";
					fromNum = 10;
					toNum = "X";
				}

				for (int cnt = 0; cnt < 17; cnt++)
				{
					if ((RegArray[cnt].txtValue != null)&&(RegArray[cnt].txtValue.Text.Length > 0))
					{
						string orgStr = RegArray[cnt].txtValue.Text;
						uint orgValue = Convert.ToUInt32(orgStr, fromNum);
						RegArray[cnt].txtValue.Text = orgValue.ToString(toNum);
					}
					RegArray[cnt].btnFormat.Text = SetStr;
				}
			}
			else
			{
				string SetStr;
				int fromNum;
				string toNum;

				if (ClickedButton.Text.Equals("HEX"))
				{
					SetStr = "DEC";
					fromNum = 16;
					toNum = "";
				}
				else
				{
					SetStr = "HEX";
					fromNum = 10;
					toNum = "X";
				}

				ClickedButton.Text = SetStr;
				for (int cnt = 0; cnt < 16; cnt++)
				{
					if (RegArray[cnt].btnFormat.Equals(ClickedButton))
					{
						string orgStr = RegArray[cnt].txtValue.Text;
						uint orgValue = Convert.ToUInt32(orgStr, fromNum);
						RegArray[cnt].txtValue.Text = orgValue.ToString(toNum);
						break;
					}
				}
			}
		}

		private void pbRegClear_Click(object sender, EventArgs e)
		{
			foreach(RegisterControl item in RegArray)
			{
				item.txtValue.Clear();
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
