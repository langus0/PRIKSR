using System;
using Filesystem;
using System.Collections.Generic;

namespace filegetserver
{
	public class DirectoryI : DirectoryDisp_
	{
		public static Ice.ObjectAdapter _adapter;		
        private List<FilePrx> files; 
		
		public DirectoryI (string name) {	
			this.files = new List<FilePrx> ();
			
			// Create Ice ID
			Ice.Identity myID = new Ice.Identity();
			myID.name = name;
						
			// Add the identity to the object adapter
			_adapter.add(this, myID);
		}
		
		public void addFile (FilePrx filePrx) 
		{
			files.Add (filePrx);
		}
						
		public override FilePrx[] getFiles (Ice.Current current__)
		{			
			return files.ToArray ();
		}
	}
}

