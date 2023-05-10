using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DeClassNet.Logger;
using DeClassNet.MemoryScanner;

namespace DeClassNet.DataExchange.Scanner
{
	[ContractClass(typeof(ScannerExportContract))]
	public interface IScannerExport
	{
		void Save(IEnumerable<MemoryRecord> records, string filePath, ILogger logger);
	}

	[ContractClassFor(typeof(IScannerExport))]
	internal abstract class ScannerExportContract : IScannerExport
	{
		public void Save(IEnumerable<MemoryRecord> records, string filePath, ILogger logger)
		{
			Contract.Requires(records != null);
			Contract.Requires(filePath != null);
			Contract.Requires(logger != null);
		}
	}
}
