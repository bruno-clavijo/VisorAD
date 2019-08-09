using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace HerramientaAD.com.LogFisico
{
    public class Log
    {
        private string rutaLog;
        public string RutaLog
        {
            get { return rutaLog; }
            set { rutaLog = value; }
        }

        private string mensajeExcepcion;
        public string MensajeExcepcion
        {
            get { return mensajeExcepcion; }
            set { mensajeExcepcion = value; }
        }

        private void InicializaRuta()
        {
            try
            {
                rutaLog = HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings["RutaFisicaLog"] + DateTime.Now.ToString("yyyyMMdd") + ".log");
            }
            catch (Exception)
            {

            }
        }

        public bool EscribeLog(string Mensaje)
        {
            bool respuesta = false;
            try
            {
                InicializaRuta();
                StreamWriter streamWriter = new StreamWriter(rutaLog, true);
                streamWriter.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " " + Mensaje);
                streamWriter.Flush();
                streamWriter.Close();
                respuesta = true;
            }
            catch (Exception Err)
            {
                mensajeExcepcion = Err.Message.ToString();
                respuesta = false;
            }
            return respuesta;
        }
    }
}