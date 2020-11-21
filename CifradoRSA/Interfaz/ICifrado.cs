using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CifradoRSA.Interfaz
{
    interface ICifrado
    {
        List<string> generarClaves(int p, int q);
        List<byte> cifrar(FileStream archivo, int n, int e);
        List<byte> descifrar(FileStream archivo, int n, int d);
    }
}
