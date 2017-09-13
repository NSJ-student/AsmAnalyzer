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
		AsmInterpreter AsmIntp;
		ArmAsmDataBase.GetMatchingMemory GetMemRow;
		RegisterControl[] RegArray;
		public Visualizer(string SymbolName, ArmAsmDataBase.GetMatchingMemory callback)
		{
			InitializeComponent();

			AsmIntp = new AsmInterpreter(GetValue, SetValue);

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

		/// <summary>
		/// ViewSymbolAsm에서 선택한 행의 어셈블리를 번역하여 레지스터 등에 적용하는 동작을 현 UI에 적용
		/// </summary>
		/// <param name="input"></param>
		public void SetInput(string Type, uint MemAddr, string Instruction, string Parameter)
		{
			if(Type.Equals("assembly"))
			{
				try
				{
					string[] Par = AsmIntp.SplitParam(Parameter);
					RegArray[15].txtValue.Text = MemAddr.ToString("X");

					AsmInterpreter.ParamType type = AsmInterpreter.ParamType.None;
					txtInst.Text = Instruction;
					if (Par.Length > 0)
					{
						txtParam1.Text = Par[0];
						txtParam1ToHex.Text = AsmIntp.ParseToHexString(MemAddr, Par[0], ref type);
					}
					if (Par.Length > 1)
					{
						txtParam2.Text = Par[1];
						txtParam2ToHex.Text = AsmIntp.ParseToHexString(MemAddr, Par[1], ref type);
					}
					if (Par.Length > 2)
					{
						txtParam3.Text = Par[2];
						txtParam3ToHex.Text = AsmIntp.ParseToHexString(MemAddr, Par[2], ref type);
					}

					AsmIntp.ParseInstruction(Instruction, Parameter);
					txtInputAll.Text = "OK";
				}
				catch
				{
					RegArray[15].txtValue.Text = MemAddr.ToString("X");
					txtInst.Clear();
					txtParam1.Clear();
					txtParam2.Clear();
					txtInputAll.Text = "Error";
				}
			}
			else
			{
				RegArray[15].txtValue.Text = MemAddr.ToString("X");
				txtInst.Clear();
				txtParam1.Clear();
				txtParam2.Clear();
				txtInputAll.Text = "OK";
			}

		}

		/// <summary>
		/// 레지스터 출력값 16진수/10진수 변환 버튼 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// 레지스터 값을 클리어
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbRegClear_Click(object sender, EventArgs e)
		{
			foreach(RegisterControl item in RegArray)
			{
				item.txtValue.Text = "0";
			}
		}


		private bool GetValue(ref MemInfo refer)
		{
			MemInfo info = refer;
			switch(info.Area)
			{
				case MemInfo.MemArea.BSS:
				case MemInfo.MemArea.DATA:
				case MemInfo.MemArea.WORD:
				case MemInfo.MemArea.RODATA:
					return GetRamValue(ref refer);
				case MemInfo.MemArea.REGISTER:
					return GetRegValue(ref refer);
				case MemInfo.MemArea.STACK:
					return GetStackValue(ref refer);
				default:
					return false;
			}
		}
		private bool SetValue(ref MemInfo refer)
		{
			switch (refer.Area)
			{
				case MemInfo.MemArea.BSS:
				case MemInfo.MemArea.DATA:
				case MemInfo.MemArea.WORD:
				case MemInfo.MemArea.RODATA:
					return PutRamValue(ref refer);
				case MemInfo.MemArea.REGISTER:
					return PutRegValue(ref refer);
				case MemInfo.MemArea.STACK:
					return PutStackValue(ref refer);
				default:
					return false;
			}
		}

		private bool GetStackValue(ref MemInfo refer)
		{
			return false;
		}
		private bool PutStackValue(ref MemInfo refer)
		{
			return false;
		}
		private bool GetRamValue(ref MemInfo refer)
		{
			MemInfo outinfo = GetMemRow(refer.MemAddr);
			
			if (outinfo == null)
			{
				ListViewItem[] items = lvRam.Items.Find(outinfo.MemAddr, true);
				if (items.Length != 0)
				{
					// Define the list items
					ListViewItem lvi = new ListViewItem(refer.MemAddr);
					lvi.SubItems.Add("Get Failed");
					lvi.SubItems.Add("0");

					// Add the list items to the ListView
					lvRam.Items.Add(lvi);
					return false;
				}
				else
				{
					refer.Area = MemInfo.MemArea.PERIPHERAL;
					refer.Value = items[0].SubItems[2].Text;
					return true;
				}
			}
			else if ((outinfo.Area == MemInfo.MemArea.BSS)||
				(outinfo.Area == MemInfo.MemArea.DATA)
				)
			{
				ListViewItem[] items = lvRam.Items.Find(outinfo.MemAddr, true);
				if (items.Length == 0)
				{
					// Define the list items
					ListViewItem lvi = new ListViewItem(outinfo.MemAddr);
					lvi.SubItems.Add(outinfo.Symbol);
					lvi.SubItems.Add(outinfo.Value);

					// Add the list items to the ListView
					lvRam.Items.Add(lvi);
					refer.Value = outinfo.Value;
				}
				else
				{
					refer.Value = items[0].SubItems[2].Text;
				}
				refer.Area = outinfo.Area;
				return true;
			}
			else if (outinfo.Area == MemInfo.MemArea.RODATA)
			{
				ListViewItem[] items = lvRam.Items.Find(outinfo.MemAddr, true);
				if(items.Length == 0)
				{
					// Define the list items
					ListViewItem lvi = new ListViewItem(outinfo.MemAddr);
					lvi.SubItems.Add(outinfo.Symbol);
					lvi.SubItems.Add(outinfo.Value);

					// Add the list items to the ListView
					lvRam.Items.Add(lvi);
				}
				refer.Value = outinfo.Value;
				refer.Area = outinfo.Area;
				return true;
			}
			else if (outinfo.Area == MemInfo.MemArea.WORD)
			{
				refer.Value = outinfo.Value;
				refer.Area = outinfo.Area;
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool PutRamValue(ref MemInfo refer)
		{
			MemInfo outinfo = GetMemRow(refer.MemAddr);
			if (outinfo == null)
			{
				ListViewItem[] items = lvRam.Items.Find(refer.MemAddr, true);

				if (items.Length == 0)
				{
					// Define the list items
					ListViewItem lvi = new ListViewItem(refer.MemAddr);
					lvi.SubItems.Add("Unknown");
					lvi.SubItems.Add(refer.Value);

					// Add the list items to the ListView
					lvRam.Items.Add(lvi);
				}
				else
				{
					items[0].SubItems[2].Text = refer.Value;
				}
				return true;
			}
			else if ((outinfo.Area == MemInfo.MemArea.BSS) ||
				(outinfo.Area == MemInfo.MemArea.DATA)
				)
			{
				ListViewItem[] items = lvRam.Items.Find(refer.MemAddr, true);
				if (items.Length == 0)
				{
					// Define the list items
					ListViewItem lvi = new ListViewItem(outinfo.MemAddr);
					lvi.SubItems.Add(outinfo.Symbol);
					lvi.SubItems.Add(outinfo.Value);

					// Add the list items to the ListView
					lvRam.Items.Add(lvi);
				}
				else
				{
					items[0].SubItems[2].Text = refer.Value;
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool GetRegValue(ref MemInfo refer)
		{
			string regnum = refer.MemAddr.Replace("r", "").Trim();
			string RegValue = RegArray[Convert.ToUInt32(regnum)].txtValue.Text;

			if (RegValue.Length == 0)
			{
				refer.Area = MemInfo.MemArea.REGISTER;
				return true;
			}
			else
			{
				string Format = RegArray[Convert.ToUInt32(regnum)].btnFormat.Text;
				if (Format.Equals("DEX"))
				{
					uint value = Convert.ToUInt32(RegValue);
					refer.Value = value.ToString("X");
					return true;
				}
				else
				{
					refer.Value = RegValue;
					return true;
				}
			}
		}
		private bool PutRegValue(ref MemInfo refer)
		{
			string regnum = refer.MemAddr.Replace("r", "").Trim();
			RegArray[Convert.ToUInt32(regnum)].txtValue.Text = refer.Value;

			return true;
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
