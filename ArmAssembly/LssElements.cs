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
					if(element.Symbol != null)
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
					if (element.Symbol != null)
						SymbolList.Add(element);
				}
				Update((cnt + 1) * 100 / AllStrings.Length);
			}
		}
	}
	public class LssElements
	{
		public enum LssType
		{
			SYMBOL_NAME,
			ASSEM_INSTRUCTION,
			CSOURCE,
			WORD_DATA,
			ARR_DATA,
			ANYTHING_ELSE
		};
		LssType Type;
		string EntireString;
		uint MemoryPosition;
		string Name;
		string strInstruction;
		string strParameter;
		uint hexInstruction;
		string Comment;

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
		public string Memory
		{
			get
			{
				if (MemoryPosition == 0)
					return null;
				else
					return MemoryPosition.ToString("X");
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
				if ((MemoryPosition == 0)||(strInstruction == null))
					return null;
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
					strInstruction = null;
					strParameter = null;
					return;
				}
				else if ((strLine[8] == ':') && (strLine[9] == '\t'))
				{
					string[] cmt = strLine.Split(new char[] { ';' });
					string[] split1 = cmt[0].Split(new char[] { ':' });

					Name = null;
					MemoryPosition = Convert.ToUInt32(split1[0].Trim(), 16);
					if (cmt.Length >= 2)
					{
						Comment = cmt[1];
					}

					string[] split2 = split1[1].Split(new char[] { '\t' });
					try
					{
						hexInstruction = Convert.ToUInt32(split2[1].Replace(" ", string.Empty), 16);
						strInstruction = split2[2].Trim();
						if (split2.Length >= 4)
						{
							strParameter = split2[3];
						}
						if (strInstruction.Equals(".word"))
						{
							Type = LssType.WORD_DATA;

						}
						else
						{
							Type = LssType.ASSEM_INSTRUCTION;
						}
					}
					catch
					{
						string middle = split2[1].Remove(split2[1].Length - 16, 16);
						strParameter = middle.TrimEnd(); //split2[1];
//						hexInstruction = Convert.ToUInt32(middle.Replace(" ", string.Empty), 16);
						hexInstruction = 0;
						Type = LssType.ARR_DATA;
						if(Comment == null)
						{
							string[] devide = split2[1].Split();
							Comment = devide[devide.Length - 1];
						}
					}
					return;
				}
			}
			Type = LssType.CSOURCE;
			Name = null;
			MemoryPosition = 0;
			hexInstruction = 0;
			strInstruction = null;
			Comment = strLine;
			strParameter = null;
		}
	}
}
