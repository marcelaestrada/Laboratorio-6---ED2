using System;
using System.Collections.Generic;
using System.Text;

namespace CifradoRSA.Metodos
{
    public class CifradoDescifrado
    {
        public string cifrar(byte[] arregloBytes, int n, int ed)
        {
            string mensaje = "";
            int bitsNecesarios = (Convert.ToString(n,2)).Length;
            int[] nuevosNumeros = formulaCifDes(arregloBytes, n, ed, arregloBytes.Length);
            Queue<int> numerosCifrar = devolverBinarios(nuevosNumeros, bitsNecesarios);
            
            foreach(var item in numerosCifrar)
            {
                mensaje += Convert.ToChar(item);
            }

            return mensaje;
        }

        public int[] formulaCifDes (byte[] arregloBytes, int n, int ed, int cant)
        {
            int[] nuevosNumeros = new int[cant];

            for(int i = 0; i<arregloBytes.Length; i++)
            {
                double nuevo = Math.Pow(arregloBytes[i], ed);
                nuevo = nuevo % n;
                nuevosNumeros[i] = Convert.ToInt32(nuevo);
            }

            return nuevosNumeros;
        }

        public Queue<int> devolverBinarios (int[] numeros, int bitsNecesarios)
        {
            int bytesNecesarios = 0;

            //definir cantidad de bytes en los que se debe dividir
            if ((bitsNecesarios % 8) == 0)
            {
                bytesNecesarios = bitsNecesarios / 8;
            }
            else
            {
                bytesNecesarios = (bitsNecesarios / 8) + 1;
            }

            Queue<int> numerosBinarios = new Queue<int>();
            foreach(var item in numeros)
            {
                string binario = Convert.ToString(item, 2);
                int[] cortes = definirSplits(binario.Length, bytesNecesarios);
                for(int i = 0; i < cortes.Length; i++)
                {
                    if(i == 0)
                    {
                        string temp = binario.Substring(0, cortes[i]);
                        numerosBinarios.Enqueue(Convert.ToInt32(temp, 2));
                    }
                    else
                    {
                        string temp = binario.Substring(cortes[i - 1], cortes[i]);
                        numerosBinarios.Enqueue(Convert.ToInt32(temp, 2));
                    }
                }
            }
            return numerosBinarios;
        }

        public int[] definirSplits(int binario, int cantidad)
        {
            int[] cortes = new int[cantidad];
            if (binario % cantidad == 0)
            {
                for(int i = 0; i < cantidad; i++)
                {
                    cortes[i] = binario / cantidad;
                }
            }
            else
            {
                for(int i = 0; i<cantidad; i++)
                {
                    if (i == cantidad - 2)
                    {
                        cortes[i] = (binario / cantidad) + 1;
                    }
                    else
                    {
                        cortes[i] = binario / cantidad;
                    }
                }
            }
            return cortes;
        }
    }
}
