using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Models
{
    public class GrupoBDModel
    {
        DatosObjetosBD datosObjetosBD = new DatosObjetosBD();

        const int TipoConsulta1 = 1;
        const int TipoConsulta2 = 2;
        const int TipoConsulta3 = 3;
        const bool primerElemento = true;

        private List<ElementosDeGrupo.Indicadores> indicadores = new List<ElementosDeGrupo.Indicadores>();
        private List<ElementosDeGrupo.GraficaPie> archivos = new List<ElementosDeGrupo.GraficaPie>();
        private List<ElementosDeGrupo.GraficaBarra> masUsados = new List<ElementosDeGrupo.GraficaBarra>();
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

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }
                
        public GrupoBDModel(int UsuarioID, int AplicacionID)
        {
            if (datosObjetosBD.ObjetosBDConsulta(TipoConsulta1, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    indicadores.Add (new ElementosDeGrupo.Indicadores (
                        elemento.Attributes["TipoObjeto"].Value.ToString(), 
                        int.Parse(elemento.Attributes["Usadas"].Value.ToString()),
                        int.Parse(elemento.Attributes["NoUsadas"].Value.ToString()),
                        elemento.Attributes["Porcentaje"].Value.ToString())
                        );
                }
            }

            if (datosObjetosBD.ObjetosBDConsulta(TipoConsulta2, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    archivos.Add(new ElementosDeGrupo.GraficaPie (
                        int.Parse(elemento.Attributes["Porcentaje"].Value.ToString()),
                        elemento.Attributes["LenguajeApp"].Value.ToString())
                        );
                }
            }

            if (datosObjetosBD.ObjetosBDConsulta(TipoConsulta3, UsuarioID, AplicacionID))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    masUsados.Add(new ElementosDeGrupo.GraficaBarra (
                        int.Parse(elemento.Attributes["Registros"].Value.ToString()),
                        elemento.Attributes["Objeto"].Value.ToString())
                        );
                }
            }
        }
    }
}