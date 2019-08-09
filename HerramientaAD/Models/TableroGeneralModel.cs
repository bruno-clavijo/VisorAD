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
    public class TableroGeneralModel
    {
        Aplicaciones aplicaciones = new Aplicaciones();

        private XmlDocument resultadoXML;

        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public TableroGeneralModel(int UsuarioID, int AreaID, int Aplicacion)
        {
            if (aplicaciones.AplicacionesConsulta(UsuarioID, AreaID, Aplicacion))
            {
                resultadoXML = aplicaciones.ResultadoXML;
            }
        }
    }
}