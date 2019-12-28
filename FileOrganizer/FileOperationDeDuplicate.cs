namespace FileOrganizer
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
	using System.Windows.Forms;

	internal class FileOperationDeDuplicate : FileOperation
	{
		public override void DoOperation(List<string> folders, TextBox txtLog)
		{
			HashSet<FileInfo> filesToRemove = new HashSet<FileInfo>();

			Dictionary<long, string> sizes = new Dictionary<long, string>();
			HashSet<string> filesKnown = new HashSet<string>();
			HashSet<string> hashes = new HashSet<string>();

			foreach (string folder in folders)
			{
				DirectoryInfo diFolder = new DirectoryInfo(folder);

				ProcessDirectory(diFolder, sizes, filesKnown, hashes, filesToRemove);

				foreach (DirectoryInfo diChild in diFolder.EnumerateDirectories())
				{
					ProcessDirectory(diChild, sizes, filesKnown, hashes, filesToRemove);
				}
			}

			foreach (FileInfo fiToRemove in filesToRemove)
			{
				fiToRemove.Delete();
				StringBuilder sb = new StringBuilder();
				sb.AppendLine($"DELETED: '{fiToRemove.FullName}' -- PRESERVED: '{sizes[fiToRemove.Length]}'");
				txtLog.AppendText(sb.ToString());
			}
		}

		private void ProcessDirectory(DirectoryInfo diFolder, Dictionary<long, string> sizeByFilepath, HashSet<string> filesComputed, HashSet<string> hashesFound, HashSet<FileInfo> fileToRemove)
		{
			foreach (FileInfo fiFile in diFolder.EnumerateFiles())
			{
				long fileSize = fiFile.Length;
				if (sizeByFilepath.ContainsKey(fileSize))
				{
					string previousFile = sizeByFilepath[fileSize];
					if (!filesComputed.Contains(previousFile))
					{
						FileInfo fiPreviousFile = new FileInfo(previousFile);
						string hashPrevious = ComputeHash(fiPreviousFile);
						AddFileHash(filesComputed, hashesFound, hashPrevious, previousFile);
					}

					string hash = ComputeHash(fiFile);
					if (!hashesFound.Contains(hash))
					{
						AddFileHash(filesComputed, hashesFound, hash, previousFile);
					}
					else
					{
						fileToRemove.Add(fiFile);
					}
				}
				else
				{
					sizeByFilepath[fileSize] = fiFile.FullName;
				}
			}
		}

		private static void AddFileHash(HashSet<string> filesComputed, HashSet<string> hashesFound, string hash, string fileName)
		{
			hashesFound.Add(hash);
			filesComputed.Add(fileName);
		}

		private string ComputeHash(FileInfo fiFile)
		{
			using (MD5 md5 = MD5.Create())
			{
				using (FileStream stream = File.OpenRead(fiFile.FullName))
				{
					byte[] hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();;
				}
			}
		}
	}
}
