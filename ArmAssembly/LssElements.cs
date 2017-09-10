using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace ArmAssembly
{
	public class LssContainer
	{
		public delegate void UpdateProgress(int value);
		public string[] AllStrings;
		public List<LssElements> ElementList;
		public List<LssElements> SymbolList;
		public LssContainer(string FileName)
		{
			ElementList = new List<LssElements>();
			SymbolList = new List<LssElements>();
			AllStrings = File.ReadAllLines(FileName);
			for (int cnt=0; cnt<AllStrings.Length; cnt++)
			{
				string str = AllStrings[cnt];
				if (!str.Replace(" ", string.Empty).Equals(""))
				{
					LssElements element = new LssElements(str);
					ElementList.Add(element);
					if(!element.Symbol.Equals(""))
						SymbolList.Add(element);
				}
			}
		}
		public LssContainer(string FileName, UpdateProgress Update)
		{
			ElementList = new List<LssElements>();
			SymbolList = new List<LssElements>();
			AllStrings = File.ReadAllLines(FileName);
			for (int cnt = 0; cnt < AllStrings.Length; cnt++)
			{
				string str = AllStrings[cnt];
				if (!str.Replace(" ", string.Empty).Equals(""))
				{
					LssElements element = new LssElements(str);
					ElementList.Add(element);
					if (!element.Symbol.Equals(""))
						SymbolList.Add(element);
				}
				Update((cnt + 1) * 100 / AllStrings.Length);
			}
			Update(0);
		}
	}
	public class LssElements
	{
		static uint refCount = 0;
		public enum LssType
		{
			SYMBOL_NAME,
			ASSEM_INSTRUCTION,
			CSOURCE,
			WORD_DATA,
			ARR_DATA,
			ANYTHING_ELSE
		};
		uint IndexNum;
		LssType Type;
		string EntireString;
		uint MemoryPosition;
		string Name;
		string strInstruction;
		string strParameter;
		uint hexInstruction;
		string Comment;

		public uint Index
		{
			get
			{
				return IndexNum;
			}
		}
		public string Area
		{
			get
			{
				switch (Type)
				{
					case LssType.CSOURCE: return "source";
					case LssType.WORD_DATA: return "word data";
					case LssType.ARR_DATA: return "array";
					case LssType.SYMBOL_NAME: return "symbol";
					case LssType.ASSEM_INSTRUCTION: return "assembly";
					default: return "undefined";
				}
			}
		}
		public uint Memory
		{
			get
			{
				return MemoryPosition;
			}
		}
		public string Symbol
		{
			get
			{
				return Name;
			}
		}
		[DisplayName("Instruction(hex)")]
		public string HexInstruction
		{
			get
			{
				if ((MemoryPosition == 0)||(strInstruction.Equals("")))
					return "";
				else
					return hexInstruction.ToString("X");
			}
		}
		public string Instruction
		{
			get
			{
				return strInstruction;
			}
		}
		public string Parameter
		{
			get
			{
				return strParameter;
			}
		}
		[DisplayName("Comment")]
		public string CommentValue
		{
			get
			{
				return Comment;
			}
		}
		[Browsable(false)]
		public string StringLine
		{
			get
			{
				return EntireString;
			}
		}
		[Browsable(false)]
		public LssType ElementType
		{
			get
			{
				return Type;
			}
		}

		public LssElements(string strLine)
		{
			refCount++;
			IndexNum = refCount;
			EntireString = strLine;
			if(strLine.Length >= 10)
			{
				if ((strLine[9] == '<') && (strLine[strLine.Length - 1] == ':'))
				{
					Type = LssType.SYMBOL_NAME;

					string[] arrstr = strLine.Split(new char[] { '<', '>' });

					Name = arrstr[1].Trim();
					MemoryPosition = Convert.ToUInt32(arrstr[0].Trim(), 16);
					hexInstruction = 0;
					strInstruction = "";
					strParameter = "";
					Comment = "";
					return;
				}
				else if ((strLine[8] == ':') && (strLine[9] == '\t'))
				{
					string[] cmt = strLine.Split(new char[] { ';' });
					string[] split1 = cmt[0].Split(new char[] { ':' });

					Name = "";
					MemoryPosition = Convert.ToUInt32(split1[0].Trim(), 16);
					if (cmt.Length >= 2)
					{
						Comment = cmt[1];
					}
					else
					{
						Comment = "";
					}

					string[] split2 = split1[1].Split(new char[] { '\t' });
					if(split2.Length >= 3)
					{
						string hexInst = split2[1].Replace(" ", string.Empty);
						hexInstruction = Convert.ToUInt32(hexInst, 16);
						strInstruction = split2[2].Trim();
						if (split2.Length >= 4)
						{
							strParameter = split2[3];
						}
						else
						{
							strParameter = "";
						}
						if (strInstruction.Equals(".word"))
						{
							strParameter = strParameter.Replace("0x", "");
							Type = LssType.WORD_DATA;
						}
						else
						{
							Type = LssType.ASSEM_INSTRUCTION;
						}
					}
					else
					{
						string middle = split2[1].Remove(split2[1].Length - 16, 16);
						strParameter = middle.TrimEnd(); //split2[1];
//						hexInstruction = Convert.ToUInt32(middle.Replace(" ", string.Empty), 16);
						hexInstruction = 0;
						strInstruction = "";
						Type = LssType.ARR_DATA;
						if(Comment == null)
						{
							string[] devide = split2[1].Split();
							Comment = devide[devide.Length - 1];
						}
						else
						{
							Comment = "";
						}
					}
					return;
				}
			}
			Type = LssType.CSOURCE;
			Name = "";
			MemoryPosition = 0;
			hexInstruction = 0;
			strInstruction = "";
			Comment = strLine;
			strParameter = "";
		}
		~LssElements()
		{
			refCount--;
		}
	}
}
