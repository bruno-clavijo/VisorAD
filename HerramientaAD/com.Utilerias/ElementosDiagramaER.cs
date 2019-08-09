using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerramientaAD.com.Utilerias
{
    public class ElementosDiagramaER
    {
        public class Cuadros
        {
            private int numero;
            public int Numero
            {
                get { return numero; }
                set { numero = value; }
            }

            private string tabla;
            public string Tabla
            {
                get { return tabla; }
                set { tabla = value; }
            }

            public Cuadros(int Numero, string Tabla)
            {
                numero = Numero;
                tabla = Tabla;
            }
        }

        public class Relaciones
        {
            private int desde;
            public int Desde
            {
                get { return desde; }
                set { desde = value; }
            }

            private int para;
            public int Para
            {
                get { return para; }
                set { para = value; }
            }

            private string nombre;
            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            public Relaciones(int Desde, int Para, string Nombre)
            {
                desde = Desde;
                para = Para;
                nombre = Nombre;
            }
        }
    }
}