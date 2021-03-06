using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmAssembly
{
	public delegate bool ArmValueHandler(ref MemInfo refer);
	public class AsmInterpreter
	{
		ArmValueHandler getValue;
		ArmValueHandler setValue;
		public enum ParamType
		{
			Register,
			AbsoluteAddress,
			RegRelativeAddress,
			PcRelativeAddress,
			StackRelativeAddress,
			Integer,
			Vector,
			None
		};

		public AsmInterpreter(ArmValueHandler argGet, ArmValueHandler argSet)
		{
			getValue = argGet;
			setValue = argSet;
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
		public void ParseInstruction(string Instruction, string Parameter)
		{
			if (Instruction.Equals("mov") || Instruction.Equals("movb") || Instruction.Equals("movs") || Instruction.Equals("mov.w"))
			{
				string[] Pars = SplitParam(Parameter);
				Move(Pars[0], Pars[1]);
			}
			if (Instruction.Equals("str") || Instruction.Equals("strb") || Instruction.Equals("strs") || Instruction.Equals("str.w"))
			{
				string[] Pars = SplitParam(Parameter);
				Move(Pars[1], Pars[0]);
			}
			if (Instruction.Equals("ldr") || Instruction.Equals("ldrb") || Instruction.Equals("ldrs") || Instruction.Equals("ldr.w"))
			{
				string[] Pars = SplitParam(Parameter);
				Move(Pars[0], Pars[1]);
			}
		}

		public bool Move(string dstOperand, string srcOperand)
		{
			MemInfo info = new MemInfo("r15", null, null, MemInfo.MemArea.REGISTER);
			if(!getValue(ref info))
			{
				return false;
			}
			
			uint pc = Convert.ToUInt32(info.Value, 16);
			ParamType dstType = ParamType.None;
			ParamType srcType = ParamType.None;
			string src = ParseToHexString(pc, srcOperand, ref srcType);
			string dest = ParseToHexString(pc, dstOperand, ref dstType);
			string srcResult;

			// src 값을 srcResult에 저장
			if (srcType == ParamType.Register)
			{
				info = new MemInfo(src, null, null, MemInfo.MemArea.REGISTER);
				if (getValue(ref info))
					srcResult = info.Value;
				else
					return false;
			}
			else if ((srcType == ParamType.RegRelativeAddress) ||
				(srcType == ParamType.PcRelativeAddress) ||
				(srcType == ParamType.AbsoluteAddress))
			{
				info = new MemInfo(src, null, null, MemInfo.MemArea.DATA);
				if (getValue(ref info))
					srcResult = info.Value;
				else
					return false;
			}
			else if(srcType == ParamType.StackRelativeAddress)
			{
				srcResult = src;
			}
			else
			{
				srcResult = src;
			}

			// srcResult값을 dst에 저장
			if (dstType == ParamType.Register)
			{
				info = new MemInfo(dest, null, srcResult, MemInfo.MemArea.REGISTER);
				if (!setValue(ref info))
				{
					return false;
				}
			}
			else if ((dstType == ParamType.RegRelativeAddress) ||
				(dstType == ParamType.PcRelativeAddress) ||
				(dstType == ParamType.AbsoluteAddress))
			{
				info = new MemInfo(dest, null, srcResult, MemInfo.MemArea.DATA);
				if (!setValue(ref info))
				{
					return false;
				}
			}
			else if(dstType == ParamType.StackRelativeAddress)
			{
				return false;
			}
			else
			{
				return false;
			}

			return true;
		}

		public string Add(string Operand1, string Operand2)
		{
			MemInfo info = new MemInfo("r15", null, null, MemInfo.MemArea.REGISTER);
			if (!getValue(ref info))
			{
				return null;
			}

			uint pc = Convert.ToUInt32(info.Value, 16);
			ParamType Op1Type = ParamType.None;
			ParamType Op2Type = ParamType.None;
			string Op1 = ParseToHexString(pc, Operand1, ref Op1Type);
			string Op2 = ParseToHexString(pc, Operand2, ref Op2Type);
			string Op1Result, Op2Result;

			// src 값을 srcResult에 저장
			if (Op1Type == ParamType.Register)
			{
				info = new MemInfo(Op1, null, null, MemInfo.MemArea.REGISTER);
				if (getValue(ref info))
					Op1Result = info.Value;
				else
					return null;
			}
			else if ((Op1Type == ParamType.RegRelativeAddress) ||
				(Op1Type == ParamType.PcRelativeAddress) ||
				(Op1Type == ParamType.AbsoluteAddress))
			{
				info = new MemInfo(Op1, null, null, MemInfo.MemArea.DATA);
				if (getValue(ref info))
					Op1Result = info.Value;
				else
					return null;
			}
			else if (Op1Type == ParamType.StackRelativeAddress)
			{
				return null;
			}
			else
			{
				Op1Result = Op1;
			}

			// srcResult값을 dst에 저장
			if (Op2Type == ParamType.Register)
			{
				info = new MemInfo(Op2, null, null, MemInfo.MemArea.REGISTER);
				if (getValue(ref info))
					Op2Result = info.Value;
				else
					return null;
			}
			else if ((Op2Type == ParamType.RegRelativeAddress) ||
				(Op2Type == ParamType.PcRelativeAddress) ||
				(Op2Type == ParamType.AbsoluteAddress))
			{
				info = new MemInfo(Op2, null, null, MemInfo.MemArea.DATA);
				if (getValue(ref info))
					Op2Result = info.Value;
				else
					return null;
			}
			else if (Op2Type == ParamType.StackRelativeAddress)
			{
				return null;
			}
			else
			{
				Op2Result = Op2;
			}

			return AddHexString(Op1Result, Op2Result);
		}

		public string AddHexString(string Old, string AddHex)
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

		/// <summary>
		/// parse assembly operand to hex string
		///     ex1) [r1, #12] ==(r1  == 400200)==> return 40020C
		///     ex2) [r2, #5]  ==(r2  == null  )==> return r2 + 5
		///     ex3) [pc, #3]  ==(mem == 400320)==> return 400327
		///     ex4) r2  => return r2
		///     ex5) #13 => return D
		/// </summary>
		/// <param name="pc"></param>
		/// <param name="Operand"></param>
		/// <param name="Reg"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public string ParseToHexString(uint pc, string Operand, ref ParamType type)
		{
			string result = "";

			if (Operand[0] == 'r')
			{
				// register
				type = ParamType.Register;
				result = Operand;
			}
			else if (Operand[0] == '[')
			{
				// relative address
				string[] split = Operand.Split(new char[] { ' ', ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string item in split)
				{
					if(item[0] == 'r')
					{
						type = ParamType.RegRelativeAddress;
						MemInfo info = new MemInfo(item, null, null, MemInfo.MemArea.REGISTER);
						if(getValue(ref info))
						{
							string reg_val = info.Value;
							result = AddHexString(result, reg_val);
						}
					}
					else if (item.Equals("pc"))
					{
						type = ParamType.PcRelativeAddress;
						result = AddHexString(result, (pc+4).ToString("X"));
					}
					else if(item.Equals("sp"))
					{
						type = ParamType.StackRelativeAddress;
					//	string reg_val = GetRegValue("r13", Reg);
						result = AddHexString(result, "0");
					}
					else if (item.Equals("lr"))
					{
						type = ParamType.RegRelativeAddress;
						MemInfo info = new MemInfo("r14", null, null, MemInfo.MemArea.REGISTER);
						if (getValue(ref info))
						{
							string reg_val = info.Value;
							result = AddHexString(result, reg_val);
						}
					}
					else if(item[0] == '#')
					{
						string dec = item.Replace("#", "");
						uint value = Convert.ToUInt32(dec);
						result = AddHexString(result, value.ToString("X"));
					}
				}
			}
			else if (Operand[0] == '{')
			{
				// vector
				type = ParamType.None;
			}
			else if (Operand[0] == '<')
			{
				// symbol
				type = ParamType.None;
			}
			else if (Operand[0] == '#')
			{
				// decimal
				type = ParamType.Integer;
				string dec = Operand.Replace("#", "");
				uint value = Convert.ToUInt32(dec);
				result = AddHexString(result, value.ToString("X"));
			}
			else if (((0 <= Operand[0])	&& (Operand[0] <= '9'))	||
					 (('a' <= Operand[0]) && (Operand[0] <= 'z'))	||
					 (('A' <= Operand[0]) && (Operand[0] <= 'F')))
			{
				// hexa
				type = ParamType.Integer;
				result = Operand;
			}

			return result;
		}

		/// <summary>
		/// split assemby operand
		///    devide by ",", "[]", "{}", "<>"
		///    
		///    ex1) Param = "r1, [pc, #12]"
		///         return = "r1", "[pc, #12]"
		///    ex2) Param = "r4, #5"
		///         return = "r4", "#5"
		/// </summary>
		/// <param name="Param"></param>
		/// <returns></returns>
		public string[] SplitParam(string Param)
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

		public static ParamType GetSourceType(object[] RowArray)
		{
			if ((LssElements.LssType)RowArray[9] == LssElements.LssType.ASSEM_INSTRUCTION)
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

	}
	public class MemInfo
	{
		public enum MemArea
		{
			WORD,
			RODATA,
			BSS,
			DATA,
			STACK,
			REGISTER,
			PERIPHERAL,
			NONE
		};
		public string MemAddr;
		public string Symbol;
		public string Value;
		public MemArea Area;
		
		public MemInfo(string argAddr, string argName, string argVal, string argArea)
		{
			MemAddr = argAddr;
			Symbol = argName;
			Value = argVal;
			if (argArea.Equals("text"))
				Area = MemArea.WORD;
			else if (argArea.Equals("bss"))
				Area = MemArea.BSS;
			else if (argArea.Equals("common"))
				Area = MemArea.BSS;
			else if (argArea.Equals("data"))
				Area = MemArea.DATA;
			else if (argArea.Equals("rodata"))
				Area = MemArea.RODATA;
			else if (argArea.Equals("register"))
				Area = MemArea.RODATA;
			else if (argArea.Equals("stack"))
				Area = MemArea.RODATA;
			else
				Area = MemArea.NONE;
		}
		public MemInfo(string argAddr, string argName, string argVal, MemArea argArea)
		{
			MemAddr = argAddr;
			Symbol = argName;
			Value = argVal;
			Area = argArea;
		}
	}
}
