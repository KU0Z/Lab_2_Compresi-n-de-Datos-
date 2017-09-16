using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
using System.Threading.Tasks;

namespace Compression
{
    public class Huffman_Compression
    {
        private string path;
        public string _path
        {
            get { return path; }
            set { path = value; }
        }

        private byte[] LecturaArchivo(string path)
        {
            FileStream Source = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader lecturaBytes = new BinaryReader(Source);
            byte[] bytesLeidos = new byte[Source.Length];
            bytesLeidos = lecturaBytes.ReadBytes(Convert.ToInt32(Source.Length));
            Source.Flush();
            Source.Close();
            return bytesLeidos;

        }
        public byte[] CompressHuffman(string path)
        {
            _path = path;
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            HuffmanTree frequencyTree = new HuffmanTree();
            frequencyTree.GetFrequencies(bytes);
            HuffmanNode aux = frequencyTree.CreateTree();
            frequencyTree.GetCodes("", aux);
            return bytes;
        }
    }
}
