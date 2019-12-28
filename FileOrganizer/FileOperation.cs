namespace FileOrganizer
{
	using System.Collections.Generic;
	using System.Windows.Forms;

	internal abstract class FileOperation
	{
		public abstract void DoOperation(List<string> folders, TextBox txtLog);
	}
}
