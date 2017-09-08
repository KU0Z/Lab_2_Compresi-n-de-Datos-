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
            _path = path;
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
        
        public byte[]Comprimir(byte[] s)
        {
            List<byte> lista= new List<byte>();
            byte un=0;
            int cantidad=0;
            for (int i =0 ; i < s.Length; i++)
            {
                if (i==0)
                {
                    un = s[i];
                    cantidad++;
                    
                }
                else
                {
                    if (s[i]== un)
                    {
                        cantidad++; 
                    }
                    else
                    {
                        if (cantidad>255)
                        {
                            byte bytes;
                            for (int j = 1; j < cantidad/255; j++)
                            {
                                 bytes = Convert.ToByte(255);
                                lista.Add(bytes);
                                lista.Add(s[i]);
                            }
                            bytes = Convert.ToByte(cantidad%255);
                            lista.Add(bytes);
                            lista.Add(s[i]);
                            un = s[i];
                            cantidad = 1;

                        }
                        else
                        {
                            byte bytes = Convert.ToByte(cantidad);
                            lista.Add(bytes);
                            lista.Add(s[i]);
                            un = s[i];
                            cantidad = 1;
                        }
                        
                    }
                    if (i==s.Length-1)
                    {
                        byte bytes = Convert.ToByte(cantidad);
                        lista.Add(bytes);
                        lista.Add(s[i]);
                    }
                }
                
            }
            return lista.ToArray();
            
        }
        public string Encode(string s)
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;
            char current = s[0];
            for (int i = 1; i < s.Length; i++)
            {
                if (current == s[i])
                {
                    count++;
                }
                else
                {
                    sb.AppendFormat("{0}{1}", count, current);
                    count = 1;
                    current = s[i];
                }
            }
            sb.AppendFormat("{0}{1}", count, current);
            return sb.ToString();
        }
        public byte[] ConvertirBinarioYTexto(string datosTexto)
        {
            return Encoding.ASCII.GetBytes(datosTexto);
        }
        public void escribir_archivo(byte[] unosbites)
        {
            string folderName = @"c:\Archivos Comprimidos";
            Directory.CreateDirectory(folderName);
            DirectoryInfo archivo = new DirectoryInfo(_path);
            string nombrenuearchivo = archivo.Name.Substring(0, (archivo.Name.Length - archivo.Extension.Length));
            string pathNew = Path.Combine(folderName, (nombrenuearchivo + ".relx"));
            byte[] bytes = unosbites;
            FileStream fsNew = new FileStream(pathNew, FileMode.Create, FileAccess.Write);
            fsNew.Write(bytes, 0, bytes.Length);
            fsNew.Flush();
        }


    }
}
