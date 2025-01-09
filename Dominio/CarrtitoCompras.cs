using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CarrtitoCompras
    {
        public int Id_Carrito {  get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Total { get; set; }
        public DateTime FechaActual { get; set; }
        public int UsuarioId { get; set; }
    }
}
