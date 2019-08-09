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
    public class DatosObjetosCM : GestionBD
    {
        const string obtenDatosCM = "Sp_ObtenDatosCM";

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool ObjetosCMConsulta(int Tipo, int UsuarioID, int AplicacionID)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(obtenDatosCM);
                CargaParametro("@Tipo", SqlDbType.Int, 8, ParameterDirection.Input, Tipo);
                CargaParametro("@Usuario", SqlDbType.Int, 8, ParameterDirection.Input, UsuarioID);
                CargaParametro("@AplicacionID", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("DatosCM");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + UsuarioID + " Consulto Objetos CM");
                else
                    EscribeLog("Error: Usuario: " + UsuarioID + " No Consulto Objetos CM");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: DatosObjetosCM.ObjetosCMConsulta" + Err.Message.ToString());
            }
            return respuesta;
        }
    }
}