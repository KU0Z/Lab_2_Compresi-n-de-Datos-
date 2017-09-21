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

        private double fileLength;
        private double fileLengthAfter;


        public double _fileLength
        {
            get { return fileLength; }
            set { fileLength = value; }
        }

        public double _fileLengthAfter
        {
            get { return fileLengthAfter; }
            set { fileLengthAfter = value; }
        }
        public string _path
        {
            get { return path; }
            set { path = value; }
        }

        private byte[] LecturaArchivo(string path)
        {
            FileStream Source = new FileStream(path, FileMode.Open, FileAccess.Read);
            _fileLength = Source.Length;
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

        private void WriteFile(byte[] bytesComprimidos, string ext)
        {
            string folderName = @"c:\Archivos Comprimidos";
            Directory.CreateDirectory(folderName);
            DirectoryInfo archivo = new DirectoryInfo(_path);
            string nombrenuearchivo = archivo.Name.Substring(0, (archivo.Name.Length - archivo.Extension.Length));
            string pathNew = Path.Combine(folderName, (nombrenuearchivo + ext));
            byte[] bytes = bytesComprimidos;
            FileStream fsNew = new FileStream(pathNew, FileMode.Create, FileAccess.Write);
            fsNew.Write(bytes, 0, bytes.Length);
            _fileLengthAfter = fsNew.Length;
            fsNew.Flush();
            fsNew.Close();
        }

        public void HuffmanCompresion(string path)
        {
            byte[] data = GetCompressedData(path);
            WriteFile(data, ".relx");
        }
        public void HuffmanDescompress(string path)
        {
            _path = path;
            //Lectura de Bytes     
            byte[] data = LecturaArchivo(_path);      
            data= EncodeText(data);
            WriteFile(data, extension);
        }
        public byte[] EncodeText(byte[] readerBytes)
        {
            Dictionary<byte,HuffmanNode> tableDescompressed = new Dictionary<byte, HuffmanNode>();
            byte[] listaDescomprimida;
            int jump = 255;
            int jumpMod= Convert.ToInt32(readerBytes[0]);
            int jumpDiv = Convert.ToInt32(readerBytes[1]);
            jump = (jump * jumpMod) + jumpDiv;

            for (int i = 2; i <= jump; i=i+2)
            {
                if (tableDescompressed.ContainsKey(readerBytes[i]))
                {
                    HuffmanNode nodo = tableDescompressed[readerBytes[i]];
                    nodo.frequency+= Convert.ToInt32(readerBytes[i + 1]);
                    tableDescompressed.Remove(readerBytes[i]);
                    tableDescompressed.Add(readerBytes[i], nodo);
                }
                else
                {
                    HuffmanNode nodo=new HuffmanNode(readerBytes[i].ToString(),readerBytes[i]);
                    nodo.frequency = Convert.ToInt32(readerBytes[i + 1]);
                    tableDescompressed.Add(readerBytes[i],nodo);
                    
                }
            }
            HuffmanTree arbolDes = new HuffmanTree();
            arbolDes.data = new List<HuffmanNode>(tableDescompressed.Values);
            arbolDes.data.Sort(delegate (HuffmanNode p1, HuffmanNode p2)
            {
                return p1.encodedCharacter.CompareTo(p2.encodedCharacter);
            });
            arbolDes.data.Sort();
            HuffmanNode aux = arbolDes.CreateTree();
            arbolDes.GetCodesDes("", aux);
            string encode="";
            int tamañoExtencion = Convert.ToInt32(readerBytes[jump+2]);
            byte[] bytesExtencion = new byte[tamañoExtencion];
            for (int i = 0; i < tamañoExtencion; i++)
            {
                bytesExtencion[i] = readerBytes[i + jump + 3];
            }
            extension = Encoding.ASCII.GetString(bytesExtencion);
            for (int i = jump +3+tamañoExtencion; i < readerBytes.Length-2; i++)
            {
                string currentByte = Convert.ToString(readerBytes[i], 2);
                if(currentByte.Length<8)
                {
                    for (int j = 0; j < 8 - currentByte.Length; j++)
                    {
                        encode += "0";
                    }
                }

                encode += currentByte;


            }
            int lastbyte = Convert.ToInt32(readerBytes[readerBytes.Length-1]);
            if (lastbyte != 0)
            {
                string currentByte = Convert.ToString(readerBytes[readerBytes.Length-2], 2);
                if(currentByte.Length<lastbyte)
                {
                    for (int j = 0; j < 8 - currentByte.Length; j++)
                    {
                        encode += "0";
                    }
                   
                }
                encode += currentByte;
            }
            else
            {
                string currentByte = Convert.ToString(readerBytes[readerBytes.Length - 2], 2);
                encode += currentByte;
            }

            listaDescomprimida = arbolDes.DescompressedBytes(encode);
            return listaDescomprimida;
        }

        public void CompressionRate()
        {
            Console.WriteLine("índice de compresion: {0}", (_fileLengthAfter / _fileLength));
            Console.WriteLine("factor de compresión: {0}", (_fileLength / _fileLengthAfter));
            Console.WriteLine("% de ahorro: {0}", ((Math.Abs(_fileLength - _fileLengthAfter)) / _fileLength) * 100);
        }
    }
}
