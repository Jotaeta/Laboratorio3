using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio3.Models;
using Laboratorio3.Helpers;
namespace Laboratorio3.Controllers

{
    public class HomeController : Controller
    {
        Logica ClaseLogica = new Logica();

        public ActionResult Index()
        {
            DataInstance.Instance.sPath = Server.MapPath($"~/Archivos");

            return View();
        }
        #region CESAR
        public ActionResult CesarCodificacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CesarCodificacion(HttpPostedFileBase file, string Clave)
        {
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
                var Texto = string.Empty;
                var ABC = ClaseLogica.ObtnerDiccionaro(1, Clave);

                var byteBuffer = new byte[1000000];
                using (var streamReader = new FileStream(file.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(streamReader))
                    {
                        DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.cif";

                        using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
                        {
                            using (var writer = new BinaryWriter(streamWriter))
                            {
                                while (reader.BaseStream.Position != reader.BaseStream.Length)
                                {
                                    byteBuffer = reader.ReadBytes(1000000);

                                    foreach (var item in byteBuffer)
                                    {
                                        writer.Write(Convert.ToByte(ABC[Convert.ToInt32(item)]));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Descargar");
        }

        public ActionResult CesarDescodificacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CesarDescodificacion(HttpPostedFileBase file, string Clave)
        {
            var Texto = string.Empty;
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
                var ABC = ClaseLogica.ObtnerDiccionaro(2, Clave);
                var byteBuffer = new byte[1000000];
                using (var streamReader = new FileStream(file.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(streamReader))
                    {
                        DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.txt";

                        using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
                        {
                            using (var writer = new BinaryWriter(streamWriter))
                            {
                                while (reader.BaseStream.Position != reader.BaseStream.Length)
                                {
                                    byteBuffer = reader.ReadBytes(1000000);

                                    foreach (var item in byteBuffer)
                                    {
                                        writer.Write(Convert.ToByte(ABC[Convert.ToInt32(item)]));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Descargar");
        }
        #endregion
    }
}
