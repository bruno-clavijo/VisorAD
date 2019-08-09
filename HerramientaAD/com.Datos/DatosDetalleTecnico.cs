using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using HerramientaAD.com.BaseDatos;

namespace HerramientaAD.com.Datos
{
    public class DatosDetalleTecnico : GestionBD
    {
        const string consultaBD = "Sp_ConsultaDependenciaBD";
        const string consultaWS = "Sp_ConsultaDependenciaWS";
        const string consultaCM = "Sp_ConsultaDependenciaCM";
        const string filtrosBD = "sp_FiltrosDB";
        const string filtrosWS = "sp_FiltrosWS";
        const string filtrosCM = "sp_FiltrosCM";

        private XmlDocument resultadoXML;

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool ConsultaDetalleFiltro(string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3, string Filtro4)
        {
            bool resp = false;
            try
            {
                switch (Tipo)
                {
                    case "BD":
                        if (string.IsNullOrEmpty(Filtro1) || Filtro1 == "Selecciona") Filtro1 = "0";
                        if (string.IsNullOrEmpty(Filtro2) || Filtro2 == "Selecciona") Filtro2 = "0";
                        if (string.IsNullOrEmpty(Filtro3) || Filtro3 == "Selecciona") Filtro3 = "";
                        if (string.IsNullOrEmpty(Filtro4) || Filtro4 == "Selecciona") Filtro4 = "0";

                        PreparaStoredProcedure(consultaBD);
                        CargaParametro("basedatos", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro1));
                        CargaParametro("objetodb", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro2));
                        CargaParametro("archivo", SqlDbType.VarChar, 150, ParameterDirection.Input, Filtro3);
                        CargaParametro("numerolinea", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro4));
                        break;

                    case "WS":
                        if (string.IsNullOrEmpty(Filtro1) || Filtro1 == "Selecciona") Filtro1 = "";
                        if (string.IsNullOrEmpty(Filtro2) || Filtro2 == "Selecciona") Filtro2 = "";
                        if (string.IsNullOrEmpty(Filtro3) || Filtro3 == "Selecciona") Filtro3 = "";
                        if (string.IsNullOrEmpty(Filtro4) || Filtro4 == "Selecciona") Filtro4 = "";

                        PreparaStoredProcedure(consultaWS);
                        CargaParametro("tipohijo", SqlDbType.VarChar, 50, ParameterDirection.Input, Filtro1);
                        CargaParametro("middleware", SqlDbType.VarChar, 50, ParameterDirection.Input, Filtro2);
                        CargaParametro("url", SqlDbType.VarChar, 200, ParameterDirection.Input, Filtro3);
                        CargaParametro("archivo", SqlDbType.VarChar, 150, ParameterDirection.Input, Filtro4);
                        break;

                    case "CM":
                        if (string.IsNullOrEmpty(Filtro1) || Filtro1 == "Selecciona") Filtro1 = "";
                        if (string.IsNullOrEmpty(Filtro2) || Filtro2 == "Selecciona") Filtro2 = "";
                        if (string.IsNullOrEmpty(Filtro3) || Filtro3 == "Selecciona") Filtro3 = "";
                        if (string.IsNullOrEmpty(Filtro4) || Filtro4 == "Selecciona") Filtro4 = "0";

                        PreparaStoredProcedure(consultaCM);
                        CargaParametro("tipohijo", SqlDbType.VarChar, 50, ParameterDirection.Input, Filtro1);
                        CargaParametro("archivo", SqlDbType.VarChar, 150, ParameterDirection.Input, Filtro2);
                        CargaParametro("lenguajeapp", SqlDbType.VarChar, 50, ParameterDirection.Input, Filtro3);
                        CargaParametro("numlinea", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro4));
                        break;
                }

                CargaParametro("aplicacionid", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);
                SqlDataReader objrst = AlmacenarStoredProcedureDataReader();
                if (objrst.Read())
                {
                    resultadoXML = new XmlDocument();
                    resultadoXML.LoadXml("<xml>" + objrst[0].ToString() + "</xml>");
                    XmlNode select = resultadoXML.DocumentElement.SelectSingleNode("Consulta");
                    resp = select.HasChildNodes;
                }
                else
                    resp = false;
                CerrarConexion();
                if (resp)
                    EscribeLog("Consulta correcta apliación: " + Tipo + " " + AplicacionID);
                else
                    EscribeLog("Consulta incorrecta aplicación: " + Tipo + " " + AplicacionID);
            }
            catch (Exception exx)
            {
                EscribeLog("ConsultaDep.ConsultaDependencia " + exx.Message.ToString());
                resp = false;
            }
            return resp;
        }

        public bool ObtenFiltros(string Filtro, string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3)
        {
            bool resp = false;
            try
            {
                switch (Filtro)
                {
                    case "BD":
                        if (Filtro1 == "") Filtro1 = "0";
                        if (Filtro2 == "") Filtro2 = "0";
                        PreparaStoredProcedure(filtrosBD);
                        CargaParametro("basedatosid", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro1));
                        CargaParametro("objetoid", SqlDbType.Int, 8, ParameterDirection.Input, int.Parse(Filtro2));
                        CargaParametro("archivo", SqlDbType.VarChar, 150, ParameterDirection.Input, Filtro3);
                        break;
                    case "WS":
                        PreparaStoredProcedure(filtrosWS);
                        break;
                    case "CM":
                        PreparaStoredProcedure(filtrosCM);
                        break;
                    default:
                        PreparaStoredProcedure(filtrosBD);
                        break;
                }

                CargaParametro("tipo", SqlDbType.VarChar, 100, ParameterDirection.Input, Tipo);
                CargaParametro("aplicacionid", SqlDbType.Int, 8, ParameterDirection.Input, AplicacionID);

                SqlDataReader objrst = AlmacenarStoredProcedureDataReader();
                if (objrst.Read())
                {
                    resultadoXML = new XmlDocument();
                    resultadoXML.LoadXml("<xml>" + objrst[0].ToString() + "</xml>");
                    XmlNode select = resultadoXML.DocumentElement.SelectSingleNode("Filtros");
                    resp = select.HasChildNodes;
                }
                else
                    resp = false;
                CerrarConexion();
                if (resp)
                    EscribeLog("Obtención correcta ObtenFiltros Tipo:" + Filtro + " " + Tipo + " Aplicacion " + AplicacionID);
                else
                    EscribeLog("Obtención incorrecta ObtenFiltros Tipo:" + Filtro + " " + Tipo + " Aplicacion " + AplicacionID);
            }
            catch (Exception exx)
            {
                EscribeLog("Aplicacion.ObtenFiltrosBD " + exx.Message.ToString());
                resp = false;
            }
            return resp;
        }
    }
}