using System.Text;
using NFluent;
using DeClassNet.Extensions;
using Xunit;

namespace DeClass.NET_Tests.Extensions
{
	public class StringBuilderExtensionTest
	{
		[Fact]
		public void TestPrependChar()
		{
			var sut = new StringBuilder("test");
			sut.Prepend('x');

			Check.That(sut.ToString()).IsEqualTo("xtest");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("test")]
		public void TestPrependString(string value)
		{
			var sut = new StringBuilder("test");
			sut.Prepend(value);

			Check.That(sut.ToString()).IsEqualTo(value + "test");
		}
	}
}
