using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio3.Models;
using Laboratorio3.Helpers;
using System.Text;
using System.IO;
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
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
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
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
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
        #region ESPIRAL
        public ActionResult EspiralDescifrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EspiralDescifrado(HttpPostedFileBase file, string clave)
        {
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
            var ClaveM = Convert.ToInt32(clave);

            var Texto = string.Empty;
            var byteBuffer = new byte[1000000];
            using (var streamReader = new FileStream(file.FileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(streamReader))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Texto += Encoding.UTF8.GetString(reader.ReadBytes(1000000));
                    }
                }
            }

            var txt = string.Empty;
            var ClaveN = Texto.Length / ClaveM;

            var Matriz = new char[ClaveM, ClaveN];
            var posX = 0;
            var posY = 0;
            var numVueltas = 0;
            var HaciaAbajo = true;
            var HaciaArriba = false;
            var HaciaDerecha = false;
            var HaciaIzquierda = false;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (HaciaAbajo && posY != ClaveN - 1 - numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posY++;
                }
                else if (HaciaAbajo && posY == ClaveN - 1 - numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posX++;
                    HaciaAbajo = false; HaciaDerecha = true;
                }
                else if (HaciaDerecha && posX != ClaveM - 1 - numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posX++;
                }
                else if (HaciaDerecha && posX == ClaveM - 1 - numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posY--;
                    HaciaDerecha = false; HaciaArriba = true;
                }
                else if (HaciaArriba && posY != numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posY--;
                }
                else if (HaciaArriba && posY == numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    numVueltas++;
                    posX--;
                    HaciaArriba = false; HaciaIzquierda = true;
                }
                else if (HaciaIzquierda && posX != numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posX--;
                }
                else if (HaciaIzquierda && posX == numVueltas)
                {
                    Matriz[posX, posY] = Texto[i];
                    posY++;
                    HaciaIzquierda = false; HaciaAbajo = true;
                }
            }

            for (int i = 0; i < ClaveN; i++)
            {
                for (int j = 0; j < ClaveM; j++)
                {
                    txt += Matriz[j, i];
                }
            }

            DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.txt";

            using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(streamWriter))
                {
                    writer.Write(Encoding.UTF8.GetBytes(txt.ToArray()));
                }
            }

            return RedirectToAction("Descargar");
        }

        public ActionResult EspiralCifrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EspiralCifrado(HttpPostedFileBase file, string clave)
        {
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
            var ClaveM = Convert.ToInt32(clave);

            var Texto = string.Empty;
            var byteBuffer = new byte[1000000];
            using (var streamReader = new FileStream(file.FileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(streamReader))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Texto += Encoding.UTF8.GetString(reader.ReadBytes(1000000));
                    }
                }
            }

            var txt = string.Empty;
            var ClaveN = Texto.Length % ClaveM == 0 ? Texto.Length / ClaveM : (Texto.Length / ClaveM) + 1;

            Texto = Texto.PadRight(ClaveN * ClaveM, '|');

            var Matriz = new char[ClaveM, ClaveN];
            var contLetras = 0;

            for (int i = 0; i < ClaveN; i++)
            {
                for (int j = 0; j < ClaveM; j++)
                {
                    Matriz[j, i] = Texto[contLetras];
                    contLetras++;
                }
            }

            var posX = 0;
            var posY = 0;
            var numVueltas = 0;
            var HaciaAbajo = true;
            var HaciaArriba = false;
            var HaciaDerecha = false;
            var HaciaIzquierda = false;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (HaciaAbajo && posY != ClaveN - 1 - numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posY++;
                }
                else if (HaciaAbajo && posY == ClaveN - 1 - numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posX++;
                    HaciaAbajo = false; HaciaDerecha = true;
                }
                else if (HaciaDerecha && posX != ClaveM - 1 - numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posX++;
                }
                else if (HaciaDerecha && posX == ClaveM - 1 - numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posY--;
                    HaciaDerecha = false; HaciaArriba = true;
                }
                else if (HaciaArriba && posY != numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posY--;
                }
                else if (HaciaArriba && posY == numVueltas)
                {
                    txt += Matriz[posX, posY];
                    numVueltas++;
                    posX--;
                    HaciaArriba = false; HaciaIzquierda = true;
                }
                else if (HaciaIzquierda && posX != numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posX--;
                }
                else if (HaciaIzquierda && posX == numVueltas)
                {
                    txt += Matriz[posX, posY];
                    posY++;
                    HaciaIzquierda = false; HaciaAbajo = true;
                }
            }

            DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.cif";

            txt = txt.TrimEnd('|');

            using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(streamWriter))
                {
                    writer.Write(Encoding.UTF8.GetBytes(txt.ToArray()));
                }
            }

            return RedirectToAction("Descargar");
        }
        #endregion
        #region ZIGZAG
        public ActionResult ZigZagCifrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ZigZagCifrado(HttpPostedFileBase file, string clave)
        {
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
            var Clave = Convert.ToInt32(clave);
            if (Clave > 1)
            {
                var Texto = string.Empty;
                var byteBuffer = new byte[1000000];
                using (var streamReader = new FileStream(file.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(streamReader))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            Texto += Encoding.UTF8.GetString(reader.ReadBytes(1000000));
                        }
                    }
                }

                var txt = ClaseLogica.CifradoZigZag(Clave, Texto);

                DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.cif";


                using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        writer.Write(Encoding.UTF8.GetBytes(txt.ToArray()));
                    }
                }


            }
            return RedirectToAction("Descargar");
        }

        public ActionResult ZigZagDesCifrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ZigZagDescifrado(HttpPostedFileBase file, string clave)
        {
            bool Exists;
            string Paths = Server.MapPath("~/Archivos/");
            Exists = Directory.Exists(Paths);
            if (!Exists)
            {
                Directory.CreateDirectory(Paths);
            }
            var Clave = Convert.ToInt32(clave);
            if (Clave > 1)
            {
                var Texto = string.Empty;
                var byteBuffer = new byte[1000000];
                using (var streamReader = new FileStream(file.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(streamReader))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            Texto += Encoding.UTF8.GetString(reader.ReadBytes(1000000));
                        }
                    }
                }

                var txt = ClaseLogica.DescifradoZigZag(Clave, Texto);

                DataInstance.Instance.ArchivoAcutal = $"{DataInstance.Instance.sPath}\\{Path.GetFileNameWithoutExtension(file.FileName)}.txt";

                using (var streamWriter = new FileStream(DataInstance.Instance.ArchivoAcutal, FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        writer.Write(Encoding.UTF8.GetBytes(txt.ToArray()));
                    }
                }
            }
            return RedirectToAction("Descargar");
        }
        #endregion
        #region DESCARGAR
        public ActionResult Descargar()
        {
            var name = Path.GetFileName(DataInstance.Instance.ArchivoAcutal);
            return File(DataInstance.Instance.ArchivoAcutal, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        #endregion
        #region OTRAS FUNCIONES
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion
    }
}
