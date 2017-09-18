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
        static bool operacion = false;
        static char[] cstring;
        static int cCaracteres = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Indide el parámetro segun la operación que desee realizar:");

            VerificarComando(Console.ReadLine());

            Console.ReadKey();
        }

        static void VerificarComando(string cmd)
        {
            if (cmd == "-f")
            {
                Console.WriteLine("Inrese la ruta del archivo:");
                path = Console.ReadLine();
                operacion = true;

                Console.WriteLine("Ahora indique la operación que desea realizar:");

                VerificarComando(Console.ReadLine());
            }
            else if(cmd == "-c")
            {
                if(operacion)
                {
                    BinaryReader brc = new BinaryReader(File.OpenRead(path));
                    StreamReader src = new StreamReader(File.OpenRead(path));

                    string archivo = src.ReadToEnd();


                    cstring = brc.ReadChars(archivo.Length);

                    Console.WriteLine(cstring);



                }
                else
                {
                    Console.WriteLine("No se ha especificado la ruda del archivo, ingrese otro comando");
                    VerificarComando(Console.ReadLine());
                }
            }
            else if(cmd == "-d")
            {
                if(operacion)
                {

                }
                else
                {
                    Console.WriteLine("No se ha especificado la ruda del archivo, ingrese otro comando");
                    VerificarComando(Console.ReadLine());
                }
            }else if (cmd == "-e")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Ingrese un comando válido");
                VerificarComando(Console.ReadLine());
            }
        }

    }
}
