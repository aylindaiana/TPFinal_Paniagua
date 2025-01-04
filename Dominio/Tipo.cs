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
     //   public int Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }
        public bool Estado { get; set; }
    }
}
