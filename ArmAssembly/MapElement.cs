using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ArmAssembly
{
	public class MapContainer
	{
		public delegate void UpdateProgress(int value);
		public string[] AllStrings;
		public DataSet MapDataSet;
		public List<MapElements> ElementList;

		public MapContainer(string FileName)
		{
			ElementList = new List<MapElements>();
			AllStrings = File.ReadAllLines(FileName);
			int mainCnt = 0;

			// Memory Configuration 영역 이전의 문자열 모두 무시
			for (; mainCnt < AllStrings.Length; mainCnt++)
			{
				string str = AllStrings[mainCnt];
				if (str.Equals("Memory Configuration"))
				{
					break;
				}
			}
			for (; mainCnt < AllStrings.Length; mainCnt++)
			{
				string str = AllStrings[mainCnt];
				// 공백이 아닌 행을 만날 때만 처리
				if (!str.Replace(" ", string.Empty).Equals(""))
				{
					// 현재 문자열이 메모리 위치 정보를 가지고 있을 때만 처리
					if (MapElements.StringSortMapType(str) != MapElements.MapType.SYM_NONE)
					{
						int subCnt = 0;
						// 다음 문자 선택
						string strLoop = AllStrings[mainCnt + 1];
						// 메모리 위치 정보를 가진 문자열이 나올 때까지 루프 (subCnt로 문자열 참조)
						while (ArmAssembly.MapElements.StringSortMapType(strLoop) == ArmAssembly.MapElements.MapType.SYM_NONE)
						{
							subCnt++;
							if (mainCnt + 1 + subCnt == AllStrings.Length)		// 문자열 전체를 다 돌았으면 루프를 빠져나감
								break;
							if (!AllStrings[mainCnt + 1 + subCnt].Equals(""))	// 공백이 아닌 행을 다음 while비교문 문자열로 설정
							{
								strLoop = AllStrings[mainCnt + 1 + subCnt];
							}
						}
						// 문자열 전체를 다 돌아서 루프를 마친게 아니면 심볼을 추출하기 위한 처리 시작
						if (subCnt != 0)
						{
							string[] arg = new string[subCnt + 1];
							int target_cnt = mainCnt + 1 + subCnt;
							// 위 루프에서 확인한 개수의 문자열을 arg에 저장
							for (int argCnt = 0; mainCnt < target_cnt; mainCnt++)
							{
								if (!AllStrings[mainCnt].Equals(""))
								{
									arg[argCnt] = AllStrings[mainCnt];
									argCnt++;
								}
							}
							MapElements element = new MapElements(arg);
							ElementList.Add(element);
							mainCnt--;
						}
					}
				}
			}

			MapDataSet = new DataSet();
			MapDataSet.Tables.Add(UserConvert.ToDataTable(ElementList, "MapTable"));
		}
		public MapContainer(string FileName, UpdateProgress Update)
		{
			ElementList = new List<MapElements>();
			AllStrings = File.ReadAllLines(FileName);
			int cnt = 0;
			
			for (; cnt < AllStrings.Length; cnt++)
			{
				string str = AllStrings[cnt];
				if(str.Equals("Memory Configuration"))
				{
					break;
				}
			}
			for (; cnt < AllStrings.Length; cnt++)
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
			Update((cnt + 1) * 100 / AllStrings.Length);

			MapDataSet = new DataSet();
			MapDataSet.Tables.Add(UserConvert.ToDataTable(ElementList, "MapTable"));
			Update(0);
		}
	}

	public class MapElements
	{
		static uint refCount = 0;
		public enum MapType
		{
			SYM_VECTOR,
			SYM_TEXT,
			SYM_RODATA,
			SYM_RAMFUNC,
			SYM_DATA,
			SYM_BSS,
			SYM_COMMON,
			SYM_STACK,
			SYM_NONE
		};
		uint IndexNum;
		MapType Type;
		string SymbolName;
		uint MemAddr;
		uint MemSize;
		string FileLocation;

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
				switch(Type)
				{
					case MapType.SYM_VECTOR: return "vectors";
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
		public uint Memory
		{
			get
			{
				return MemAddr;
			}
		}
		public uint Size
		{
			get
			{
				return MemSize;
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
			refCount++;
			IndexNum = refCount;
			// .(type).(symbolname)으로 시작하는 첫번째 문자열 => 최소 메모리 영역에 대한 정보를 가지고 있음
			string[] split1 = input[0].Split(new char[] { "       ".ToCharArray()[0], "                ".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			// 메모리 영역과 심볼을 분리
			string[] split2 = split1[0].Split(new char[] { ".".ToCharArray()[0] }, StringSplitOptions.RemoveEmptyEntries);
			Type = SortMapType(split2[0]);
			if(split2.Length > 1)
			{
				SymbolName = split2[1];
			}
			else
			{
				SymbolName = "";
			}
			
			// 첫번째 문자열에 memory area와 symbol 정보만 있는 경우
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
				if(SymbolName.Equals("") && (input.Length > 2))
				{
					string[] temp2 = input[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (!temp2[0].Equals("*fill*"))
						SymbolName = temp2[temp2.Length - 1];
				}
			}
			// 첫번째 문자열에 memory area와 symbol 외 size, path 저오도 있는 경우
			else if (split1.Length >= 3)
			{
				MemAddr = Convert.ToUInt32(split1[1].Remove(0, 2), 16);
				MemSize = Convert.ToUInt32(split1[2].Remove(0, 2), 16);
				if(split1.Length >= 4)
				{
					int cnt = 0;
					while (true)
					{
						FileLocation += split1[cnt + 3];
						cnt++;
						if (cnt >= split1.Length - 3) break;
						FileLocation += " ";
					}
					if (SymbolName.Equals("") && (input.Length > 1))
					{
						string[] temp2 = input[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						if (!temp2[0].Equals("*fill*"))
							SymbolName = temp2[temp2.Length - 1];
					}
				}
			}
		}
		~MapElements()
		{
			refCount--;
		}
		MapType SortMapType(string strMapType)
		{
			if (strMapType.Equals("text"))
			{
				return MapType.SYM_TEXT;
			}
			else if (strMapType.Equals("vectors"))
			{
				return MapType.SYM_VECTOR;
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
//			if (split2.Length < 2)
//				return MapType.SYM_NONE;
			string strMapType = split2[0];

			if (strMapType.Equals("text"))
			{
				return MapType.SYM_TEXT;
			}
			else if (strMapType.Equals("vectors"))
			{
				return MapType.SYM_VECTOR;
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
