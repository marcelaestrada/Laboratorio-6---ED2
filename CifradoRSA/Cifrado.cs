using CifradoRSA.Interfaz;
using CifradoRSA.Metodos;
using System;
using System.Collections.Generic;

namespace CifradoRSA
{
    public class Cifrado : ICifrado
    {
        public string cifrarDescifrar()
        {

            throw new NotImplementedException();
        }

        public List<string> generarClaves(int p, int q)
        {
            Claves clave = new Claves();
            return clave.generar(p, q);
        }
    }
}
