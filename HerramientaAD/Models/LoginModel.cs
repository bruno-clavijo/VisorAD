using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HerramientaAD.Models
{
    public class LoginModel
    {
        private int usuarioID;
        private string nombreUsuario;
        private string errores;
        private string usuario;
        private string contraseña;

        public int UsuarioID
        {
            get { return usuarioID; }
            set { usuarioID = value; }
        }

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        [Display(Name = "")]
        public string Errores
        {
            get { return errores; }
            set { errores = value; }
        }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Usuario es requerido.")]
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña es requerida.")]
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }
    }
}