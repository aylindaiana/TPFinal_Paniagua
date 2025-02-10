using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CarritoManager
    {
        public int AgregarCarrito(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Se usa OUTPUT INSERTED.Id_CarritoCompra para devolver el ID del carrito insertado
                datos.SetearConsulta("INSERT INTO CarritoCompras (UsuarioId, ImporteTotal, FechaCreacion) OUTPUT INSERTED.Id_CarritoCompra VALUES(@UsuarioId, 0, GETDATE())");

                datos.SetearParametro("@UsuarioId", idUsuario);

                int nuevoIdCarrito = (int)datos.ejecutarEscalar(); 

                return nuevoIdCarrito; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el carrito: " + ex.Message);
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

    }
}
