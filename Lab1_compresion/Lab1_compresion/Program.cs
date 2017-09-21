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
        static string denuevo;
        static char[] mensaje;

        static void Main(string[] args)
        {
            Console.WriteLine("Inicia el programa: \n e.g: -c -f" + "(entre comillas)ruta del archivo");
            VerificarComando(Console.ReadLine());
            Console.WriteLine("Si desea ejecutar otra operación escriba 'otave', de lo contrario presione cualquier letra");

            denuevo = Console.ReadLine();

            if(denuevo == "otave")
            {
                VerificarComando(Console.ReadLine());
            }
            else
            {
                Environment.Exit(0);
            }
            
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

                    StreamWriter swc = new StreamWriter(path + ".comp");

                    swc.WriteLine("RLE");
                    swc.WriteLine(resultado);
                    swc.Close();

                    Console.Write("Compresión realizada con éxito:");

                    FileInfo bcfile = new FileInfo(path);
                    FileInfo acfile = new FileInfo(path + ".comp");


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
                else if (cmd == "-h")
                {
               
                    StreamReader src = new StreamReader(File.OpenRead(path));

                    string archivo = src.ReadToEnd();

                    resultado = compression_methods.ComprimirHuffman.Comprimir(archivo);


                    StreamWriter swn = new StreamWriter(path + ".comp");
                    swn.WriteLine("HOFFMAN");

                    foreach (KeyValuePair<string, string> item in compression_methods.ComprimirHuffman.Diccionario)
                    {
                        swn.WriteLine(item.Key + "," + item.Value);
                    }

                    swn.WriteLine("termino");

                    swn.WriteLine(resultado);

                    swn.Close();

                    Console.Write("Compresión realizada con éxito:");

                    FileInfo bcfile = new FileInfo(path);
                    FileInfo acfile = new FileInfo(path + ".comp");


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

                    string tipocompresion = srd.ReadLine();

                    if(tipocompresion == "RLE")
                    { 
                    
                    string archivo = srd.ReadToEnd();

                        compression_methods.RLE_compression descompresion = new compression_methods.RLE_compression();

                        StreamWriter swd = new StreamWriter(path);
                  
                        string descomp = descompresion.Decode(archivo);

                        swd.WriteLine(descomp);
                        swd.Close();
                         
                    }
                    else
                    {
                        string linea = srd.ReadLine();
                        Dictionary<string, string> claves = new Dictionary<string, string>();
                        while(linea != "termino")
                        {
                            string[] clave = linea.Split(',');
                            claves.Add(clave[0], clave[1]);

                            linea = srd.ReadLine();
                        }
                        string mensaje = srd.ReadLine();

                        StreamWriter swd = new StreamWriter(path);

                        string descomp = compression_methods.ComprimirHuffman.descomprimir(claves, mensaje);

                        swd.WriteLine(descomp);

                        swd.Close();

                    }
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

