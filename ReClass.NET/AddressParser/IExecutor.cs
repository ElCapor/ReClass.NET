using System;
using DeClassNet.Memory;

namespace DeClassNet.AddressParser
{
	public interface IExecutor
	{
		IntPtr Execute(IExpression expression, IProcessReader processReader);
	}
}
