using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerramientaAD.com.Utilerias
{
    public class ListasImagen
    {
        private string texto;
        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        private string imagen;
        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        public ListasImagen(string Texto, string Imagen)
        {
            texto = Texto;
            imagen = Imagen;
        }
    }
}