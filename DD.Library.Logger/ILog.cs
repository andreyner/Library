using System;

namespace DD.Library.Logger
{
	public interface ILog
	{
		void Information(string message);
		void Warning(string message);
		void Debug(string message);
		void Error(string message);
	}
}
