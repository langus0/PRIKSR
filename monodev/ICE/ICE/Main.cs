using System;
using Demo;

namespace ICE
{

	class Client:Ice.Application{
		public override  int run (string[] args)
		{

			Ice.RouterPrx defRouter = communicator ().getDefaultRouter ();
		
			Glacier2.RouterPrx router = Glacier2.RouterPrxHelper.checkedCast (defRouter);

			if (router == null)
				Console.WriteLine("NULL!!");

			router.createSession ("test", "test");
			Console.WriteLine("s");
			//---Stare bez zmian
			Ice.ObjectPrx obj = communicator ().stringToProxy (@"SimplePrinter");
			//Ice.ObjectPrx obj = communicator().stringToProxy(@"SimplePrinter@SimplePrinterAdapter");
			PrinterPrx printer = PrinterPrxHelper.checkedCast (obj);
			if (printer == null)
				throw new ApplicationException ("invalid proxy");
			Console.WriteLine (printer.printString ("registry"));
			//---END Stare bez zmian

			if (router != null) {
				try{
					router.destroySession();
				}catch(Ice.ConnectionLostException){

				}
			}

			return 0;


		}

	}
	class MainClass
	{


		public static void Main (string[] args)
		{

			Client cln = new Client();
			Environment.Exit(cln.main(args));/*
			int status = 0;
			Ice.Communicator ic = null;
			try {
				ic = Ice.Util.initialize(ref args);
				Ice.ObjectPrx ibj = ic.stringToProxy("SimplePrinter:default -p 10000 ");
				PrinterPrx printer = PrinterPrxHelper.checkedCast(ibj);
				if (printer == null)
					throw new ApplicationException("e----------");
				Console.WriteLine(printer.printString("Mateusz"));
			} catch (Exception e){
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
			Environment.Exit(status);*/
		}
	}
}
