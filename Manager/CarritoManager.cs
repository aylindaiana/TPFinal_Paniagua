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
        public void AgregarCarrito(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO CarritoCompras (UsuarioId, ImporteTotal, FechaCreacion) VALUES(@UsuarioId, 0, GETDATE())");
                datos.SetearParametro("@UsuarioId", idUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message); ;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
    }
}
