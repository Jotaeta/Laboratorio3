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

            var tamVector = 224;
            var ABCOriginal = new List<int>();
            var ABCClave = new List<int>();

            for (int i = 0; i < tamVector; i++)
            {
                ABCOriginal.Add(i + 32);
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
            return AuxABC;
        }
        #endregion

    }

}
