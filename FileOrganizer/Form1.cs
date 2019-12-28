namespace FileOrganizer
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	public partial class MainForm : Form
	{
		private readonly Dictionary<string, bool> _directories = new Dictionary<string, bool>();

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

		}

		private void chkShowLog_CheckedChanged(object sender, System.EventArgs e)
		{
			lstFolders.Visible = !chkShowLog.Checked;
			txtLog.Visible = chkShowLog.Checked;
		}

		private void lstFolders_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			int index = e.Index;
			string folder = lstFolders.Items[index] as string;
			Debug.Assert(folder != null, nameof(folder) + " != null");
			_directories[folder] = e.NewValue == CheckState.Checked;
		}
	}
}
