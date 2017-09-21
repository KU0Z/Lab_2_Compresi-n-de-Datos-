using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Compression.RunLength rle = new Compression.RunLength();
            Compression.Huffman_Compression huffman = new Compression.Huffman_Compression();
            string path = "";
            string option = "";


            while (option != "s")
            {
                Console.WriteLine("Bienvenido");
                menu();
                option = Console.ReadLine();
                if (option == "c")
                {
                    Console.Clear();
                    Console.WriteLine("Elija un método de compresión");
                    Console.WriteLine("1. Run Length Encoding");
                    Console.WriteLine("2. Huffman");
                    Console.WriteLine("3. Salir");
                    option = Console.ReadLine();
                    if (option != "3")
                    {
                        try
                        {
                            if (option == "1")
                            {
                                Console.WriteLine("Ingrese la ruta del archivo");
                                path = Console.ReadLine();
                                rle.Comprimir(path);
                                rle.CompressionRate();
                                Console.WriteLine("Presione una tecla para continuar...");
                                Console.ReadKey();
                            }
                            else if (option == "2")
                            {
                                Console.WriteLine("Ingrese la ruta del archivo");
                                path = Console.ReadLine();
                                huffman.HuffmanCompresion(path);
                                huffman.CompressionRate();
                                Console.WriteLine("Presione una tecla para continuar...");
                                Console.ReadKey();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Descompresión Fallada, presione una tecla para continuar...");
                            Console.ReadKey();
                        }
                        
                    }
                }
                else if (option == "d")
                {
                    Console.Clear();
                    Console.WriteLine("Elija un método de descompresión");
                    Console.WriteLine("1. Run Length Encoding");
                    Console.WriteLine("2. Huffman");
                    Console.WriteLine("3. Salir");
                    option = Console.ReadLine();
                    if (option != "3")
                    {
                        if (option == "1")
                        {
                            try
                            {
                                Console.WriteLine("Ingrese la ruta del archivo");
                                path = Console.ReadLine();
                                rle.Descomprir(path);
                                Console.WriteLine("Descompresión realizada exitosamente, presione una tecla para continuar...");
                                Console.ReadKey();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Descompresión Fallada, presione una tecla para continuar...");
                                Console.ReadKey();
                            }
                            
                        }
                        else if (option == "2")
                        {
                            try
                            {
                                Console.WriteLine("Ingrese la ruta del archivo");
                                path = Console.ReadLine();
                                huffman.HuffmanDescompress(path);
                                Console.WriteLine("Descompresión realizada exitosamente, presione una tecla para continuar...");
                                Console.ReadKey();
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Descompresión Fallada, presione una tecla para continuar...");
                                Console.ReadKey();
                            }
                            
                        }
                    }
                }
            }

        }

        static void menu()
        {
            Console.Clear(); 
            Console.WriteLine("Elija una opción");
            Console.WriteLine("c. compresión");
            Console.WriteLine("d. descompresión");

            Console.WriteLine("s salir");
        }
    }
    
}
