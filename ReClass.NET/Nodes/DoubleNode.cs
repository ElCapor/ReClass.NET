using System.Drawing;
using DeClassNet.Controls;
using DeClassNet.Extensions;
using DeClassNet.Memory;
using DeClassNet.UI;

namespace DeClassNet.Nodes
{
	public class DoubleNode : BaseNumericNode
	{
		public override int MemorySize => 8;

		public override void GetUserInterfaceInfo(out string name, out Image icon)
		{
			name = "Double";
			icon = Properties.Resources.B16x16_Button_Double;
		}

		public override Size Draw(DrawContext context, int x, int y)
		{
			return DrawNumeric(context, x, y, context.IconProvider.Double, "Double", ReadValueFromMemory(context.Memory).ToString("0.000"), null);
		}

		public override void Update(HotSpot spot)
		{
			base.Update(spot);

			if (spot.Id == 0)
			{
				if (double.TryParse(spot.Text, out var val))
				{
					spot.Process.WriteRemoteMemory(spot.Address, val);
				}
			}
		}

		public double ReadValueFromMemory(MemoryBuffer memory)
		{
			return memory.ReadDouble(Offset);
		}
	}
}
