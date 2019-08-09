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
    public class Areas : GestionBD
    {
        private string obtenAreas = "Sp_ObtenArea";

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }
        
        public bool AreasConsulta(int UsuarioID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenAreas);
                CargaParametro("@UsuarioID", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("Areas");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Areas");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Areas");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: Areas.AreasConsulta " + Err.Message.ToString());
            }
            return respuesta;
        }
    }
}