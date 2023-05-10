using System;

namespace DeClassNet.AddressParser
{
	public class ParseException : Exception
	{
		public ParseException(string message)
			: base(message)
		{

		}
	}
}
