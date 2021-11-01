using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;

namespace CosmosKernel1 {
	public class Filesystem {
		public static void Init() {
			CosmosVFS fs = new CosmosVFS();
			VFSManager.RegisterVFS( fs );
		}

		public static void Init( CosmosVFS pFs ) {
			CosmosVFS fs = pFs;
			VFSManager.RegisterVFS( fs );
		}

		public static Dictionary<string, object> GetSpaceavail( string pDisk, string pFormat ) {
			long available_space = VFSManager.GetAvailableFreeSpace( pDisk );
			double output;

			switch ( pFormat.ToUpper() ) {
				case "K": // Kilobytes
				case "KB":
					output = available_space / 1_000;
					break;
				case "M": // Megaobytes
				case "MB":
					output = available_space / 1_000_000;
					break;
				case "G": // Gigabytes
				case "GB":
					output = available_space / 1_000_000_000;
					break;
				case "T": // Terabytes
				case "TB":
					output = available_space / 1_000_000_000_000;
					break;
				case "E": // how the fuck did you make a FAT32 partition that needs exabytes
				case "EB":
					output = available_space / 1_125_899_906_842_624;
					break;
				case "KI": // Kibibytes
				case "KIB":
					output = available_space / 1_024;
					break;
				case "MI": // Mebibytes
				case "MIB":
					output = available_space / 1_048_576;
					break;
				case "GI": // Gibibytes
				case "GIB":
					output = available_space / 1_073_741_824;
					break;
				case "TI": // Tebibytes
				case "TIB":
					output = available_space / 1_099_511_627_776;
					break;
				case "EI": // Exibytes
				case "EIB":
					output = available_space / 1_125_899_906_842_624;
					break;
				default:
					output = available_space;
					break;
			} //*/

			return new Dictionary<string, object> {
				{ "space", output },
				{ "type", pFormat }
			};
		}
	}
}
