using System;

namespace DeClassNet.Debugger
{
	public class NoHardwareBreakpointAvailableException : Exception
	{
		public NoHardwareBreakpointAvailableException()
			: base("All available hardware breakpoints are already set.")
		{

		}
	}
}
