using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleArticulo
    {
        public int Id_DetalleArticulo { get; set; }
        public int DetalleCompraId { get; set; }
        public int ArticuloId { get; set; }
        public string NombreArticulo { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioUnidad { get; set; }
    }
}
