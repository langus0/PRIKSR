using System;
using Demo;

namespace ICE
{

	class Client:Ice.Application{
		public override  int run (string[] args)
		{
			Ice.ObjectPrx obj = communicator().stringToProxy(@"SimplePrinter@SimplePrinterAdapter");
			PrinterPrx printer = PrinterPrxHelper.checkedCast(obj);
			if (printer == null)
				throw new ApplicationException("dsds");
			Console.WriteLine(printer.printString("registry"));
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
