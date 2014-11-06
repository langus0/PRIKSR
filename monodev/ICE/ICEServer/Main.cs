using System;
using IObj = Ice.Object; //mogę teraz używać Iobj zamiast Ice.Object

namespace ICEServer
{

	class Server : Ice.Application
	{
	 	public override int run(string[] args)
		{
			shutdownOnInterrupt();

			Ice.ObjectAdapter adapter = communicator().createObjectAdapter("SimplePrinterAdapter");
			Ice.Object obj = new PrinterI();
			adapter.add(obj, Ice.Util.stringToIdentity("SimplePrinter"));
			adapter.activate();

			communicator().waitForShutdown();
			if (interrupted())
				Console.Error.WriteLine(appName() + " - terminationg");
			return 0;
		}
	}
	class MainClass
	{
		public static void Main (string[] args)
		{
			Server srv = new Server();
			Environment.Exit(srv.main(args));


			/*

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
*/
		}
	}
}
