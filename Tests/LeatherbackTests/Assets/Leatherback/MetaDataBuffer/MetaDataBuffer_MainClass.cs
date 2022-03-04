
// This is, quite obviously, a fork (of sorts)
// from https://github.com/harroo/Blitzbit/blob/main/BlitPacket/BlitPacket.cs

using System;
using System.Collections.Generic;

namespace Leatherback {

    public partial class MetaDataBuffer {

        private List<byte> buffer = new List<byte>();

        public MetaDataBuffer () { }

        public MetaDataBuffer (byte[] preBuffer) {

            buffer.Clear();
            AddArray(preBuffer);
        }

        public void AddArray (byte[] data) {

            foreach (byte b in data)
                buffer.Add(b);
        }

        public void Clear () { buffer.Clear(); }

        public void Reset () { index = 0; }

        public byte[] ToArray () => buffer.ToArray();

        public byte[] GetArray () => buffer.ToArray();

        public int Size => buffer.Count;
    }
}
