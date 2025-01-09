using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tipo
    {
        public int Id_Tipo { get; set; }
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public bool Estado { get; set; }
    }
}
