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
    public class DatosObjetosBD : GestionBD
    {
        const string obtenDatosBD       = "Sp_ObtenDatosBD";
        const string obtenerAnalisisBD  = "Sp_GraficaAnalisisBD";

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool ObjetosBDConsulta(int Tipo, int UsuarioID, int AplicacionID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenDatosBD);
                CargaParametro("@Tipo", SqlDbType.Int, 8, ParameterDirection.Input, Tipo);
                CargaParametro("@Usuario", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@BDId", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Objetos BD");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Objetos BD");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: DatosObjetosBD.ObjetosBDConsulta" + Err.Message.ToString());
            }
            return respuesta;
        }

        public bool AnalisisBDConsulta(int Tipo, int UsuarioID, int AplicacionID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenerAnalisisBD);
                CargaParametro("@Tipo", SqlDbType.Int, 8, ParameterDirection.Input, Tipo);
                CargaParametro("@Usuario", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@BDId", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Objetos BD");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Objetos BD");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: DatosObjetosBD.ObjetosBDConsulta" + Err.Message.ToString());
            }
            return respuesta;
        }

    }
}