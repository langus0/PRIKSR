using System;
using Filesystem;

namespace filegetserver
{
	public class ChunkI : ChunkDisp_
	{

			public static Ice.ObjectAdapter _adapter;	
		private long seqNo;
		byte[] content;
		byte[] hash;

		public ChunkI (long seq, byte[] content, byte[] hash, FileI file) {	
			seqNo=seq;
			this.content=content;
			this.hash=hash;

			Ice.Identity myID = new Ice.Identity();
			myID.name = System.Guid.NewGuid ().ToString ();
			
			// Add the identity to the object adapter
			_adapter.add(this, myID);

			ChunkPrx thisChunk = ChunkPrxHelper.uncheckedCast(_adapter.createProxy(myID));
			file.addChunk(thisChunk);
		}

		
		public override long getSeqNo(Ice.Current current__){
			return seqNo;
			
		}

			public override byte[] read(Ice.Current current__){
			return content;

			}

			public override byte[] getHash(Ice.Current current__){
			return hash;
		}
	}
}

