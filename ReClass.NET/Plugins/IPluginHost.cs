using System;
using System.Diagnostics.Contracts;
using System.Resources;
using DeClassNet.Forms;
using DeClassNet.Logger;
using DeClassNet.Memory;

namespace DeClassNet.Plugins
{
	[ContractClass(typeof(PluginHostContract))]
	public interface IPluginHost
	{
		/// <summary>Gets the main window of DeClass.NET.</summary>
		MainForm MainWindow { get; }

		/// <summary>Gets the resources of DeClass.NET.</summary>
		ResourceManager Resources { get; }

		/// <summary>Gets the process DeClass.NET is attached to.</summary>
		RemoteProcess Process { get; }

		/// <summary>Gets the logger DeClass.NET is using.</summary>
		ILogger Logger { get; }

		/// <summary>Gets the settings DeClass.NET is using.</summary>
		Settings Settings { get; }
	}

	[ContractClassFor(typeof(IPluginHost))]
	internal abstract class PluginHostContract : IPluginHost
	{
		public ILogger Logger
		{
			get
			{
				Contract.Ensures(Logger != null);

				throw new NotImplementedException();
			}
		}

		public MainForm MainWindow
		{
			get
			{
				Contract.Ensures(MainWindow != null);

				throw new NotImplementedException();
			}
		}

		public RemoteProcess Process
		{
			get
			{
				Contract.Ensures(Process != null);

				throw new NotImplementedException();
			}
		}

		public ResourceManager Resources
		{
			get
			{
				Contract.Ensures(Resources != null);

				throw new NotImplementedException();
			}
		}

		public Settings Settings
		{
			get
			{
				Contract.Ensures(Settings != null);

				throw new NotImplementedException();
			}
		}
	}
}
