using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Models
{
    public class DiagramaERModel
    {
        Areas areas = new Areas();
        DatosDiagramaER datosDiagramaER = new DatosDiagramaER();

        const int Tipo1 = 1;
        const int Tipo2 = 2;

        private int areaID;
        public int AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        private int aplicacionID;
        public int AplicacionID
        {
            get { return aplicacionID; }
            set { aplicacionID = value; }
        }

        private string baseID;
        public string BaseID
        {
            get { return baseID; }
            set { baseID = value; }
        }

        private List<ElementosDiagramaER.Cuadros> cuadros = new List<ElementosDiagramaER.Cuadros>();
        public List<ElementosDiagramaER.Cuadros> Cuadros
        {
            get { return cuadros; }
            set { cuadros = value; }
        }

        private List<ElementosDiagramaER.Relaciones> relaciones = new List<ElementosDiagramaER.Relaciones>();
        public List<ElementosDiagramaER.Relaciones> Relaciones
        {
            get { return relaciones; }
            set { relaciones = value; }
        }

        private List<ListasDesplegables> areasLista = new List<ListasDesplegables>();
        public List<ListasDesplegables> AreasLista
        {
            get { return areasLista; }
            set { areasLista = value; }
        }

        private List<ListasDesplegables> aplicacionesLista = new List<ListasDesplegables>();
        public List<ListasDesplegables> AplicacionesLista
        {
            get { return aplicacionesLista; }
            set { aplicacionesLista = value; }
        }

        private List<ListasDesplegables> basesLista = new List<ListasDesplegables>();
        public List<ListasDesplegables> BasesLista
        {
            get => basesLista;
            set => basesLista = value;
        }

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public DiagramaERModel()
        {

        }

        public DiagramaERModel(int UsuarioID, int BaseDeDatosID)
        {
            if (areas.AreasConsulta(UsuarioID))
            {
                XmlNode xmlNode = areas.ResultadoXML.DocumentElement.SelectSingleNode("Areas");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    areasLista.Add(new ListasDesplegables(
                        int.Parse(elemento.Attributes["AreaID"].Value.ToString()),
                        elemento.Attributes["Descripcion"].Value.ToString()));
                }
            }

            if (datosDiagramaER.DiagramaERConsulta(Tipo1, UsuarioID, BaseDeDatosID))
            {
                XmlNode xmlNode = datosDiagramaER.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    cuadros.Add(new ElementosDiagramaER.Cuadros(
                        int.Parse(elemento.Attributes["Numero"].Value.ToString()),
                        elemento.Attributes["Tabla"].Value.ToString())
                        );
                }
            }

            if (datosDiagramaER.DiagramaERConsulta(Tipo2, UsuarioID, BaseDeDatosID))
            {
                XmlNode xmlNode = datosDiagramaER.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    relaciones.Add(new ElementosDiagramaER.Relaciones(
                        int.Parse(elemento.Attributes["From"].Value.ToString()),
                        int.Parse(elemento.Attributes["To"].Value.ToString()),
                        elemento.Attributes["Text"].Value.ToString())
                        );
                }
            }
        }
    }
}