using System;
using System.Collections.Generic;
using System.IO;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            int e = 0;
            int d = 0;
            int cont = 0;
            CifradoRSA.Cifrado cipher = new CifradoRSA.Cifrado();
            FileStream filestream = new FileStream(@"C:\Users\marce\Desktop\2020\Semestre II 2020\Estructura de datos II\Laboratorio\Laboratorio-6---ED2\Pruebas\cuento.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            List<string> listaClave = cipher.generarClaves(61, 53);

            foreach(var item in listaClave)
            {
                if (cont == 0)
                {
                    string[] splited = item.Split(',');
                    n = Convert.ToInt32(splited[0]);
                    e = Convert.ToInt32(splited[1]);
                    cont++;
                }
                else
                {
                    string[] splited = item.Split(',');
                    n = Convert.ToInt32(splited[0]);
                    d = Convert.ToInt32(splited[1]);
                }
            }

            FileStream writer = new FileStream(@"C:\Users\marce\Desktop\2020\Semestre II 2020\Estructura de datos II\Laboratorio\Laboratorio-6---ED2\Pruebas\resultadoRSA.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter write = new StreamWriter(writer);
            string cifrado = cipher.cifrarDescifrar(filestream, n, e);
            write.Write(cifrado);
            writer.Close();
        }
    }
}
