using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Talles
    {
        public int Id_Talle {  get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
    }
}
