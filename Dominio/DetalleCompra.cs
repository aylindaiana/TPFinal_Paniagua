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

        public string RutaFactura { get; set; }

        public DetalleCompra()
        {
            Id_DetalleCompra = 0;
            UsuarioId = 0;
            CarritoCompraId = 0;
            EstadoCompraId = 0;
            ImporteTotal = 0;
            DireccionEntregar = "0";
            Fecha_Compra = DateTime.Now;
            EstadoCompra = "";
            NombreUsuario = "";
            ApellidoUsuario = "";
            EmailUsuario = "";
        }
        public DetalleCompra(int id_DetalleCompra, int usuarioId, int carritoCompraId, decimal importeTotal, string direccionEntregar, int estadoCompraId, DateTime fecha_Compra, string estadoCompra, string nombreUsuario, string apellidoUsuario, string emailUsuario)
        {
            Id_DetalleCompra = id_DetalleCompra;
            UsuarioId = usuarioId;
            CarritoCompraId = carritoCompraId;
            ImporteTotal = importeTotal;
            DireccionEntregar = direccionEntregar;
            EstadoCompraId = estadoCompraId;
            Fecha_Compra = fecha_Compra;
            EstadoCompra = estadoCompra;
            NombreUsuario = nombreUsuario;
            ApellidoUsuario = apellidoUsuario;
            EmailUsuario = emailUsuario;
        }
    }
}
