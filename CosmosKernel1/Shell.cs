using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys = Cosmos.System;
using System.Diagnostics;

namespace CosmosKernel1 {
	public class Shell {
		//public void MinArgs( int pMinArgs, List<string> pArgs ) {
		//
		//}

		// parse commands while respecting strings
		// NOTE: this doesn't fully work yet, as fo"ob"ar will
		// be split into "fo", "ob" and "ar"
		// also no single qoutes
		
		public static string[] SplitArguments( string word ) {
			List<string> result = new List<string>();
			var split1 = word.Split( '"' );
			for ( int i = 0; i < split1.Length; i++ ) {
				split1[ i ] = split1[ i ].Trim();
			}
			for ( int i = 0; i < split1.Length; i++ ) {
				if ( i % 2 == 0 ) {
					var split2 = split1[ i ].Split( ' ' );
					foreach ( var el in split2 ) {
						result.Add( el );
					}
				} else {
					result.Add( split1[ i ] );
				}

			}
			string[] arr = new string[ result.Count ];
			for ( int i = 0; i < result.Count; i++ ) {
				arr[ i ] = result[ i ];
			}
			return arr;
		}

		// the library functions for conversion are broken in cosmos
		public int StringToInt( string str ) {
			int response = 0;
			foreach ( char c in str ) {
				response *= 10;
				response += c - '0';
			}
			return response;
		}

		private readonly Debug debug = new Debug();
		private History history = new History();
		private readonly Os os = new Os();
		private Dictionary<string, string> variables = new Dictionary<string, string>();
		private int retval = 0; // return value; 0 = all good, everything higher = error

		// bundle most builtin commands into a class
		public static class Builtins {
			// perfom mathmatical calculations
			//public void CalculateResoult( string[] pInput ) {
			//	
			//}

			//public static void ListItems() {
			//	Os.Print( Sys.FileSystem. );
			//}

			// print text to the console
			public static void PrintText( string[] input ) {
				string output = "";

				if ( input.Length <= 0 )
					Os.Print( "" );

				for ( int i = 1; i < input.Length - 1; i++ )
					output += input[ i ] + " ";

				output += input[ input.Length - 1 ];

				Os.Print( output );
			}

			// read a string from input, then print it
			public static void ReadText( string pPrompt = "> " ) {
				if ( pPrompt != "" )
					Console.Write( pPrompt );
				string read = Console.ReadLine();
				Console.WriteLine( read );
			}
		}

		public int Run() {
			Console.Write( "Welcome to the " );
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write( "Cosmo" );
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write( "Shell" );
			Console.ResetColor();
			Console.WriteLine( "!" );

			while ( true ) {
				// show the interactive prompt
				if ( retval > 0 ) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write( retval+" " );
					retval = 0;
				}
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write( "shell> " );
				Console.ResetColor();
				string input_str = Console.ReadLine();
				string[] input = SplitArguments( input_str );
				debug.PrintCommand( input );

				List<string> _args = new List<string>();
				for ( int i = 0; i < input.Length - 1; i++ ) {
					_args.Add( input[ i + 1 ] );
				}
				string[] args = _args.ToArray();

				// append typed command to history
				history.Add( input_str );

				/* if ( ( input[ 0 ] == "exit" ) || ( input[ 0 ] == "QuitSession" ) ) {
					try {
						if ( input.Length > 1 ) {
							return StringToInt( input[ 1 ] );
						}
						return 0;
					} catch {
						debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
						retval = 1;
					}
				} */

				switch ( input[ 0 ] ) {
					case "clear":
					case "ClearScreen":
						try {
							Console.Clear();
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "echo":
					case "PrintText":
						try {
							if ( input.Length > 1 )
								Builtins.PrintText( input );
							else
								Builtins.PrintText( new[] { "" } );
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "hist":
					case "PrintHistory":
						try {
							// print all commands stored in history
							// (this cannot be easily made into a builtin)
							history.Print();
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "exit":
					case "QuitSession":
						try {
							Console.WriteLine( "Goodbye!" );
							if ( input.Length > 1 ) {
								return StringToInt( input[ 1 ] );
							}
							return 0;
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "read":
					case "ReadText":
						try {
							if ( input.Length > 1 ) {
								Builtins.ReadText( input[ 1 ] );
								break;
							}
							Builtins.ReadText();
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "poweroff":
					case "ShutdownSystem":
						try {
							Sys.Power.Shutdown();
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "reboot":
					case "RebootSystem":
						try {
							Sys.Power.Reboot();
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "return":
					case "SetReturnvalue":
						try {
							if ( input.Length > 1 ) {
								retval = StringToInt( input[ 1 ] );
							}
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "set":
					case "SetVariable":
						try {
							variables.Add( input[ 1 ], input[ 2 ] );
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "get":
					case "GetVariable":
						try {
	#if DEBUG
							Console.WriteLine( "Key: {0}, Value: {1}", input[ 1 ], variables[ input[ 1 ] ] );
	#else
							Console.WriteLine( variables[ input[ 1 ] ] );
	#endif
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					case "getspace":
					case "GetSpaceavail":
						try {
							Dictionary<string, object> output;
							if ( args.Length <= 0 ) {
								output = Filesystem.GetSpaceavail( "0:/", "MiB" );
							} else if ( args.Length <= 1 ) {
								output = Filesystem.GetSpaceavail( args[ 0 ], "MiB" );
							} else {
								output = Filesystem.GetSpaceavail( args[ 0 ], args[ 1 ] );
							}
							Os.Print( output[ "space" ] + " " + output[ "type" ] );
						} catch {
							debug.Print( string.Format( "Failed to run '{0}'!", input[ 0 ] ) );
							retval = 1;
						}
						break;
					default:
						if ( input.Length > 0 ) {
							Os.Print( string.Format( "Unrecognized command: '{0}'", input[ 0 ] ) );
							retval = 1;
						}
						break;
				}
			}

			return 0;
		}
	}
}
