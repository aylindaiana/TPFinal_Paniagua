using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {
        public int Id_DetalleCompra { get; set; }
        public int UsuarioId { get; set; }
        public int CarritoCompraId { get; set; }
        public decimal ImporteTotal { get; set; }
        public string DireccionEntregar { get; set; }
        public int EstadoCompraId { get; set; }
        public DateTime Fecha_Compra { get; set; }

        public string EstadoCompra { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string EmailUsuario { get; set; }
    }
}
