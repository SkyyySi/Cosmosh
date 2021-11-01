using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosKernel1 {
	public class Os {
		public static void Print( string text = "", bool newline = true ) {
			if ( text is null ) {
				throw new ArgumentNullException( nameof( text ) );
			}

			Console.Write( text );
			if ( newline ) {
				Console.WriteLine();
			}
		}
	}
}
