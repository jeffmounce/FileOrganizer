namespace FileOrganizer
{
	using System;
	using System.Collections.Generic;

	internal abstract class FileOperation
	{
		public abstract void DoOperation(List<string> folders, Action<string> updateLogFunc, Action updateProgressFunc);
	}
}
