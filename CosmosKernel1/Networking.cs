using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Network.IPv4;

namespace CosmosKernel1 {
	public class Networking {
		private static readonly Address ip;
		private static readonly Address subnet;
		public Config connection;

		// create a new connection with defaults
		public Networking() {
			Address ip = new Address( 192, 168, 178, 120 );
			Address subnet = new Address( 255, 255, 255, 0 );
			Config connection = new Config( ip, subnet );
		}

		// create a new connection with a custom adress
		public Networking( Address pIp ) {
			Address ip = pIp;
			Address subnet = new Address( 255, 255, 255, 0 );
			Config connection = new Config( ip, subnet );
		}
		
		// create a new connection with a custom adress and subnet mask
		public Networking( Address pIp, Address pSubnet ) {
			Address ip = pIp;
			Address subnet = pSubnet;
			Config connection = new Config( ip, subnet );
		}

		// retunrs the properties of the connection-object
		public object GetProps() {
			return connection.GetType().GetProperties();
		}

		//Console.WriteLine( connection.GetType().GetProperties() );
	}
}
