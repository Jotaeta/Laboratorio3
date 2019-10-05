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
    }

}



