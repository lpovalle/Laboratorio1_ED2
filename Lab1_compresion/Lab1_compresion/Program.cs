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
        static char[] chars;
        static int charslenght = 0;
        static string resultado;

        static void Main(string[] args)
        {
            Console.WriteLine("Inicia el programa: \n e.g: -c -f" + "(entre comillas)ruta del archivo");
            VerificarComando(Console.ReadLine());

            Console.ReadKey();
        }

        static void VerificarComando(string lineacompleta)
        {
            linea = lineacompleta;

            try
            {

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

                    Console.Write("Compresión realizada con éxito:");

                    FileInfo bcfile = new FileInfo(path);
                    FileInfo acfile = new FileInfo(path + ".rlex");


                    int original = Convert.ToInt32(bcfile.Length);
                    int nuevo = Convert.ToInt32(acfile.Length);

                    double compratio = ((double)nuevo / (double)original);
                    double compfactor = ((double)original / (double)nuevo);
                    double savingp = (((double)original - (double)nuevo) / (double)original) * 100;

                    Console.WriteLine("Tamaño original:         " + Convert.ToString(original));
                    Console.WriteLine("Tamaño nuevo:            " + Convert.ToString(nuevo));
                    Console.WriteLine("Ratio de compresión:     " + Convert.ToString(compratio));
                    Console.WriteLine("Factor de compresión:    " + Convert.ToString(compfactor));
                    Console.WriteLine("Porcentaje ahorrado:     " + Convert.ToString(savingp) + "%");

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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine("Escriba el comando de nuevo: ");
                VerificarComando(Console.ReadLine());


            }

        }
    }
}

