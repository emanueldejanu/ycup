using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChromiumUpdater.Engine.Extensions
{
    internal static class StreamExtensions
    {
        static int DefaultBufferSize = 65536;

        public static void CopyContentsTo(this Stream source, Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (!source.CanRead && !source.CanWrite)
            {
                throw new ObjectDisposedException(String.Empty, "Both source and target are not accessible");
            }
            if (!destination.CanRead && !destination.CanWrite)
            {
                throw new ObjectDisposedException("destination");
            }
            if (!source.CanRead)
            {
                throw new NotSupportedException("Can't read from source");
            }
            if (!destination.CanWrite)
            {
                throw new NotSupportedException("Can't write to target");
            }
            StreamExtensions.InternalCopyContentsTo(source, destination, StreamExtensions.DefaultBufferSize);
        }

        static void InternalCopyContentsTo(Stream source, Stream destination, int bufferSize)
        {
            int num;
            byte[] buffer = new byte[bufferSize];
            while ((num = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                destination.Write(buffer, 0, num);
            }
        }
    }
}
