using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Lab1_compresion
{

    class Program
    {

        static string path;
        static string linea;
        static bool operacion = false;
        static char[] chars;
        static int charslenght = 0;
        static string resultado;

        static void Main(string[] args)
        {

            VerificarComando(Console.ReadLine());

            Console.ReadKey();
        }

        static void VerificarComando(string lineacompleta)
        {
            linea = lineacompleta;

            string cmd = linea.Substring(0, 2);
            string[] lineacmd = linea.Split('"');

            path = lineacmd[1];


            if (cmd == "-c")
            {

                BinaryReader brc = new BinaryReader(File.OpenRead(path));
                StreamReader src = new StreamReader(File.OpenRead(path));

                string archivo = src.ReadToEnd();

                charslenght = archivo.Length;
                chars = brc.ReadChars(charslenght);

                compression_methods.RLE_compression compresion = new compression_methods.RLE_compression();

                resultado = compresion.Encode(charslenght, chars);

                StreamWriter swc = new StreamWriter(path + ".rlex");
                swc.WriteLine(resultado);
                swc.Close();

                Console.Write(resultado);

            }
            else if (cmd == "-d")
            {

                StreamReader srd = new StreamReader(File.OpenRead(path));

                string archivo = srd.ReadToEnd();


                compression_methods.RLE_compression descompresion = new compression_methods.RLE_compression();

                Console.WriteLine(descompresion.Decode(archivo));

            }
            else
            {
                Console.WriteLine("Ingrese un comando válido");
                VerificarComando(Console.ReadLine());
            }


            }

        }
    }
