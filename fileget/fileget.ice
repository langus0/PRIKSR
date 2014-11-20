module Filesystem {

	exception GenericError {
		string reason;
	};

	sequence<byte> Buffer;

	interface Chunk {
		idempotent long getSeqNo ();
		idempotent Buffer read ();
		idempotent Buffer getHash ();
	};

	sequence<Chunk*> Chunks;

	interface File {
		idempotent string getName ();
		idempotent long getByteLength ();
		idempotent Chunks getChunks ();
		idempotent Chunk* getChunk (long chunkSeqNo);
	};

	sequence<File*> Files;

	interface Directory {
		idempotent Files getFiles ();
	};
};

