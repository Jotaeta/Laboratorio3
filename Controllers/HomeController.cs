using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio3.Models;
namespace Laboratorio3.Controllers
{
    public class HomeController : Controller
    {
        Logica ClaseLogica = new Logica();
        public ActionResult Index()
        {
            return View();
        }

        #region CESAR
        public ActionResult CesarCodificacion()
        {
            var Clave = "ABC";
            var Texto = " !\"";
            var txt = string.Empty;

            var ErrorPalabra = false;
            var contCaracter = 0;

            while (!ErrorPalabra && contCaracter < Clave.Length)
            {
                if (Clave.Count(letra => letra == Clave[contCaracter]) != 1)
                {
                    ErrorPalabra = true;
                }
                contCaracter++;
            }

            if (!ErrorPalabra)
            {
                //AGREGAR EL READER
                var ABC = ClaseLogica.ObtnerDiccionaro(1, Clave);
                foreach (var item in Texto)
                {
                    txt += ABC[item]; //AGREGAR WRITER
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult CesarDescodificacion()
        {
            var Clave = "ABC";
            var Texto = "ABC";
            var txt = string.Empty;

            var ErrorPalabra = false;
            var contCaracter = 0;

            while (!ErrorPalabra && contCaracter < Clave.Length)
            {
                if (Clave.Count(letra => letra == Clave[contCaracter]) != 1)
                {
                    ErrorPalabra = true;
                }
                contCaracter++;
            }

            if (!ErrorPalabra)
            {
                // AGREGAR EL READER

                var ABC = ClaseLogica.ObtnerDiccionaro(2, Clave);
                foreach (var item in Texto)
                {
                    txt += ABC[item]; //PASAR AL WRITER
                }
            }
            return RedirectToAction("Index");
        }
        #endregion


    }
}
