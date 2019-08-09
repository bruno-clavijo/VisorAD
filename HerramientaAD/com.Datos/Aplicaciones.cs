using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using HerramientaAD.com.BaseDatos;

namespace HerramientaAD.com.Datos
{
    public class Aplicaciones : GestionBD
    {
        private string obtenAplicaciones = "Sp_ObtenAplicacion";
        private string obtenAplicacionesDatosExtra = "Sp_ObtenAplicacionDatosExtra";

        public XmlDocument AplicacionesDatosExtraXML { get; private set; }

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        
        public bool AplicacionesConsulta(int UsuarioID, int AreaID, int AplicacionID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenAplicaciones);
                CargaParametro("@UsuarioID", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@AreaID", SqlDbType.Int, 8, ParameterDirection.Input, AreaID);
                CargaParametro("@Aplicacion", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("Aplicaciones");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Aplicaciones");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Aplicaciones");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: Aplicaciones.AplicacionesConsulta " + Err.Message.ToString());
            }
            return respuesta;
        }

        public bool AplicacionesConsultaDatosExtra(int UsuarioID, int AplicacionID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenAplicacionesDatosExtra);
                CargaParametro("@UsuarioID", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@Aplicacion", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    AplicacionesDatosExtraXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    AplicacionesDatosExtraXML.LoadXml(Document);
                    XmlNode xmlNode = AplicacionesDatosExtraXML.DocumentElement.SelectSingleNode("AplicacionesDatosExtra");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Aplicaciones");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Aplicaciones");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: Aplicaciones.AplicacionesConsultaDatosExtra " + Err.Message.ToString());
            }
            return respuesta;
        }

    }
}