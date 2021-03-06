using System;
using Filesystem;
using System.Collections.Generic;
using System.Linq;

namespace filegetserver
{
	public class FileI : FileDisp_
	{
		public static Ice.ObjectAdapter _adapter;
		private string name;
        private long byteLength;
        private List<ChunkPrx> chunks; 
		
		public FileI (string name, long byteLength, DirectoryI owner) {
			this.name = name;
			this.byteLength = byteLength;
			this.chunks = new List<ChunkPrx> ();
						
			// Create Ice ID
			Ice.Identity myID = new Ice.Identity();
			//losowa nazwa w ICE, bo i tak odnoismy sie do pliku poprez katalog
			myID.name = System.Guid.NewGuid ().ToString ();
						
			// Add the identity to the object adapter
			_adapter.add(this, myID);
			
			// Create a proxy for the new file and add it as a child to the parents
			// ucheked bo mam pewnosc i jest wydajniejsze
			FilePrx thisFile = FilePrxHelper.uncheckedCast(_adapter.createProxy(myID));
			owner.addFile(thisFile);
		}
		
		public void addChunk (ChunkPrx chPrx) 
		{
			chunks.Add (chPrx);
		}
		
		public override long getByteLength (Ice.Current current__)
		{
			return byteLength;
		}
		
		public override ChunkPrx getChunk (long chunkSeqNo, Ice.Current current__)
		{
			return getChunk(chunkSeqNo);
		}
		
		public override Filesystem.ChunkPrx[] getChunks (Ice.Current current__)
		{			
			return chunks.ToArray();
		}
		
		public override string getName (Ice.Current current__)
		{
			return name;
		}
	}
}

