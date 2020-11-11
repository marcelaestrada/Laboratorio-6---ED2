using System;
using System.Collections.Generic;
using System.Text;

namespace CifradoRSA.Interfaz
{
    interface ICifrado
    {
        List<string> generarClaves(int p, int q);
        string cifrarDescifrar();
    }
}
