using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace CommandLineWPFTestApp {
	#region Commandline Parser Model
	[Verb("testapp", HelpText = "Verb for testapp.")]
	public sealed class CommandLineOptions : IDisposable {

		[Option(longName: "ID", shortName: 'i', Required = true, HelpText = "There can be only one")]
		public int ID { get; set; }

		[Option(longName: "Hello", shortName: 'h', Required = true, HelpText = "Hello Darkness my old friend")]
		public String Hello { get; set; }

		#region Dispose
		private bool disposed = false;

		private void Dispose(bool disposing) {
			if (disposing) {
				ID = 0;
				Hello = "";
				disposed = true;
			}
		}

		/// <summary>
		/// Weltraumputze
		/// </summary>
		public void Dispose() {
			Dispose(!disposed);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
	#endregion
	/// <remarks />
	public partial class App : Application {
		#region Properties
		private static StringBuilder MessageSB = new StringBuilder();
		private static StringWriter MessageOut = new StringWriter(MessageSB);
		private CommandLineOptions Options { get; set; }
		#endregion
		protected override void OnStartup(StartupEventArgs e) {
			ParseArgs(e.Args);
			new MainWindow().Show();
		}

		private void ParseArgs(string[] args) {
			// Here comes the IOException when you deactivate "Options -> Debug -> Just my code"
			// Just step over it and watch the output window
			var parser = new Parser(config => {
				config.HelpWriter = MessageOut;
				config.MaximumDisplayWidth = 1000;
				config.EnableDashDash = true;
				config.CaseInsensitiveEnumValues = true;
				config.IgnoreUnknownArguments = false;
			});

			var result = parser.ParseArguments(args, typeof(CommandLineOptions))
				.MapResult(
					(CommandLineOptions opts) => ParseCommands(opts),
					notParsedFunc: errs => RunUsage(errs)
				);
		}
		private object RunUsage(IEnumerable<Error> errs) {
			// Do something... or not
			return null;
		}
		private object ParseCommands(CommandLineOptions opts) {
			// still, do nothing, 
			return null;
		}
	}
}
