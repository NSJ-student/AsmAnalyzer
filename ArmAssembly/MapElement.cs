using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace ArmAssembly
{
	public class MapContainer
	{
		public delegate void UpdateProgress(int value);
		public string[] AllStrings;
		public List<MapElements> ElementList;
		public List<MapElements> SymbolList;
		public MapContainer(string FileName)
		{
			ElementList = new List<MapElements>();
			AllStrings = File.ReadAllLines(FileName);
			for (int cnt = 0; cnt < AllStrings.Length; cnt++)
			{
				string str = AllStrings[cnt];
				if (!str.Replace(" ", string.Empty).Equals(""))
				{
					if (MapElements.StringSortMapType(str) != MapElements.MapType.SYM_NONE)
					{
						int itemcnt = 0;
						string strLoop = AllStrings[cnt + 1];
						while (ArmAssembly.MapElements.StringSortMapType(strLoop) == ArmAssembly.MapElements.MapType.SYM_NONE)
						{
							itemcnt++;
							if (cnt + 1 + itemcnt == AllStrings.Length)
								break;
							if (!AllStrings[cnt + 1 + itemcnt].Equals(""))
							{
								strLoop = AllStrings[cnt + 1 + itemcnt];
							}
						}
						if (itemcnt != 0)
						{
							string[] arg = new string[itemcnt + 1];
							int target_cnt = cnt + 1 + itemcnt;
							for (int loop = 0; cnt < target_cnt; cnt++)
							{
								if (!AllStrings[cnt].Equals(""))
								{
									arg[loop] = AllStrings[cnt];
									loop++;
								}
							}
							MapElements element = new MapElements(arg);
							ElementList.Add(element);
							cnt--;
						}
					}
				}
			}
		}
		public MapContainer(string FileName, UpdateProgress Update)
		{
			ElementList = new List<MapElements>();
			SymbolList = new List<MapElements>();
			AllStrings = File.ReadAllLines(FileName);
			for (int cnt = 0; cnt < AllStrings.Length; cnt++)
			{
				string str = AllStrings[cnt];
				Update((cnt + 1) * 100 / AllStrings.Length);
				if (!str.Replace(" ", string.Empty).Equals(""))
				{
					if (MapElements.StringSortMapType(str) != MapElements.MapType.SYM_NONE)
					{
						int itemcnt = 0;
						string strLoop = AllStrings[cnt+1];
						while (ArmAssembly.MapElements.StringSortMapType(strLoop) == ArmAssembly.MapElements.MapType.SYM_NONE)
						{
							itemcnt++;
							if (cnt + 1 + itemcnt == AllStrings.Length)
								break;
							if(!AllStrings[cnt + 1 + itemcnt].Equals(""))
							{
								strLoop = AllStrings[cnt + 1 + itemcnt];
							}
						}
						if(itemcnt != 0)
						{
							string[] arg = new string[itemcnt+1];
							int target_cnt = cnt + 1 + itemcnt;
							for (int loop = 0; cnt < target_cnt; cnt++)
							{
								if (!AllStrings[cnt].Equals(""))
								{
									arg[loop] = AllStrings[cnt];
									loop++;
								}
							}
							MapElements element = new MapElements(arg);
							ElementList.Add(element);
							cnt--;
						}
					}
				}
			}
		}
	}
	public class MapElements
	{
		public enum MapType
		{
			SYM_TEXT,
			SYM_RODATA,
			SYM_RAMFUNC,
			SYM_DATA,
			SYM_BSS,
			SYM_COMMON,
			SYM_STACK,
			SYM_NONE
		};
		MapType Type;
		string SymbolName;
		uint MemAddr;
		uint MemSize;
		string FileLocation;
		public string Area
		{
			get
			{
				switch(Type)
				{
					case MapType.SYM_DATA: return "data";
					case MapType.SYM_TEXT: return "text";
					case MapType.SYM_RODATA: return "rodata";
					case MapType.SYM_BSS: return "bss";
					case MapType.SYM_COMMON: return "common";
					case MapType.SYM_RAMFUNC: return "ramfunc";
					case MapType.SYM_STACK: return "stack";
					default: return "undefined";
				}
			}
		}
		public string Symbol
		{
			get
			{
				return SymbolName;
			}
		}
		[DisplayName("Address(hex)")]
		public string Address
		{
			get
			{
				return MemAddr.ToString("X");
			}
		}
		public string Size
		{
			get
			{
				return MemSize.ToString();
			}
		}
		public string Location
		{
			get
			{
				return FileLocation;
			}
		}


		public MapElements(string[] input)
		{
			// .(type).(symbolname)
			string[] split1 = input[0].Split(new char[] { "       ".ToCharArray()[0], "                ".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			string[] split2 = split1[0].Split(new char[] { ".".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			Type = SortMapType(split2[0]);
			SymbolName = split2[1];
			
			if (split1.Length == 1)
			{
				string[] temp = input[1].Split(new char[] { "       ".ToCharArray()[0], "                ".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
				MemAddr = Convert.ToUInt32(temp[0].Remove(0, 2), 16);
				MemSize = Convert.ToUInt32(temp[1].Remove(0, 2), 16);
				int cnt = 0;
				while(true)
				{
					FileLocation += temp[cnt + 2];
					cnt++;
					if (cnt >= temp.Length-2) break;
					FileLocation += " ";
				}
			}
			else if (split1.Length == 4)
			{
				MemAddr = Convert.ToUInt32(split1[1].Remove(0, 2), 16);
				MemSize = Convert.ToUInt32(split1[2].Remove(0, 2), 16);
				int cnt = 0;
				while (true)
				{
					FileLocation += split1[cnt + 3];
					cnt++;
					if (cnt >= split1.Length-3) break;
					FileLocation += " ";
				}
			}
		}

		MapType SortMapType(string strMapType)
		{
			if (strMapType.Equals("text"))
			{
				return MapType.SYM_TEXT;
			}
			else if (strMapType.Equals("rodata"))
			{
				return MapType.SYM_RODATA;
			}
			else if (strMapType.Equals("ramfunc"))
			{
				return MapType.SYM_RAMFUNC;
			}
			else if (strMapType.Equals("data"))
			{
				return MapType.SYM_DATA;
			}
			else if (strMapType.Equals("bss"))
			{
				return MapType.SYM_BSS;
			}
			else
			{
				return MapType.SYM_NONE;
			}
		}

		public static MapType StringSortMapType(string strInput)
		{
			string[] split1 = strInput.Split(new char[] { "       ".ToCharArray()[0], "                ".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			string[] split2 = split1[0].Split(new char[] { ".".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			if (split2.Length < 2)
				return MapType.SYM_NONE;
			string strMapType = split2[0];

			if (strMapType.Equals("text"))
			{
				return MapType.SYM_TEXT;
			}
			else if (strMapType.Equals("rodata"))
			{
				return MapType.SYM_RODATA;
			}
			else if (strMapType.Equals("ramfunc"))
			{
				return MapType.SYM_RAMFUNC;
			}
			else if (strMapType.Equals("data"))
			{
				return MapType.SYM_DATA;
			}
			else if (strMapType.Equals("bss"))
			{
				return MapType.SYM_BSS;
			}
			else
			{
				return MapType.SYM_NONE;
			}
		}
	}
}
