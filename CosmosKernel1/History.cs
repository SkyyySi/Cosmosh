using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosKernel1 {
	public class History {
		public List<string> history = new List<string>();

		public void Add( string pValue ) {
			history.Add( pValue );
		}

		public void Print() {
			for ( int i = 0; i < history.ToArray().Length - 1; i++ ) {
				Console.WriteLine( "{0}: {1}", i, history[ i ] );
			}
		}
	}
}
