using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmAssembly
{
	public class ArmAsmDataBase
	{
		public delegate void ProgressBarUpdate(object obj, ProgressChangedEventArgs arg);
		enum LoadType
		{
			MAP_LOADER,
			LSS_LOADER,
			NONE
		};

		BackgroundWorker LoadWorker;
		BindingSource MapBindingSource;
		LssContainer LssList;
		MapContainer MapList;
		bool bProgressBar;

		public BackgroundWorker BackgroundLoader
		{
			get
			{
				return LoadWorker;
			}
		}
		public BindingSource MapSource
		{
			get
			{
				return MapBindingSource;
			}
		}

		public ArmAsmDataBase(ProgressBarUpdate progress = null)
		{
			MapBindingSource = new BindingSource();

			LoadWorker = new BackgroundWorker();
			LoadWorker.WorkerReportsProgress = true;
			LoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MakeComponentsComplete);
			LoadWorker.DoWork += new DoWorkEventHandler(MakeComponents);
			
			if (progress != null)
			{
				bProgressBar = true;
				LoadWorker.ProgressChanged += new ProgressChangedEventHandler(progress);
			}
			else
			{
				bProgressBar = false;
			}
		}
		/// <summary>
		/// map, lss 데이터 업데이트 관련 async callback 함수
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="arg"></param>
		private void MakeComponents(object obj, DoWorkEventArgs arg)
		{
			string FilePath = (string)arg.Argument;
			string[] split = FilePath.Split(new char[] { '.' });
			LoadType type = LoadType.NONE;

			if(split[split.Length-1].Equals("map"))
				type = LoadType.MAP_LOADER;
			else if (split[split.Length - 1].Equals("lss"))
				type = LoadType.LSS_LOADER;
			else
				type = LoadType.NONE;

			if (bProgressBar)
			{
				if (type == LoadType.MAP_LOADER)
					MapList = new MapContainer(FilePath, new MapContainer.UpdateProgress(LoadWorker.ReportProgress));
				if (type == LoadType.LSS_LOADER)
					LssList = new LssContainer(FilePath, new LssContainer.UpdateProgress(LoadWorker.ReportProgress));
			}
			else
			{
				if (type == LoadType.MAP_LOADER)
					MapList = new MapContainer(FilePath);
				if (type == LoadType.LSS_LOADER)
					LssList = new LssContainer(FilePath);
			}

			arg.Result = type;
		}
		private void MakeComponentsComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			if ((LoadType)arg.Result == LoadType.MAP_LOADER)
			{
				MapBindingSource.DataSource = MapList.MapDataSet;
				MapBindingSource.DataMember = MapList.MapDataSet.Tables[0].TableName;
			}
		}

		/// <summary>
		/// lss 데이터 중에서 특정 메모리관련 정보를 object[]로 리턴
		/// </summary>
		/// <param name="memory"></param>
		/// <returns></returns>
		public MemInfo GetMemoryRow(string memory)
		{
			uint uiMemory = Convert.ToUInt32(memory, 16);
			MapElements mapData = MapList.ElementList.Find(x => (x.Memory <= uiMemory) && (uiMemory < (x.Memory + x.Size)));
			LssElements lssData = LssList.ElementList.Find(x => (x.Memory == uiMemory));

			if (lssData != null)
			{
				MemInfo retValue = new MemInfo(lssData.Memory.ToString("X"), lssData.Symbol, lssData.Parameter, mapData.Area);
				return retValue;
			}
			else if (mapData != null)
			{
				MemInfo retValue = new MemInfo(mapData.Memory.ToString("X"), mapData.Symbol, "0", mapData.Area);
				return retValue;
			}
			else
				return null;
		}

		public LssContainer FindMatchingLss(uint ItemAddress, ref int StartIndex, ref int EndIndex)
		{
			int SymbolIndex = LssList.SymbolList.FindIndex(x => (x.Memory == ItemAddress));
			if (SymbolIndex < 0)
			{
				return null;
			}

			StartIndex = LssList.ElementList.FindIndex(x => (x.Memory == LssList.SymbolList[SymbolIndex].Memory));
			if(StartIndex < 0)
			{
				return null;
			}

			if (SymbolIndex != LssList.SymbolList.Count - 1)
			{
				EndIndex = LssList.ElementList.FindIndex(x => (x.Memory == LssList.SymbolList[SymbolIndex + 1].Memory));
				if(EndIndex < 0)
				{
					return null;
				}
			}
			else
			{
				EndIndex = LssList.ElementList.Count;
			}

			return LssList;
		}
	}
}
