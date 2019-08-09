using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HerramientaAD.com.Datos;
using HerramientaAD.com.Utilerias;
using System.Xml;


namespace HerramientaAD.Models
{
    public class AnalisisBD
    {
        DatosObjetosBD datosObjetosBD = new DatosObjetosBD();
        
        const int sp_llaveprimaria      = 1;
        const int sp_llaveforanea       = 2;
        const int sp_indexes            = 3;
        const int sp_tipoobjeto         = 4;
        const int sp_tamanotabla        = 5;
        const int sp_lineasEfectivas    = 6;
        const int sp_lineasComentadas   = 7;
        const int sp_select             = 8;
        const int sp_insert             = 9;
        const int sp_update             = 10;
        const int sp_delete             = 11;
        const int sp_siif               = 12;
        const int sp_loop               = 13;
        const int sp_select2            = 14;
        const int sp_excepcion          = 15;

        private List<ElementosDeGrupo.GraficaPie3V> llavePrimaria = new List<ElementosDeGrupo.GraficaPie3V>();
        private List<ElementosDeGrupo.GraficaPie3V> llaveForanea = new List<ElementosDeGrupo.GraficaPie3V>();
        private List<ElementosDeGrupo.GraficaPie3V> indexes = new List<ElementosDeGrupo.GraficaPie3V>();
        private List<ElementosDeGrupo.GraficaColumnas> tipoObjeto = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> tamanoTabla = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> lineasEfectivas = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> lineasComentadas = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> select = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> insert = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> update = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> delete = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> siif = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> loop = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> select2 = new List<ElementosDeGrupo.GraficaColumnas>();
        private List<ElementosDeGrupo.GraficaColumnas> excepcion = new List<ElementosDeGrupo.GraficaColumnas>();
        private XmlDocument resultadoXML;

        public List<ElementosDeGrupo.GraficaPie3V> LlavePrimaria
        {
            get { return llavePrimaria; }
            set { llavePrimaria = value; }

        }

        public List<ElementosDeGrupo.GraficaPie3V> LlaveForanea
        {
            get { return llaveForanea; }
            set { llaveForanea = value; }

        }

        public List<ElementosDeGrupo.GraficaPie3V> Indexes
        {
            get { return indexes; }
            set { indexes = value; }

        }

        public List<ElementosDeGrupo.GraficaColumnas> TipoObjeto
        {
            get { return tipoObjeto; }
            set { tipoObjeto = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> TamanoTabla
        {
            get { return tamanoTabla; }
            set { tamanoTabla = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> LineasEfectivas
        {
            get { return lineasEfectivas; }
            set { lineasEfectivas = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> LineasComentadas
        {
            get { return lineasComentadas; }
            set { lineasComentadas = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Select
        {
            get { return select; }
            set { select = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Insert
        {
            get { return insert; }
            set { insert = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Update
        {
            get { return update; }
            set { update = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Siif
        {
            get { return siif; }
            set { siif = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Select2
        {
            get { return select2; }
            set { select2 = value; }
        }

        public List<ElementosDeGrupo.GraficaColumnas> Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }


        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
            set { resultadoXML = value; }
        }

        public AnalisisBD (int Usuario, int BDId)
        {
            if (datosObjetosBD.AnalisisBDConsulta(sp_llaveprimaria, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    llavePrimaria.Add(new ElementosDeGrupo.GraficaPie3V(
                        elemento.Attributes["Descripcion"].Value.ToString(),
                        int.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        float.Parse(elemento.Attributes["Porc"].Value.ToString())
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_llaveforanea, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    llaveForanea.Add(new ElementosDeGrupo.GraficaPie3V(
                        elemento.Attributes["Descripcion"].Value.ToString(),
                        int.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        float.Parse(elemento.Attributes["Porc"].Value.ToString())
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_indexes, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    indexes.Add(new ElementosDeGrupo.GraficaPie3V(
                        elemento.Attributes["Descripcion"].Value.ToString(),
                        int.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        float.Parse(elemento.Attributes["Porc"].Value.ToString())
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_tipoobjeto, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    tipoObjeto.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_tamanotabla, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    tamanoTabla.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_lineasEfectivas, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    lineasEfectivas.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_lineasComentadas, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    lineasComentadas.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_select, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    select.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_insert, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    insert.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_update, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    update.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_delete, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    delete.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_siif, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    siif.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_loop, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    loop.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_select2, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    select2.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

            if (datosObjetosBD.AnalisisBDConsulta(sp_excepcion, Usuario, BDId))
            {
                XmlNode xmlNode = datosObjetosBD.ResultadoXML.DocumentElement.SelectSingleNode("DatosBD");
                foreach (XmlNode elemento in xmlNode.SelectNodes("row"))
                {
                    excepcion.Add(new ElementosDeGrupo.GraficaColumnas(
                        float.Parse(elemento.Attributes["Valor"].Value.ToString()),
                        elemento.Attributes["Nombre"].Value.ToString()
                        ));
                }
            }

        }



    }
}