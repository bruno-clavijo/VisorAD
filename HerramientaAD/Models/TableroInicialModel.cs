using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using HerramientaAD.com.Utilerias;
using HerramientaAD.com.Datos;

namespace HerramientaAD.Models
{
    public class TableroInicialModel
    {
        Aplicaciones aplicaciones = new Aplicaciones();

        private string aplicacion;
        private string descripcion;

        private XmlDocument datosExtraXML;
        private XmlDocument resultadoXML;

        public string Aplicacion
        {
            get { return aplicacion; }
            set { aplicacion = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public XmlDocument DatosExtraXML
        {
            get { return datosExtraXML; }
            set { datosExtraXML = value; }
        }


        public TableroInicialModel(int UsuarioID, int AreaID, int AplicacionID)
        {
            if (aplicaciones.AplicacionesConsulta(UsuarioID, AreaID, AplicacionID))
            {
                resultadoXML = aplicaciones.ResultadoXML;
            }

            if (aplicaciones.AplicacionesConsultaDatosExtra(UsuarioID, AplicacionID))
            {
                datosExtraXML = aplicaciones.AplicacionesDatosExtraXML;
            }
        }
    }
}