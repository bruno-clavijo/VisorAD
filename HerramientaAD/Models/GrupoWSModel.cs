using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Models
{
    public class GrupoWSModel
    {
        DatosObjetosWS datosObjetosWS = new DatosObjetosWS();

        const int TipoConsulta1 = 1;
        const int TipoConsulta2 = 2;
        const int TipoConsulta3 = 3;
        const int TipoConsulta4 = 4;

        private List<ElementosDeGrupo.Indicadores> indicadores = new List<ElementosDeGrupo.Indicadores>();
        private List<ElementosDeGrupo.GraficaPie> archivos = new List<ElementosDeGrupo.GraficaPie>();
        private List<ElementosDeGrupo.GraficaBarra> masUsados = new List<ElementosDeGrupo.GraficaBarra>();
        private List<ElementosDeGrupo.GraficaPie> middleware = new List<ElementosDeGrupo.GraficaPie>();
        private XmlDocument resultadoXML;

        public List<ElementosDeGrupo.Indicadores> Indicadores
        {
            get { return indicadores; }
            set { indicadores = value; }
        }

        public List<ElementosDeGrupo.GraficaPie> Archivos
        {
            get { return archivos; }
            set { archivos = value; }
        }

        public List<ElementosDeGrupo.GraficaBarra> MasUsados
        {
            get { return masUsados; }
            set { masUsados = value; }
        }

        public List<ElementosDeGrupo.GraficaPie> Middleware
        {
            get { return middleware; }
            set { middleware = value; }
        }

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public GrupoWSModel(int UsuarioID, int AplicacionID)
        {
            if (datosObjetosWS.ObjetosWSConsulta(TipoConsulta1, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosWS.ResultadoXML.DocumentElement.SelectSingleNode("DatosWS");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    indicadores.Add(new ElementosDeGrupo.Indicadores(
                        elemento.Attributes["TipoObjeto"].Value.ToString(),
                        int.Parse(elemento.Attributes["Total"].Value.ToString()),
                        0, "")
                        );
                }
            }

            if (datosObjetosWS.ObjetosWSConsulta(TipoConsulta2, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosWS.ResultadoXML.DocumentElement.SelectSingleNode("DatosWS");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    archivos.Add(new ElementosDeGrupo.GraficaPie(
                        int.Parse(elemento.Attributes["Porcentaje"].Value.ToString()),
                        elemento.Attributes["LenguajeApp"].Value.ToString())
                        );
                }
            }

            if (datosObjetosWS.ObjetosWSConsulta(TipoConsulta3, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosWS.ResultadoXML.DocumentElement.SelectSingleNode("DatosWS");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    masUsados.Add(new ElementosDeGrupo.GraficaBarra(
                        int.Parse(elemento.Attributes["Registros"].Value.ToString()),
                        elemento.Attributes["Objeto"].Value.ToString())
                        );
                }
            }

            if (datosObjetosWS.ObjetosWSConsulta(TipoConsulta4, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosWS.ResultadoXML.DocumentElement.SelectSingleNode("DatosWS");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    middleware.Add(new ElementosDeGrupo.GraficaPie(
                        int.Parse(elemento.Attributes["Porcentaje"].Value.ToString()),
                        elemento.Attributes["Middleware"].Value.ToString())
                        );
                }
            }
        }
    }
}