using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {
        public int Id_Detalle {  get; set; }
        public Usuario Usuario { get; set; }
        public CarrtitoCompras Carrito { get; set; }

    }
}
