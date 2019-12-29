namespace FileOrganizer
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
	using System.Threading;

	internal class FileOperationDeDuplicate : FileOperation
	{
		public override void DoOperation(List<string> folders, Action<string> updateLogFunc, Action updateProgressFunc, CancellationToken token)
		{
			Dictionary<long, string> sizes = new Dictionary<long, string>();
			HashSet<string> filesKnown = new HashSet<string>();
			HashSet<string> hashes = new HashSet<string>();

			foreach (string folder in folders)
			{
				DirectoryInfo diFolder = new DirectoryInfo(folder);
				ProcessDirectory(diFolder, sizes, filesKnown, hashes, updateProgressFunc, updateLogFunc, token);
			}
		}

		private void ProcessDirectory(
			DirectoryInfo diFolder,
			Dictionary<long, string> sizes,
			HashSet<string> filesKnown,
			HashSet<string> hashes,
			Action updateProgressFunc,
			Action<string> updateLogFunc,
			CancellationToken token)
		{
			foreach (FileInfo fiFile in diFolder.EnumerateFiles())
			{
				Thread.Sleep(1000);

				if (token.IsCancellationRequested) return;

				long fileSize = fiFile.Length;
				if (sizes.ContainsKey(fileSize))
				{
					string previousFile = sizes[fileSize];
					if (!filesKnown.Contains(previousFile))
					{
						FileInfo fiPreviousFile = new FileInfo(previousFile);
						string hashPrevious = ComputeHash(fiPreviousFile);
						AddFileHash(filesKnown, hashes, hashPrevious, previousFile);
					}

					string hash = ComputeHash(fiFile);
					if (!hashes.Contains(hash))
					{
						AddFileHash(filesKnown, hashes, hash, previousFile);
					}
					else
					{
						RemoveFile(fiFile, sizes, updateLogFunc);
					}
				}
				else
				{
					sizes[fileSize] = fiFile.FullName;
				}

				updateProgressFunc.Invoke();
			}

			foreach (DirectoryInfo diChild in diFolder.EnumerateDirectories())
			{
				ProcessDirectory(diChild, sizes, filesKnown, hashes, updateProgressFunc, updateLogFunc, token);
			}
		}

		private void RemoveFile(FileInfo fiToRemove, Dictionary<long, string> sizes, Action<string> updateLogFunc)
		{
			fiToRemove.Delete();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"DELETED: '{fiToRemove.FullName}' -- PRESERVED: '{sizes[fiToRemove.Length]}'");
			updateLogFunc.Invoke(sb.ToString());
		}

		private void AddFileHash(HashSet<string> filesComputed, HashSet<string> hashesFound, string hash, string fileName)
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
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}
	}
}
