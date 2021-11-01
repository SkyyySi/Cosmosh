using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;

namespace CosmosKernel1 {
	public class Kernel: Sys.Kernel {
		public Shell shell = new Shell();
		//public Networking networking = new Networking();

		protected override void BeforeRun() {
			// set keyboard layout to german
			Sys.KeyboardManager.SetKeyLayout( new Sys.ScanMaps.DE_Standard() );

			// set up a filesystem
			Filesystem.Init();

			// set up the network connection
			//Console.WriteLine( networking.GetProps() );

			// print some text once everything is loaded
			Console.WriteLine();
			Console.WriteLine( "Keymap: German (DE)" );
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write( "Cosmos" );
			Console.ResetColor();
			Console.WriteLine( " boot complete!" );
			Console.WriteLine();

			// set up graphics
			//Display.Init();
		}

		protected override void Run() {
			// run the shell in an infinite loop
			//shell.Run();
			Console.WriteLine( Console.ReadKey().Key );

			//Display.Update();
        }
	}
}
