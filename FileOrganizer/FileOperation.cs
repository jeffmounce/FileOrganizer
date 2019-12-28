namespace FileOrganizer
{
	using System.Collections.Generic;

	internal abstract class FileOperation
	{
		public abstract void DoOperation(List<string> folders);
	}
}
