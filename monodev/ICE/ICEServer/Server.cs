using System;
using Ice;
namespace ICEServer
{
	public class PrinterI  : Demo.PrinterDisp_
	{
		public override string printString (string s, Current current__)
		{
			return string.Format("Hello {0}", s);
		}
	}
}

