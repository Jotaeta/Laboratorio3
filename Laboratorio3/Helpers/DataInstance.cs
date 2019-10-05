using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio3.Helpers
{
    public class DataInstance
    {
        private static DataInstance _instance = null;
        public static DataInstance Instance
        {
            get
            {
                if (_instance == null) _instance = new DataInstance();
                return _instance;
            }
        }

        public string sPath;
        public string ArchivoAcutal;
    }
}