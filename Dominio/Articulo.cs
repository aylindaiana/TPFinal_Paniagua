using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id_Articulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<string> ImagenURL { get; set; }
        public int Stock {  get; set; }
        public decimal Precio { get; set; }

        public Categoria Categoria { get; set; }
        public bool Estado { get; set; }
    }
}
