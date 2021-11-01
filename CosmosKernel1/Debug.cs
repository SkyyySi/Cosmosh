using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosKernel1 {
	public class Debug {
		private readonly string head;
		private readonly string foot;

		public Debug( string head = "--- DEBUG ---", string foot = "--- DEBUG END ---" ) {
			this.head = head;
			this.foot = foot;
		}

		public void Head() {
			Console.WriteLine();
			Console.WriteLine( head );
			Console.WriteLine();
		}

		public void Foot() {
			Console.WriteLine();
			Console.WriteLine( foot );
			Console.WriteLine();
		}

		public void Print( string msg ) {
#if DEBUG
			Head();
			Console.WriteLine( msg );
			Foot();
#else
			return;
#endif
		}

		public void PrintArgs( string[] args ) {
#if DEBUG
			Head();
			for ( int i = 0; i < args.Length; i++ ) {
				Console.WriteLine( "Key: {0}, Value: {1}", i, args[ i ] );
			}
			Foot();
#else
			return;
#endif
		}

		public void PrintCommand( string[] args ) {
#if DEBUG
			Head();
			if ( args.Length > 0 ) {
				Console.WriteLine( "Command: {0}", args[ 0 ] );
			}

			if ( args.Length > 1 ) {
				Console.Write( "Arguments: " );
				for ( int i = 1; i < args.Length - 1; i++ ) {
					Console.Write( "{0}: '{1}', ", i, args[ i ] );
				}
				Console.Write( "{0}: '{1}'", args.Length - 1, args[ args.Length - 1 ] );
				Console.WriteLine();
			}
			Foot();
#else
			return;
#endif
		}
	}
}
