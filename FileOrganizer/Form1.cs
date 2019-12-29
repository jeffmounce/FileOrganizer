﻿namespace FileOrganizer
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	public partial class MainForm : Form
	{
		private readonly Dictionary<string, bool> _directories = new Dictionary<string, bool>();
		private readonly HashSet<FileOperation> _operations = new HashSet<FileOperation>();

		public MainForm()
		{
			InitializeComponent();
		}

		private void lstFolders_DragDrop(object sender, DragEventArgs e)
		{
			string[] newFolders = e.Data.GetData(DataFormats.FileDrop, false) as string[];

			txtLog.Clear();

			AddNewFolders(newFolders);

			ClearList();

			RenderFolders();
		}

		private void RenderFolders()
		{
			int index = 0;
			foreach (string folder in _directories.Keys.OrderBy(_ => _))
			{
				bool selected = _directories[folder];

				lstFolders.Items.Add(folder);
				lstFolders.SetItemChecked(index, selected);

				index++;
			}
		}

		private void AddNewFolders(string[] newFolders)
		{
			List<string> duplicates = new List<string>();

			if (newFolders != null)
			{
				foreach (string folder in newFolders.OrderBy(_ => _))
				{
					if (_directories.ContainsKey(folder))
					{
						duplicates.Add(folder);
					}
					else
					{
						if (!IsChildOfAny(folder))
						{
							_directories[folder] = true;
						}
						else
						{
							duplicates.Add(folder);
						}
					}
				}
			}

			if (duplicates.Any())
			{
				StringBuilder msgDuplicates = new StringBuilder();
				msgDuplicates.AppendLine("The following directories are duplicates of existing folders and were not added:");
				foreach (string duplicate in duplicates)
				{
					msgDuplicates.AppendLine(duplicate);
				}
				txtLog.AppendText(msgDuplicates.ToString());
			}
		}

		private bool IsChildOfAny(string folder)
		{
			DirectoryInfo diFolder = new DirectoryInfo(folder);

			HashSet<string> parents = new HashSet<string>();

			DirectoryInfo diParent = diFolder.Parent;
			while (diParent != null)
			{
				parents.Add(diParent.FullName);
				diParent = diParent.Parent;
			}

			IEnumerable<string> possibleParents = _directories.Keys;

			return parents.Intersect(possibleParents).Any();
		}

		private void lstFolders_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false)
				? DragDropEffects.All
				: DragDropEffects.None;
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			ClearList();
		}

		private void ClearList()
		{
			lstFolders.Items.Clear();
		}

		private void startOperation_Click(object sender, System.EventArgs e)
		{
			_operations.Add(new FileOperationDeDuplicate());

			chkShowLog.Checked = true;

			List<string> folders = new List<string>();
			foreach (string key in _directories.Keys)
			{
				if (_directories[key])
				{
					folders.Add(key);
				}
			}

			int fileCount = GetFileCount(folders);
			progressStart.Maximum = fileCount;
			progressStart.Step = 1;
			progressStart.Visible = true;

			foreach (FileOperation fileOperation in _operations)
			{
				Task myTask = Task.Run(() => fileOperation.DoOperation(folders, UpdateLog, UpdateProgress));
			}
		}

		private int GetFileCount(List<string> folders)
		{
			int retVal = 0;

			foreach (string folder in folders)
			{
				DirectoryInfo directory = new DirectoryInfo(folder);
				retVal += GetFileCount(directory);
			}

			return retVal;
		}

		private int GetFileCount(DirectoryInfo directory)
		{
			int retVal = 0;

			retVal += directory.EnumerateFiles().Count();

			foreach (DirectoryInfo directoryInfo in directory.EnumerateDirectories())
			{
				retVal += GetFileCount(directoryInfo);
			}

			return retVal;
		}

		private void chkShowLog_CheckedChanged(object sender, System.EventArgs e)
		{
			lstFolders.Visible = !chkShowLog.Checked;
			txtLog.Visible = chkShowLog.Checked;

			if (progressStart.Visible)
			{
				progressStart.Visible = false;
			}
		}

		private void lstFolders_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			int index = e.Index;
			string folder = lstFolders.Items[index] as string;
			Debug.Assert(folder != null, nameof(folder) + " != null");
			_directories[folder] = e.NewValue == CheckState.Checked;
		}

		private delegate void ProgressDelegate();

		private void UpdateProgress()
		{
			if (progressStart.InvokeRequired)
			{
				ProgressDelegate d = new ProgressDelegate(UpdateProgress);
				progressStart.Invoke(d);
			}
			else
			{
				progressStart.PerformStep();
			}
		}

		private delegate void WriteLogDelegate(string toWrite);

		private void UpdateLog(string toWrite)
		{
			if (txtLog.InvokeRequired)
			{
				WriteLogDelegate d = new WriteLogDelegate(UpdateLog);
				txtLog.Invoke(d, new object[] {toWrite});
			}
			else
			{
				txtLog.AppendText(toWrite);
			}
		}
	}
}
