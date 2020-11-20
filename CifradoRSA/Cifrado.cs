using CifradoRSA.Interfaz;
using CifradoRSA.Metodos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CifradoRSA
{
    public class Cifrado : ICifrado
    {
        public string cifrar(FileStream archivo, int n, int ed)
        {
            CifradoDescifrado cifdes = new CifradoDescifrado();
            string mensaje = "";
            archivo.Position = 0;
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            while (archivo.Position < archivo.Length)
            {
                buffer = reader.ReadBytes(2000000);
                mensaje += cifdes.cifrar(buffer, n, ed);
            }
            reader.Close();
            archivo.Close();

            return mensaje;
        }

        public string descifrar(FileStream archivo, int n, int d)
        {
            CifradoDescifrado cifdes = new CifradoDescifrado();
            string mensaje = "";
            archivo.Position = 0;
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            while (archivo.Position < archivo.Length)
            {
                buffer = reader.ReadBytes(2000000);
                mensaje += cifdes.descifrar(buffer, n, d);
            }
            reader.Close();
            archivo.Close();

            return mensaje;
        }

        public List<string> generarClaves(int p, int q)
        {
            Claves clave = new Claves();
            return clave.generar(p, q);
        }
    }
}
