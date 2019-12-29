namespace FileOrganizer
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	public partial class MainForm : Form
	{
		private readonly Dictionary<string, bool> _directories = new Dictionary<string, bool>();

		public List<string> Folders { get; set; }

		private List<FileOperation> _operations;
		private int _idxOperation;

		public Task MyTask { get; set; }
		public CancellationTokenSource MyCancelToken { get; set; }

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
			ClearDirectories();
		}

		private void ClearDirectories()
		{
			_directories.Clear();
		}

		private void ClearList()
		{
			lstFolders.Items.Clear();
		}

		private void startOperation_Click(object sender, System.EventArgs e)
		{
			if (_directories.Count == 0) return;

			Folders = DetermineFolders();

			PrepareProgressBar();

			StartOperation();

			MyCancelToken = new CancellationTokenSource();

			RunOperations();
		}

		private void cancelOperation_Click(object sender, System.EventArgs e)
		{
			MyCancelToken.Cancel(false);

			StopOperation();
		}

		private List<string> DetermineFolders()
		{
			List<string> folders = new List<string>();
			foreach (string key in _directories.Keys)
			{
				if (_directories[key])
				{
					folders.Add(key);
				}
			}

			return folders;
		}

		private void PrepareProgressBar()
		{
			int fileCount = GetFileCount(Folders);
			progressStart.Maximum = fileCount;
			progressStart.Step = 1;
			progressStart.Value = 0;
		}

		private void StartOperation()
		{
			txtLog.Clear();
			startOperation.Enabled = false;
			chkShowLog.Checked = true;
			cancelOperation.Visible = true;
			progressStart.Visible = true;
		}

		private void RunOperations()
		{
			_operations = new List<FileOperation>
			{
				new FileOperationDeDuplicate()
			};

			_idxOperation = 0;

			DoNextOperation(MyTask);
		}

		private void DoNextOperation(Task myTask)
		{
			if (_idxOperation < _operations.Count)
			{
				FileOperation fileOperation = _operations[_idxOperation];
				CancellationToken cancellationToken = MyCancelToken.Token;
				MyTask = Task.Run(() => fileOperation.DoOperation(Folders, UpdateLog, UpdateProgress, cancellationToken), cancellationToken);
				MyTask.ContinueWith(DoNextOperation, cancellationToken);
				_idxOperation++;
			}
			else
			{
				StopOperation();
			}
		}

		private delegate void StopOperationDelegate();

		private void StopOperation()
		{
			if (cancelOperation.InvokeRequired)
			{
				StopOperationDelegate d = StopOperation;
				cancelOperation.Invoke(d);
			}
			else
			{
				cancelOperation.Visible = false;
				startOperation.Enabled = true;

				Folders = null;
				MyTask = null;
				_operations = null;
				_idxOperation = 0;
				MyCancelToken = null;
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
				ProgressDelegate d = UpdateProgress;
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
				WriteLogDelegate d = UpdateLog;
				txtLog.Invoke(d, toWrite);
			}
			else
			{
				txtLog.AppendText(toWrite);
			}
		}
	}
}
