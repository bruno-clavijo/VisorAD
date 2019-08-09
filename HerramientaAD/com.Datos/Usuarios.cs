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
    public class Usuarios : GestionBD
    {
        private string validaUsuario = "Sp_ValidaUsuario";

        public XmlDocument AplicacionesDatosExtraXML { get; private set; }

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool UsuariosValida(string Usuario, string Contraseña)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(validaUsuario);
                CargaParametro("@Usuario", SqlDbType.VarChar, 100, ParameterDirection.Input, Usuario);
                CargaParametro("@Contraseña", SqlDbType.VarChar, 100, ParameterDirection.Input, Contraseña);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("Usuario");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    EscribeLog("Correcto: Usuario: " + Usuario + " Intento de Ingreso");
                else
                    EscribeLog("Error: Usuario: " + Usuario + " No Intento de Ingreso");
            }
            catch (Exception Err)
            {
                EscribeLog("Excepcion: Usuarios.UsuariosValida " + Err.Message.ToString());
            }
            return respuesta;
        }
    }
}