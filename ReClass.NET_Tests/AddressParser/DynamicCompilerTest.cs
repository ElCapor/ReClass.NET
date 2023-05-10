using DeClassNet.AddressParser;

namespace DeClass.NET_Tests.AddressParser
{
	public class DynamicCompilerTest : ExecutorTest
	{
		protected override IExecutor CreateExecutor()
		{
			return new DynamicCompiler();
		}
	}
}
