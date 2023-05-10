using System;
using System.Collections.Generic;
using DeClassNet.Extensions;

namespace DeClassNet.Util
{
	public class IntPtrComparer : IComparer<IntPtr>
	{
		public static IntPtrComparer Instance { get; } = new IntPtrComparer();

		public int Compare(IntPtr x, IntPtr y)
		{
			return x.CompareTo(y);
		}
	}
}
