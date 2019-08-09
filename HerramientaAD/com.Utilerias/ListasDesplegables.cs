using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerramientaAD.com.Utilerias
{
    public class ListasDesplegables
    {
        private int indice;
        public int Indice
        {
            get { return indice; }
            set { indice = value; }
        }

        private string texto;
        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        public ListasDesplegables(int Indice, string Texto)
        {
            indice = Indice;
            texto = Texto;
        }
    }
}