using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HerramientaAD.Models;

namespace HerramientaAD.Controllers
{
    public class TablerosController : Controller
    {
        // GET: Tableros
        public ActionResult Index()
        {
            var tableroGeneralModel = new TableroGeneralModel(int.Parse(Session["UsuarioID"].ToString()), 0, 0);

            return View(tableroGeneralModel);
        }
        
        public ActionResult Inicial(int AplicacionID)
        {
            var tableroInicialModel = new TableroInicialModel(int.Parse(Session["UsuarioID"].ToString()), 0, AplicacionID);
            ViewBag.Aplicacion = AplicacionID;
            return View(tableroInicialModel);
        }

        public ActionResult GrupoBD(int AplicacionID)
        {
            var grupoBDModel = new GrupoBDModel(int.Parse(Session["UsuarioID"].ToString()), AplicacionID);
            ViewBag.Aplicacion = AplicacionID;
            return View(grupoBDModel);
        }

        public ActionResult AnalisisBD(int AplicacionID)
        {
            var analisisBD = new AnalisisBD(int.Parse(Session["UsuarioID"].ToString()), AplicacionID);
            ViewBag.Aplicacion = AplicacionID;
            return View(analisisBD);
        }

        public ActionResult GrupoWS(int AplicacionID)
        {
            var grupoWSModel = new GrupoWSModel(int.Parse(Session["UsuarioID"].ToString()), AplicacionID);
            ViewBag.Aplicacion = AplicacionID;
            return View(grupoWSModel);
        }

        public ActionResult GrupoCM(int AplicacionID)
        {
            var grupoCMModel = new GrupoCMModel(int.Parse(Session["UsuarioID"].ToString()), AplicacionID);
            ViewBag.Aplicacion = AplicacionID;
            return View(grupoCMModel);
        }

        public ActionResult DiagramaUML(int AplicacionID)
        {
            ViewBag.Aplicacion = AplicacionID;
            return View();
        }
    }
}