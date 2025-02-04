using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class DetalleManager
    {
        public List<DetalleCompra> ListarTodos()
        {
            List<DetalleCompra> list = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM vw_ListarTodasLasCompras ORDER BY Fecha_Compra DESC");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Id_DetalleCompra = (int)datos.Lector["Id_DetalleCompra"];
                    detalle.UsuarioId = (int)datos.Lector["UsuarioId"];
                    detalle.CarritoCompraId = (int)datos.Lector["CarritoCompraId"];
                    detalle.Fecha_Compra = (DateTime)datos.Lector["Fecha_Compra"];
                    detalle.ImporteTotal = (Decimal)datos.Lector["ImporteDetalleCompra"];
                    detalle.DireccionEntregar = (string)datos.Lector["DireccionEntregar"];
                    detalle.EstadoCompraId = (int)datos.Lector["Id_EstadoCompra"];
                    detalle.EstadoCompra = (string)datos.Lector["EstadoCompra"];
                    detalle.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    detalle.ApellidoUsuario = (string)datos.Lector["ApellidoUsuario"];
                    detalle.EmailUsuario = (string)datos.Lector["EmailUsuario"];

                    list.Add(detalle);
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {
                datos.CerrarConeccion();
            }
            return list;
        }

        public List<DetalleCompra> ObtenerPorDetalle(int idUsuario)
        {
            List<DetalleCompra> list = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC spObtenerDetalleCompra @IdUsuario");
                datos.SetearParametro("@IdUsuario", idUsuario);

                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Id_DetalleCompra = (int)datos.Lector["Id_DetalleCompra"];
                    detalle.UsuarioId = (int)datos.Lector["UsuarioId"];
                    detalle.CarritoCompraId = (int)datos.Lector["CarritoCompraId"];
                    detalle.Fecha_Compra = (DateTime)datos.Lector["Fecha_Compra"];
                    detalle.ImporteTotal = (Decimal)datos.Lector["ImporteDetalleCompra"];
                    detalle.DireccionEntregar = (string)datos.Lector["DireccionEntregar"];
                    detalle.EstadoCompraId = (int)datos.Lector["Id_EstadoCompra"];
                    detalle.EstadoCompra = (string)datos.Lector["EstadoCompra"];
                    detalle.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    detalle.ApellidoUsuario = (string)datos.Lector["ApellidoUsuario"];
                    detalle.EmailUsuario = (string)datos.Lector["EmailUsuario"];

                    list.Add(detalle);
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {
                datos.CerrarConeccion();
            }
            return list;
        }

        public bool CambiarEstadoCompraCiclo(int idDetalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_CambiarEstadoCompraCiclo @Id_DetalleCompra");
                datos.SetearParametro("@Id_DetalleCompra", idDetalle);
                datos.ejecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        public void Agregar(DetalleCompra detalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {/*
                if (!CarritoPerteneceAlUsuario(detalle.CarritoCompraId, detalle.UsuarioId))
                {
                    throw new InvalidOperationException("El carrito de compra no existe o no pertenece al usuario.");
                }*/
                datos.SetearConsulta("EXEC sp_InsertarPedido @UsuarioId, @CarritoCompraId, @ImporteTotal, @DireccionEntregar, 1");
                datos.SetearParametro("@UsuarioId", detalle.UsuarioId);
                datos.SetearParametro("@CarritoCompraId", detalle.CarritoCompraId);
                datos.SetearParametro("@ImporteTotal", detalle.ImporteTotal);
                datos.SetearParametro("@DireccionEntregar", detalle.DireccionEntregar);
                datos.SetearParametro("@EstadoCompraId", detalle.EstadoCompraId);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        private bool CarritoPerteneceAlUsuario(int carritoCompraId, int usuarioId)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM CarritoCompras WHERE Id_CarritoCompra = @CarritoCompraId AND UsuarioId = @UsuarioId");
                datos.SetearParametro("@CarritoCompraId", carritoCompraId);
                datos.SetearParametro("@UsuarioId", usuarioId);
                int count = (int)datos.ejecutarEscalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el carrito de compra: " + ex.Message);
            }
        }

    }
}
