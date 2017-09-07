using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compression
{
    public class RunLength
    {
        private string path;
        private string data;
        public string _data { get; set; }

        public string _path { get; set; }

        public byte[] getData(string path)
        {
            FileStream Source = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[Source.Length];
            int numBytesToRead = (int)Source.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = Source.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;

            return bytes;
            // Read the source file into a byte array.

        }

    }
}
