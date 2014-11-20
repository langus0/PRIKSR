using System;
using Filesystem;


namespace filgetclient
{
	public class Client
	{

		public static void Main(string[] args)
		{
			int status = 0;
			Ice.Communicator ic = null;
			try {
				// Create a communicator
				ic = Ice.Util.initialize(ref args);

				// Create a proxy for the directory
				Ice.ObjectPrx obj = ic.stringToProxy (@"main");

				// Down-cast the proxy to a Directory proxy
				DirectoryPrx dir = DirectoryPrxHelper.checkedCast(obj);

				// List the contents of the root directory
				Console.WriteLine("Files in directory:");
				foreach (FilePrx f in dir.getFiles ()) {
					Console.WriteLine ("\tFile: {0}, size: {1}", f.getName (), f.getByteLength ());
					foreach (ChunkPrx ch in f.getChunks ()) {
						Console.WriteLine ("\t\tChunk[{0}]:hash: {1}", ch.getSeqNo (), BitConverter.ToString (ch.getHash ()));
						Console.WriteLine ("\t\t\t{0}", BitConverter.ToString (ch.read ()));
					}
				}
			} catch (Exception e) {
				Console.Error.WriteLine(e);
				status = 1;
			}

				
			if (ic != null) {
				// Clean up
				try {
					ic.destroy();
				} catch (Exception e) {
					Console.Error.WriteLine(e);
					status = 1;
				}
			}
				
			Environment.Exit(status);
		}
	}
}
