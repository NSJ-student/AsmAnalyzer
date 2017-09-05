using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmAssembly
{
	public static class AsmInterpreter
	{
		public enum ParamType
		{
			AbsoluteAddress,
			RegRelativeAddress,
			PcRelativeAddress,
			Integer,
			Vector,
			None
		};

		// RowArray[10]
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

		public static string ParseToHex(string Input, RegisterControl[] RegArray)
		{
			if (Input[0] == 'r')
			{
				// register
			}
			else if (Input[0] == '[')
			{
				// relative address
			}
			else if (Input[0] == '{')
			{
				// vector
			}
			else if (Input[0] == '<')
			{
				// symbol
			}
			else if (Input[0] == '#')
			{
				// decimal
			}
			else if (((0 <= Input[0]) && (Input[0] <= '9')) ||
					 (('a' <= Input[0]) && (Input[0] <= 'z')))
			{
				// hexa
			}

			return null;
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
