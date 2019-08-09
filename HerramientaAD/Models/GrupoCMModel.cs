using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Models
{
    public class GrupoCMModel
    {
        DatosObjetosCM datosObjetosCM = new DatosObjetosCM();

        const int TipoConsulta1 = 1;
        const int TipoConsulta2 = 2;
        const int TipoConsulta3 = 3;
        const int TipoConsulta4 = 4;

        private List<ElementosDeGrupo.Indicadores> indicadores = new List<ElementosDeGrupo.Indicadores>();
        private List<ElementosDeGrupo.GraficaPie> archivos = new List<ElementosDeGrupo.GraficaPie>();
        private List<ElementosDeGrupo.GraficaBarra> masUsados = new List<ElementosDeGrupo.GraficaBarra>();
        private List<ElementosDeGrupo.GraficaPie> componentes = new List<ElementosDeGrupo.GraficaPie>();
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

        public List<ElementosDeGrupo.GraficaPie> Componentes
        {
            get { return componentes; }
            set { componentes = value; }
        }

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public GrupoCMModel(int UsuarioID, int AplicacionID)
        {
            if (datosObjetosCM.ObjetosCMConsulta(TipoConsulta1, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosCM.ResultadoXML.DocumentElement.SelectSingleNode("DatosCM");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    indicadores.Add(new ElementosDeGrupo.Indicadores(
                        elemento.Attributes["TipoObjeto"].Value.ToString(),
                        int.Parse(elemento.Attributes["Total"].Value.ToString()),
                        0, "")
                        );
                }
            }

            if (datosObjetosCM.ObjetosCMConsulta(TipoConsulta2, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosCM.ResultadoXML.DocumentElement.SelectSingleNode("DatosCM");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    archivos.Add(new ElementosDeGrupo.GraficaPie(
                        int.Parse(elemento.Attributes["Porcentaje"].Value.ToString()),
                        elemento.Attributes["LenguajeApp"].Value.ToString())
                        );
                }
            }

            if (datosObjetosCM.ObjetosCMConsulta(TipoConsulta3, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosCM.ResultadoXML.DocumentElement.SelectSingleNode("DatosCM");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    masUsados.Add(new ElementosDeGrupo.GraficaBarra(
                        int.Parse(elemento.Attributes["Registros"].Value.ToString()),
                        elemento.Attributes["Objeto"].Value.ToString())
                        );
                }
            }

            if (datosObjetosCM.ObjetosCMConsulta(TipoConsulta4, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosCM.ResultadoXML.DocumentElement.SelectSingleNode("DatosCM");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    componentes.Add(new ElementosDeGrupo.GraficaPie(
                        int.Parse(elemento.Attributes["Porcentaje"].Value.ToString()),
                        elemento.Attributes["TipoHijo"].Value.ToString())
                        );
                }
            }
        }
    }
}