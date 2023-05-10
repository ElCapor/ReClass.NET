namespace DeClassNet
{
	public class Constants
	{
		public const string ApplicationName = "DeClass.NET";

		public const string ApplicationExecutableName = ApplicationName + ".exe";

		public const string ApplicationVersion = "1.2";

		public const string LauncherExecutableName = ApplicationName + "_Launcher.exe";

		public const string Author = "KN4CK3R";

		public const string HomepageUrl = "https://github.com/DeClassNet/DeClass.NET";

		public const string HelpUrl = "https://github.com/DeClassNet/DeClass.NET/issues";

		public const string PluginUrl = "https://github.com/DeClassNet/DeClass.NET#plugins";

#if RECLASSNET64
		public const string Platform = "x64";

		public const string AddressHexFormat = "X016";
#else
		public const string Platform = "x86";

		public const string AddressHexFormat = "X08";
#endif

		public const string SettingsFile = "settings.xml";

		public const string PluginsFolder = "Plugins";

		public static class CommandLineOptions
		{
			public const string AttachTo = "attachto";

			public const string FileExtRegister = "registerfileext";
			public const string FileExtUnregister = "unregisterfileext";
		}
	}
}
