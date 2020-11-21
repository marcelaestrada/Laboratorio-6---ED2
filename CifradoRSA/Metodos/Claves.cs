using System;
using System.Collections.Generic;
using System.Text;

namespace CifradoRSA.Metodos
{
    public class Claves
    {
        public List<string> generar(int p, int q)
        {
            List<string> claves = new List<string>();
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = calcularE(phi);
            int d = calcularD(phi, e);
            claves.Add(n + "," + e);
            claves.Add(n + "," + d);
            return claves;
        }

        int calcularE(int phi)
        {
            int e = 2;
            int n = 2;

            while (phi % e == 0)
            {
                bool esPrimo = true;
                for (int i = 2; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        esPrimo = false;
                        break;
                    }
                }

                if (esPrimo)
                {
                    e = n;
                }

                n++;
            }

            return e;
        }

        int calcularD(int phi, int e)
        {
            int aux = 0;
            int aux2 = 0;
            int aux3 = 0;
            int[,] numeros = new int[2, 2];
            numeros[0, 0] = phi;
            numeros[0, 1] = phi;
            numeros[1, 0] = e;
            numeros[1, 1] = 1;

            while (numeros[1, 0] != 1)
            {
                aux = numeros[0, 0] / numeros[1, 0];
                aux2 = numeros[0, 0];
                aux3 = numeros[0, 1];
                numeros[0, 0] = numeros[1, 0];
                numeros[0, 1] = numeros[1, 1];
                numeros[1, 0] = aux2 - (numeros[1, 0] * aux);
                numeros[1, 1] = aux3 - (numeros[1, 1] * aux);

                if (numeros[1, 1] < 0)
                {
                    int numero = numeros[1, 1];
                    numeros[1, 1] = (numero % phi + phi) % phi;
                }
            }

            return numeros[1, 1];
        }
    }
}
