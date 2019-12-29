namespace FileOrganizer
{
	using System;
	using System.Collections.Generic;
	using System.Threading;

	internal abstract class FileOperation
	{
		public abstract void DoOperation(List<string> folders, Action<string> updateLogFunc, Action updateProgressFunc, CancellationToken token);
	}
}
