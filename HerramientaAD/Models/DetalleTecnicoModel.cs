using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Models
{
    public class DetalleTecnicoModel
    {
        Areas areas = new Areas();
        DatosDetalleTecnico datosDetalle = new DatosDetalleTecnico();

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

        private string filtro1;
        public string Filtro1
        {
            get { return filtro1; }
            set { filtro1 = value; }
        }

        private int filtro2;
        public int Filtro2
        {
            get { return filtro2; }
            set { filtro2 = value; }
        }

        private int filtro3;
        public int Filtro3
        {
            get { return filtro3; }
            set { filtro3 = value; }
        }

        private int filtro4;
        public int Filtro4
        {
            get { return filtro4; }
            set { filtro4 = value; }
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

        private List<ListasDesplegables> filtro1Lista = new List<ListasDesplegables>();
        public List<ListasDesplegables> Filtro1Lista
        {
            get => filtro1Lista;
            set => filtro1Lista = value;
        }

        private List<ListasDesplegables> filtro2Lista = new List<ListasDesplegables>();
        public List<ListasDesplegables> Filtro2Lista
        {
            get => filtro2Lista;
            set => filtro2Lista = value;
        }

        private List<ListasDesplegables> filtro3Lista = new List<ListasDesplegables>();
        public List<ListasDesplegables> Filtro3Lista
        {
            get => filtro3Lista;
            set => filtro3Lista = value;
        }

        private List<ListasDesplegables> filtro4Lista = new List<ListasDesplegables>();
        public List<ListasDesplegables> Filtro4Lista
        {
            get => filtro4Lista;
            set => filtro4Lista = value;
        }
        


        public DetalleTecnicoModel()
        {
            
        }

        public DetalleTecnicoModel(int UsuarioID)
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

            if (datosDetalle.ConsultaDetalleFiltro("BD", 0, string.Empty, string.Empty, string.Empty, string.Empty))
            {
                detalleXML = datosDetalle.ResultadoXML;
            }
        }
        
        public DetalleTecnicoModel(string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3, string Filtro4)
        {
            if (datosDetalle.ConsultaDetalleFiltro(Tipo, AplicacionID, Filtro1, Filtro2, Filtro3, Filtro4))
            {
                detalleXML = datosDetalle.ResultadoXML;
            }
        }

        private XmlDocument detalleXML;
        public XmlDocument DetalleXML
        {
            get { return detalleXML; }
            set { detalleXML = value; }
        }
    }
}