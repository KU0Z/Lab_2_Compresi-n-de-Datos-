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
        private string extension;

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
            DirectoryInfo file = new DirectoryInfo(_path);
            byte[] bytes = LecturaArchivo(_path);
            HuffmanTree frequencyTree = new HuffmanTree(file.Extension);
            frequencyTree.GetFrequencies(bytes);
            HuffmanNode aux = frequencyTree.CreateTree();
            frequencyTree.GetCodes("", aux);
            return frequencyTree.EncodeText(bytes, frequencyTree._fileExtension);            
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
        public void HuffmanDescompress(string path)
        {
            _path = path;
            //Lectura de Bytes     
            byte[] data = LecturaArchivo(_path);

            WriteFile(data);
        }
        public byte[] EncodeText(byte[] readerBytes)
        {
            Dictionary<string, byte> tableDescompressed = new Dictionary<string, byte>();
              List <byte> listaDescomprimida = new List<byte>();
            int jump = 255;
            int jumpMod= Convert.ToInt32(readerBytes[0]);
            int jumpDiv = Convert.ToInt32(readerBytes[1]);
            jump = (jump * jumpMod) + jumpDiv;

            for (int i = 2; i < jump; i=i+2)
            {
                //if (tableDescompressed.ContainsKey())
                //{

                //}
                //else
                //{
                //    tableDescompressed.Add(Convert.ToString(readerBytes[i + 1]), readerBytes[i]);
                //}
            }

            //int numeroRepeticiones;
            //byte caracterRepetido;
            //byte[] bytesExtencion;
            //int tamañoExtencion = Convert.ToInt32(s[0]);
            //bytesExtencion = new byte[tamañoExtencion];
            //for (int i = 0; i < tamañoExtencion; i++)
            //{
            //    bytesExtencion[i] = s[i + 1];
            //}
            //extencionArchivo = ConvertirBinarioYTexto(bytesExtencion);
            //for (int i = tamañoExtencion + 1; i < s.Length; i = i + 2)
            //{
            //    numeroRepeticiones = Convert.ToInt32(s[i]);
            //    caracterRepetido = s[i + 1];
            //    for (int j = 0; j < numeroRepeticiones; j++)
            //    {
            //        listaDescomprimida.Add(caracterRepetido);
            //    }
            //}
            return listaDescomprimida.ToArray();
        }
    }
}
