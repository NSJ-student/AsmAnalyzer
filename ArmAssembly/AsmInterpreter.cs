﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmAssembly
{
	public static class AsmInterpreter
	{
		public delegate object[] GetMatchedRow(string memory);
		public enum ParamType
		{
			Register,
			AbsoluteAddress,
			RegRelativeAddress,
			PcRelativeAddress,
			Integer,
			Vector,
			None
		};

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
		public static void ParseInstruction(object[] Source, RegisterControl[] Regs, GetMatchedRow getMemValue)
		{
			string MemAddr		= (string)Source[2];
			string Instruction	= (string)Source[5];
			string Parameter	= (string)Source[6];


			if(Instruction.Equals("mov") || Instruction.Equals("movs"))
			{
				string[] Pars = SplitParam(Parameter);

				ParamType dstType = ParamType.None;
				ParamType srcType = ParamType.None;
				string dest = ParseToHexString(MemAddr, Pars[0], Regs, ref dstType);
				string src = ParseToHexString(MemAddr, Pars[1], Regs, ref srcType);

				if(dstType == ParamType.Register)
				{
					uint regPos = Convert.ToUInt32(dest.Replace("r", ""));
					if ((srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if (memRow != null)
						{
							Regs[regPos].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[regPos].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[regPos].txtValue.Text = src;
					}
				}
				else if((dstType != ParamType.None) && (dstType != ParamType.Vector))
				{
					// 특정 어드레스로 출력
					Regs[14].txtValue.Text = dest;
					if( (srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if(memRow != null)
						{
							Regs[15].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[15].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[15].txtValue.Text = src;
					}
				}
			}
			if (Instruction.Equals("str") || Instruction.Equals("strb"))
			{
				string[] Pars = SplitParam(Parameter);

				ParamType dstType = ParamType.None;
				ParamType srcType = ParamType.None;
				string src = ParseToHexString(MemAddr, Pars[0], Regs, ref srcType);
				string dest = ParseToHexString(MemAddr, Pars[1], Regs, ref dstType);

				if (srcType == ParamType.Register)
				{
					uint regPos = Convert.ToUInt32(dest.Replace("r", ""));
					if ((srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if (memRow != null)
						{
							Regs[regPos].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[regPos].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[regPos].txtValue.Text = src;
					}
				}
				else if ((srcType != ParamType.None) && (srcType != ParamType.Vector))
				{
					// 특정 어드레스로 출력
					Regs[14].txtValue.Text = dest;
					if ((srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if (memRow != null)
						{
							Regs[15].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[15].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[15].txtValue.Text = src;
					}
				}
			}
			if (Instruction.Equals("ldr") || Instruction.Equals("ldrb"))
			{
				string[] Pars = SplitParam(Parameter);

				ParamType dstType = ParamType.None;
				ParamType srcType = ParamType.None;
				string dest = ParseToHexString(MemAddr, Pars[0], Regs, ref dstType);
				string src = ParseToHexString(MemAddr, Pars[1], Regs, ref srcType);

				if (dstType == ParamType.Register)
				{
					uint regPos = Convert.ToUInt32(dest.Replace("r", ""));
					if ((srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if (memRow != null)
						{
							Regs[regPos].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[regPos].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[regPos].txtValue.Text = src;
					}
				}
				else if ((dstType != ParamType.None) && (dstType != ParamType.Vector))
				{
					// 특정 어드레스로 출력
					Regs[14].txtValue.Text = dest;
					if ((srcType == ParamType.RegRelativeAddress) ||
						(srcType == ParamType.PcRelativeAddress) ||
						(srcType == ParamType.AbsoluteAddress))
					{
						object[] memRow = getMemValue(src);
						if (memRow != null)
						{
							Regs[15].txtValue.Text = ((string)memRow[6]).Replace("0x", "");
						}
						else
						{
							Regs[15].txtValue.Text = "Can't find " + src;
						}
					}
					else
					{
						Regs[15].txtValue.Text = src;
					}
				}
			}

		}

		public static ParamType GetSourceType(object[] RowArray)
		{
			if((LssElements.LssType)RowArray[9] == LssElements.LssType.ASSEM_INSTRUCTION)
			{
				string Param = (string)RowArray[6];
				string[] split = Param.Split(new char[] { ',', ' ' }, 2);

				return ParamType.None;
			}
			else
			{
				return ParamType.None;
			}
		}

		public static string GetRegValue(string strReg, RegisterControl[] Reg)
		{
			string regnum = strReg.Replace("r", "").Trim();
			string RegValue = Reg[Convert.ToUInt32(regnum)].txtValue.Text;

			if (RegValue.Length == 0)
			{
				return strReg;
			}
			else
			{
				string Format = Reg[Convert.ToUInt32(regnum)].btnFormat.Text;
				if (Format.Equals("DEX"))
				{
					uint value = Convert.ToUInt32(RegValue);
					return value.ToString("X");
				}
				else
				{
					return RegValue;
				}
			}
		}

		public static string AddHexString(string Old, string AddHex)
		{
			string result = Old;
			
			if ((AddHex[0] == 'r') ||
				(AddHex[0] == 'p'))
			{
				result += AddHex;
			}
			else if ((result.Length != 0) &&
					 ((Old[0] == 'r') ||  (Old[0] == 'p')))
			{
				result += " + " + AddHex;
			}
			else
			{
				if(Old.Length == 0)
				{
					Old = "0";
				}
				uint oldVal = Convert.ToUInt32(Old, 16);
				uint addVal = Convert.ToUInt32(AddHex, 16);
				result = (oldVal + addVal).ToString("X");
			}
			
			return result;
		}

		public static string ParseToHexString(string pc, string Input, RegisterControl[] Reg, ref ParamType type)
		{
			string result = "";

			if (Input[0] == 'r')
			{
				// register
				type = ParamType.Register;
				result = Input;
			}
			else if (Input[0] == '[')
			{
				// relative address
				string[] split = Input.Split(new char[] { ' ', ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string item in split)
				{
					if(item[0] == 'r')
					{
						type = ParamType.RegRelativeAddress;
						string reg_val = GetRegValue(item, Reg);
						result = AddHexString(result, reg_val);
					}
					if (item.Equals("pc"))
					{
						type = ParamType.PcRelativeAddress;
						result = AddHexString(result, pc);
						result = AddHexString(result, "4");
					}
					else if(item[0] == '#')
					{
						string dec = item.Replace("#", "");
						uint value = Convert.ToUInt32(dec);
						result = AddHexString(result, value.ToString("X"));
					}
				}
			}
			else if (Input[0] == '{')
			{
				// vector
				type = ParamType.None;
			}
			else if (Input[0] == '<')
			{
				// symbol
				type = ParamType.None;
			}
			else if (Input[0] == '#')
			{
				// decimal
				type = ParamType.Integer;
				string dec = Input.Replace("#", "");
				uint value = Convert.ToUInt32(dec);
				result = AddHexString(result, value.ToString("X"));
			}
			else if (((0 <= Input[0]) && (Input[0] <= '9')) ||
					 (('a' <= Input[0]) && (Input[0] <= 'z')))
			{
				// hexa
				type = ParamType.Integer;
				result = Input;
			}

			return result;
		}

		public static string[] SplitParam(string Param)
		{
			List<string> strList = new List<string>();
			strList.Add("");

			int TokenCount = 0;
			for(int index=0; index<Param.Length; index++)
			{
				if (
					(index > 0) &&
					((Param[index] == '[') || (Param[index] == '{') || (Param[index] == '<'))
				   )
				{
					strList[TokenCount].TrimEnd();
					if(strList[TokenCount][strList[TokenCount].Length-1] == ',')
					{
						strList[TokenCount].Remove(strList[TokenCount].Length - 1);
					}
					strList.Add("");
					TokenCount++;
				}
				else if  ((Param[index] == ',') && (Param[index+1] == ' ') && (strList[TokenCount][0] != '[') && (strList[TokenCount][0] != '{'))
				{
					index += 2;
					strList.Add("");
					TokenCount++;
				}

				strList[TokenCount] = strList[TokenCount].Insert(strList[TokenCount].Length, Param[index].ToString());
			}

			return strList.ToArray();
		}
	}
}
