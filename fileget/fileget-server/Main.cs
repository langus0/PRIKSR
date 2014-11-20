using System;
using Filesystem;


namespace filegetserver
{
	public class Server
	{
		class App : Ice.Application
		{
			public override int run(string[] args)
			{
				// Terminate cleanly on receipt of a signal
				shutdownOnInterrupt();

				// Create an object adapter (stored in the _adapter static members)	
				Ice.ObjectAdapter adapter = communicator ().createObjectAdapter("DirectoryAdapter");
				DirectoryI._adapter = adapter;
				FileI._adapter = adapter;
				ChunkI._adapter = adapter;
				
				// Create the directory
				DirectoryI root = new DirectoryI ("main");
				
				// Create a file called "README" in the directory	
				//TODO: read actual file from a disk
				FileI file = new FileI ("README", 100, root);
												
				// Create chunks
				//TODO: split file into chunks, compute hashes and create chunks
				new ChunkI (0, new byte [] {0, 0}, new byte [] {0x10, 0x10}, file);
				new ChunkI (1, new byte [] {1, 1}, new byte [] {0x11, 0x11}, file);

				// All objects are created, allow client requests now
				adapter.activate();

				// Wait until we are done
				communicator().waitForShutdown();

				if (interrupted())
					Console.Error.WriteLine(appName() + ": terminating");
				return 0;
			}
		}

		public static void Main(string[] args)
		{
			App app = new App();
			Environment.Exit(app.main(args));
		}
	}
}



