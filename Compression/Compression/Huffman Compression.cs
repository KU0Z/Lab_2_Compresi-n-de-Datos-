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
        public byte[] GetCompressedData(string path)
        {
            _path = path;
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            HuffmanTree frequencyTree = new HuffmanTree();
            frequencyTree.GetFrequencies(bytes);
            HuffmanNode aux = frequencyTree.CreateTree();
            frequencyTree.GetCodes("", aux);
            return frequencyTree.EncodeText(bytes);            
        }

        private void WriteFile(byte[] bytesComprimidos)
        {
            string folderName = @"c:\Archivos Comprimidos";
            Directory.CreateDirectory(folderName);
            DirectoryInfo archivo = new DirectoryInfo(_path);
            string nombrenuearchivo = archivo.Name.Substring(0, (archivo.Name.Length - archivo.Extension.Length));
            string pathNew = Path.Combine(folderName, (nombrenuearchivo + ".relx"));
            byte[] bytes = bytesComprimidos;
            FileStream fsNew = new FileStream(pathNew, FileMode.Create, FileAccess.Write);
            fsNew.Write(bytes, 0, bytes.Length);
            fsNew.Flush();
            fsNew.Close();
        }

        public void HuffmanCompresion(string path)
        {
            byte[] data = GetCompressedData(path);
            WriteFile(data);
        }
    }
}
