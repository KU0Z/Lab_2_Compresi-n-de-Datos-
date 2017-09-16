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
        private string extencionArchivo;
        public string _data { get; set; }

        public string _path { get; set; }

        public void Comprimir(string path)
        {
            _path = path;
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            //Llamada al metodo para comprimir los datos
            bytes = ConteoDatos(bytes);
            //Escritura del archivo Comprimido
            escribir_archivo(bytes);
        }

      
        
        public void Descomprir(string path)
        {
            _path = path;
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            bytes = DescomprecionDatos(bytes);
            escribirArchivoDes(bytes);
            
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
        private byte[] ConteoDatos(byte[] s)
        {
            DirectoryInfo archivo = new DirectoryInfo(_path);
            List<byte> lista= new List<byte>();
            byte un=0;
            int cantidad=0;
            byte bytesCantidad;
            byte[] byteExtencion = ConvertirBinarioYTexto(archivo.Extension);
            bytesCantidad = Convert.ToByte(archivo.Extension.Length);
            lista.Add(bytesCantidad);
            for (int i = 0; i < byteExtencion.Length; i++)
            {
                lista.Add(byteExtencion[i]);
            }
            for (int i =0 ; i < s.Length; i++)
            {
                if (i==0)
                {
                    un = s[i];
                    cantidad=1;
                    
                }
                else
                {
                    if (un==s[i])
                    {
                        cantidad++; 
                    }
                    else
                    {
                        if (cantidad>255)
                        {
                            
                            for (int j = 1; j < cantidad/255; j++)
                            {
                                bytesCantidad = Convert.ToByte(255);
                                lista.Add(bytesCantidad);
                                lista.Add(s[i]);
                            }
                            bytesCantidad = Convert.ToByte(cantidad%255);
                            lista.Add(un);
                            lista.Add(s[i]);
                            un = s[i];
                            cantidad = 1;

                        }
                        else
                        {
                            bytesCantidad = Convert.ToByte(cantidad);
                            lista.Add(bytesCantidad);
                            lista.Add(un);
                            un = s[i];
                            cantidad = 1;
                        }
                        
                    }
                    if (i==s.Length-1)
                    {
                        bytesCantidad = Convert.ToByte(cantidad);
                        lista.Add(bytesCantidad);
                        lista.Add(s[i]);
                    }
                }
                
            }
            return lista.ToArray();
            
        }
        private byte[] DescomprecionDatos(byte[] s)
        {

            DirectoryInfo archivo = new DirectoryInfo(_path);
            List<byte> listaDescomprimida = new List<byte>();
            int numeroRepeticiones;
            byte caracterRepetido;
            byte[] bytesExtencion;
            int tamañoExtencion=Convert.ToInt32(s[0]);
            bytesExtencion = new byte[tamañoExtencion];
            for (int i = 0; i < tamañoExtencion; i++)
            {
                bytesExtencion[i]=s[i+1];
            }
            extencionArchivo = ConvertirBinarioYTexto(bytesExtencion);
            for (int i = tamañoExtencion + 1; i < s.Length; i = i + 2)
            {
                numeroRepeticiones=Convert.ToInt32(s[i]);
                caracterRepetido = s[i + 1];
                for (int j = 0; j < numeroRepeticiones; j++)
                {
                    listaDescomprimida.Add(caracterRepetido);
                }                                
            }
            return listaDescomprimida.ToArray();
        }


        private string ConvertirBinarioYTexto(byte[] datosBinario)
        {
            return Encoding.ASCII.GetString(datosBinario);
        }
        private byte[] ConvertirBinarioYTexto(string datosTexto)
        {
            return Encoding.ASCII.GetBytes(datosTexto);
        }
        private void escribir_archivo(byte[] bytesComprimidos)
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
        //Sirve para descomprimir
        private void escribirArchivoDes(byte[] bytesComprimidos)
        {
            string folderName = @"c:\Archivos Comprimidos";
            Directory.CreateDirectory(folderName);
            DirectoryInfo archivo = new DirectoryInfo(_path);
            string nombrenuearchivo = archivo.Name.Substring(0, (archivo.Name.Length - archivo.Extension.Length));
            string pathNew = Path.Combine(folderName, (nombrenuearchivo + extencionArchivo));
            byte[] bytes = bytesComprimidos;
            FileStream fsNew = new FileStream(pathNew, FileMode.Create, FileAccess.Write);
            fsNew.Write(bytes, 0, bytes.Length);
            fsNew.Flush();
            fsNew.Close();
        }
        

    }
}
