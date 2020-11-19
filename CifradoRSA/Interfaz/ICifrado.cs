using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CifradoRSA.Interfaz
{
    interface ICifrado
    {
        List<string> generarClaves(int p, int q);
        string cifrar(FileStream archivo, int n, int ed);
    }
}
