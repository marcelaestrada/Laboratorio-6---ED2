using CifradoRSA.Interfaz;
using CifradoRSA.Metodos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace CifradoRSA
{
    public class Cifrado : ICifrado
    {
        public List<byte> cifrar(FileStream archivo, int n, int e)
        {
            List<byte> lista = new List<byte>();
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                buffer = reader.ReadBytes(2000000);
                foreach (var item in buffer)
                {
                    var ok = BigInteger.ModPow(item, (BigInteger)e, (BigInteger)n);
                    byte[] bytes = BitConverter.GetBytes((long)ok);
                    foreach(var b in bytes)
                    {
                        lista.Add(b);
                    }
                }
            }
            reader.Close();
            archivo.Close();

            return lista;
        }

        public List<byte> descifrar(FileStream archivo, int n, int d)
        {
            string mensaje = "";
            List<byte> lista = new List<byte>();
            int cont = 0;
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            List<byte> bytes = new List<byte>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                buffer = reader.ReadBytes(2000000);
                foreach (var item in buffer)
                {
                    bytes.Add(item);
                    if (bytes.Count==8)
                    {
                        byte[] by = new byte[bytes.Count];
                        foreach(var bytee in bytes)
                        {
                            by[cont] = bytee;
                            cont++;
                        }
                        long num = BitConverter.ToInt32(by, 0);
                        var ok = BigInteger.ModPow(num, (BigInteger)d, (BigInteger)n);
                        lista.Add((byte)ok);
                        bytes.Clear();
                        cont = 0;
                    }
                }
            }
            reader.Close();
            archivo.Close();

            return lista;
        }

        public List<string> generarClaves(int p, int q)
        {
            Claves clave = new Claves();
            return clave.generar(p, q);
        }
    }
}
