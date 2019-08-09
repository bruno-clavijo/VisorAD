using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using HerramientaAD.Models;
using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

namespace HerramientaAD.Controllers
{
    public class DetalleTecnicoController : Controller
    {
        
        DatosDetalleTecnico datosDetalleTecnico = new DatosDetalleTecnico();
        List<ListasDesplegables> aplicacionesLista = new List<ListasDesplegables>();
        List<ListasDesplegables> filtroLista = new List<ListasDesplegables>();
        DetalleTecnicoModel detalleTecnicoModel = new DetalleTecnicoModel();

        // GET: DetalleTecnico
        public ActionResult Index()
        {
            var detalleTecnicoModel = new DetalleTecnicoModel(int.Parse(Session["UsuarioID"].ToString()));
            return View(detalleTecnicoModel);
        }

        public JsonResult ActualizarFiltros(string Filtro, string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3)
        {
            if (datosDetalleTecnico.ObtenFiltros(Filtro, Tipo, AplicacionID, Filtro1, Filtro2, Filtro3))
            {
                filtroLista.Add(new ListasDesplegables(0, "Selecciona"));
                XmlNode xmlNode = datosDetalleTecnico.ResultadoXML.DocumentElement.SelectSingleNode("Filtros");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    filtroLista.Add(new ListasDesplegables(
                        int.Parse(elemento.Attributes["Numero"].Value.ToString()), 
                        elemento.Attributes["Nombre"].Value.ToString())
                        );
                }

            }
            return Json(new SelectList(filtroLista, "Indice", "Texto"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarAplicaciones(int AreaID)
        {
            Aplicaciones aplicaciones = new Aplicaciones();

            if (aplicaciones.AplicacionesConsulta(1, AreaID, 0))
            {
                aplicacionesLista.Add(new ListasDesplegables(0, "Selecciona"));
                XmlNode xmlNode = aplicaciones.ResultadoXML.DocumentElement.SelectSingleNode("Aplicaciones");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    aplicacionesLista.Add(new ListasDesplegables(
                        int.Parse(elemento.Attributes["AplicacionID"].Value.ToString()), 
                        elemento.Attributes["Aplicacion"].Value.ToString())
                        );
                }
            }

            return Json(new SelectList(aplicacionesLista, "Indice", "Texto"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarConsulta(string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3, string Filtro4)
        {
            
            detalleTecnicoModel.DetalleXML = ObtenerDetalle(Tipo, AplicacionID, Filtro1, Filtro2, Filtro3, Filtro4);
            return PartialView("Detalle", detalleTecnicoModel);
        }   

        public XmlDocument ObtenerDetalle(string Tipo, int AplicacionID, string Filtro1, string Filtro2, string Filtro3, string Filtro4)
        {
            XmlDocument detalleXML = new XmlDocument();
            if (datosDetalleTecnico.ConsultaDetalleFiltro(Tipo, AplicacionID, Filtro1, Filtro2, Filtro3, Filtro4))
            {
                detalleXML = datosDetalleTecnico.ResultadoXML;
            }
            return detalleXML;
        }
    }
}