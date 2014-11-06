using System;
using IObj = Ice.Object; //mogę teraz używać Iobj zamiast Ice.Object

namespace ICEServer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int status = 0;
			Ice.Communicator ic = null;
			try {
				ic = Ice.Util.initialize(ref args);
				Ice.ObjectAdapter adapter
					= ic.createObjectAdapterWithEndpoints(
						"SimplePrinterAdapter", "default -p 10000");
				Ice.Object obj = new PrinterI ();
				adapter.add(obj, Ice.Util.stringToIdentity("SimplePrinter"));
				adapter.activate();
				ic.waitForShutdown();
			} catch (Exception e) {
				Console.Error.WriteLine(e);
				status = 1;
			}
			
			if (ic != null) {
				// Clean up
				//
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
