using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Laboratorio3.Controllers;
namespace Laboratorio3.Models
{
    public class Logica
    {
       #region CESAR
        public Dictionary<int, int> ObtnerDiccionaro(int Tipo, string clave)
        {
            var AuxABC = new Dictionary<int, int>();

            var tamVector = 256;
            var ABCOriginal = new List<int>();
            var ABCClave = new List<int>();

            for (int i = 0; i < tamVector; i++)
            {
                ABCOriginal.Add(i);
            }

            foreach (var item in clave)
            {
                ABCClave.Add(item);
            }

            for (int i = 0; i < tamVector; i++)
            {
                if (!ABCClave.Contains(ABCOriginal[i]))
                {
                    ABCClave.Add(ABCOriginal[i]);
                }
            }

            if (Tipo == 1)
            {
                for (int i = 0; i < tamVector; i++)
                {
                    AuxABC.Add(ABCOriginal[i], ABCClave[i]);
                }
            }
            else
            {
                for (int i = 0; i < tamVector; i++)
                {
                    AuxABC.Add(ABCClave[i], ABCOriginal[i]);
                }
            }

            return AuxABC;
        }
        #endregion
        #region ZIGZAG
        public string DescifradoZigZag(int clave, string Texto)
        {
            var MatrizAux = new char[Texto.Length, clave];
            var PosX = 0;
            var PosY = 1;
            var IndexTexto = 0;

            var cantLetras = (2 * clave) - 1;
            var contEspacios = 1;

            for (int i = 0; i < Texto.Length / (cantLetras - 1); i++)
            {
                MatrizAux[PosX, 0] = Texto[IndexTexto];
                PosX += cantLetras - 1;
                IndexTexto++;
            }

            MatrizAux[Texto.Length - 1, 0] = Texto[IndexTexto];
            IndexTexto++;
            PosX = 0;

            var cantV = Texto.Length / (cantLetras - 1);
            cantLetras -= 2;

            for (int i = 1; i < clave - 1; i++)
            {
                cantLetras -= 2;

                PosX += contEspacios;
                for (int j = 0; j < cantV; j++)
                {
                    MatrizAux[PosX, PosY] = Texto[IndexTexto];
                    IndexTexto++;
                    PosX += cantLetras + 1;
                    MatrizAux[PosX, PosY] = Texto[IndexTexto];
                    PosX += (i * 2);
                    IndexTexto++;
                }

                PosY++;
                PosX = 0;
                contEspacios++;
            }

            PosX += contEspacios;

            for (int j = 0; j < cantV; j++)
            {
                MatrizAux[PosX, PosY] = Texto[IndexTexto];
                PosX += (2 * clave) - 2;
                IndexTexto++;
            }

            var HaciaAbajo = true;
            var HaciaArriba = false;
            var text = string.Empty;
            PosY = 0;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (HaciaAbajo && PosY != clave - 1)
                {
                    text += MatrizAux[i, PosY];
                    PosY++;
                }
                else if (HaciaAbajo && PosY == clave - 1)
                {
                    text += MatrizAux[i, PosY];
                    PosY--;
                    HaciaAbajo = false; HaciaArriba = true;
                }
                else if (HaciaArriba && PosY != 0)
                {
                    text += MatrizAux[i, PosY];
                    PosY--;
                }
                else if (HaciaArriba && PosY == 0)
                {
                    text += MatrizAux[i, PosY];
                    PosY++;
                    HaciaAbajo = true; HaciaArriba = false;
                }
            }

            return text.TrimEnd('|');
        }


        public string CifradoZigZag(int clave, string Texto)
        {
            var tamX = 3;

            if (clave > 2)
            {
                var AuxTam = (Texto.Length - 1) / ((2 * clave) - 2);
                tamX = (Texto.Length - 1) % ((2 * clave) - 2) == 0 ? (AuxTam * ((2 * clave) - 2)) + 1 : ((AuxTam + 1) * ((2 * clave) - 2)) + 1;
            }
            else if (Texto.Length > 3)
            {
                tamX = (Texto.Length - 3) % 2 == 0 ? Texto.Length : Texto.Length + 1;
            }

            var matrizZigZag = new char[tamX, clave];
            Texto = Texto.PadRight(tamX, '|');

            var HaciaAbajo = true;
            var HaciaArriba = false;
            var PosY = 0;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (HaciaAbajo && PosY != clave - 1)
                {
                    matrizZigZag[i, PosY] = Texto[i];
                    PosY++;
                }
                else if (HaciaAbajo && PosY == clave - 1)
                {
                    matrizZigZag[i, PosY] = Texto[i];
                    PosY--;
                    HaciaAbajo = false; HaciaArriba = true;
                }
                else if (HaciaArriba && PosY != 0)
                {
                    matrizZigZag[i, PosY] = Texto[i];
                    PosY--;
                }
                else if (HaciaArriba && PosY == 0)
                {
                    matrizZigZag[i, PosY] = Texto[i];
                    PosY++;
                    HaciaAbajo = true; HaciaArriba = false;
                }
            }

            var Text = string.Empty;

            for (int i = 0; i < clave; i++)
            {
                for (int j = 0; j < tamX; j++)
                {
                    if (matrizZigZag[j, i] != '\0')
                    {
                        Text += matrizZigZag[j, i];
                    }
                }
            }

            return Text;
        }
        #endregion
    }

}



