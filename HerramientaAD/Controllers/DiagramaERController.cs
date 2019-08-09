using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;

using HerramientaAD.Models;

namespace HerramientaAD.Controllers
{
    public class DiagramaERController : Controller
    {
        List<ListasDesplegables> aplicacionesLista = new List<ListasDesplegables>();
        List<ListasDesplegables> baseLista = new List<ListasDesplegables>();
        DatosDetalleTecnico datosDetalleTecnico = new DatosDetalleTecnico();
        DatosDiagramaER datosDiagramaER = new DatosDiagramaER();
        
        // GET: DiagramaER
        public ActionResult Index(int BaseDeDatosID)
        {
            var diagramaERModel = new DiagramaERModel(int.Parse(Session["UsuarioID"].ToString()), BaseDeDatosID);
            return View(diagramaERModel);
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

        public JsonResult ActualizarBase(string Filtro, string Tipo, int AplicacionID)
        {
            if (AplicacionID > 0)
            {
                if (datosDetalleTecnico.ObtenFiltros(Filtro, Tipo, AplicacionID, string.Empty, string.Empty, string.Empty))
                {
                    baseLista.Add(new ListasDesplegables(0, "Selecciona"));
                    XmlNode xmlNode = datosDetalleTecnico.ResultadoXML.DocumentElement.SelectSingleNode("Filtros");
                    foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                    {
                        baseLista.Add(new ListasDesplegables(
                            int.Parse(elemento.Attributes["Numero"].Value.ToString()),
                            elemento.Attributes["Nombre"].Value.ToString())
                            );
                    }
                }
            }
            
            return Json(new SelectList(baseLista, "Indice", "Texto"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarDiagrama(int BaseDeDatosID, DiagramaERModel diagramaERModel)
        {
            diagramaERModel.ResultadoXML = ObtenerDatos(1, int.Parse(Session["UsuarioID"].ToString()), BaseDeDatosID);
            XmlNode xmlNode = datosDiagramaER.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
            foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
            {
                diagramaERModel.Cuadros.Add(new ElementosDiagramaER.Cuadros(
                    int.Parse(elemento.Attributes["Numero"].Value.ToString()),
                    elemento.Attributes["Tabla"].Value.ToString())
                    );
            }

            diagramaERModel.ResultadoXML = ObtenerDatos(2, int.Parse(Session["UsuarioID"].ToString()), BaseDeDatosID);
            xmlNode = datosDiagramaER.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
            foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
            {
                diagramaERModel.Relaciones.Add(new ElementosDiagramaER.Relaciones(
                    int.Parse(elemento.Attributes["From"].Value.ToString()),
                    int.Parse(elemento.Attributes["To"].Value.ToString()),
                    elemento.Attributes["Text"].Value.ToString())
                    );
            }

            return PartialView("Diagrama", diagramaERModel);
        }

        public XmlDocument ObtenerDatos(int Tipo, int UsuarioID, int BaseDeDatosID)
        {
            XmlDocument detalleXML = new XmlDocument();
            if (datosDiagramaER.DiagramaERConsulta(Tipo, UsuarioID, BaseDeDatosID))
            {
                detalleXML = datosDiagramaER.ResultadoXML;
            }
            return detalleXML;
        }

     

    }
}