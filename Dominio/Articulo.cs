﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Articulo
    {
        public int Id_Articulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Imagenes> Imagenes { get; set; } = new List<Imagenes>();
        public int Stock {  get; set; }
        public void CalcularStockTotal()
        {
            this.Stock = Talles.Sum(t => t.Stock);
        }
        public int Talle { get; set; }


        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public int TipoId { get; set; }
        public List<Talles> Talles { get; set; } = new List<Talles>();
        public bool Estado { get; set; }

        //en caso de sumarse, tomar como una lista de listas a las Marcas-
        //tener en cuenta que una buena práctica es sumar el precio anterior

    }
}
