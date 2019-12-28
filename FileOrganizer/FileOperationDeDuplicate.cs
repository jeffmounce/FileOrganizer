namespace FileOrganizer
{
	using System.Collections.Generic;
	using System.IO;

	internal class FileOperationDeDuplicate : FileOperation
	{
		public override void DoOperation(List<string> folders)
		{
			Dictionary<long, string> sizeByFilepath = new Dictionary<long, string>();
			HashSet<string> hashesFound = new HashSet<string>();

			foreach (string folder in folders)
			{
				DirectoryInfo diFolder = new DirectoryInfo(folder);
				foreach (FileInfo fiFile in diFolder.EnumerateFiles())
				{
					long fileSize = fiFile.Length;
					if (sizeByFilepath.ContainsKey(fileSize))
					{
						string previousFile = sizeByFilepath[fileSize];
						if(!hashesFound.Contains(previousFile))
						string hash = ComputeHash(fiFile);
					}
					else
					{
						sizeByFilepath[fileSize] = fiFile.FullName;
					}
				}
			}
			Dictionary<string, List<string>> folderHashes = GatherFileHashes(folders);
		}

		private string ComputeHash(FileInfo fiFile)
		{
			
		}

		private Dictionary<string, List<string>> GatherFileHashes(List<string> folders)
		{
			Dictionary<string, List<string>> fileHashesByFiles = new Dictionary<string, List<string>>();


			return fileHashesByFiles;
		}
	}
}
