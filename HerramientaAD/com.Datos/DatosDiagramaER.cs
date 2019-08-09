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
    public class DatosDiagramaER : GestionBD
    {
        const string obtenDiagramaER = "Sp_DiagramaEntidadRelacion";

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool DiagramaERConsulta(int Tipo, int UsuarioID, int BaseDeDatosID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenDiagramaER);
                CargaParametro("@Tipo", SqlDbType.Int, 8, ParameterDirection.Input, Tipo);
                CargaParametro("@Usuario", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@BDId", SqlDbType.Int, 8, ParameterDirection.Input, BaseDeDatosID);
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
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Diagrama ER");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Diagrama ER");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: DatosDiagramaER.DiagramaERConsulta" + Err.Message.ToString());
            }
            return respuesta;
        }
    }
}