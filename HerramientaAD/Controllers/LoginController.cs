using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using HerramientaAD.Models;
using HerramientaAD.com.Datos;

namespace HerramientaAD.Controllers
{
    public class LoginController : Controller
    {
        LoginModel loginModel = new LoginModel();
        Usuarios usuarios = new Usuarios();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", loginModel);
            }

            if (usuarios.UsuariosValida(loginModel.Usuario, loginModel.Contraseña))
            {
                XmlNode xmlNode = usuarios.ResultadoXML.DocumentElement.SelectSingleNode("Usuario");
                XmlNode datosUsuario = xmlNode.ChildNodes[0];

                Session["UsuarioID"] = datosUsuario.Attributes["UsuarioID"].Value.ToString();
                Session["UsuarioNombre"] = datosUsuario.Attributes["Nombre"].Value.ToString() + " " + 
                                           datosUsuario.Attributes["Apellido_Paterno"].Value.ToString() + " " + 
                                           datosUsuario.Attributes["Apellido_Materno"].Value.ToString();

                
                return RedirectToAction("Index", "Tableros");
            }

            ViewBag.Message = "Usuario no Registrado";
            return View("Index", loginModel);
        }
    }
}