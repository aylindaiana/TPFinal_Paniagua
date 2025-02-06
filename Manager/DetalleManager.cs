using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            {
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
        public List<DetalleArticulo> ObtenerArticulosPorDetalleCompra(int detalleCompraId)
        {
            List<DetalleArticulo> lista = new List<DetalleArticulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_ObtenerArticulosPorDetalleCompra @DetalleCompraId");
                datos.SetearParametro("@DetalleCompraId", detalleCompraId);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleArticulo detalle = new DetalleArticulo
                    {
                        ArticuloId = (int)datos.Lector["ArticuloId"],
                        NombreArticulo = (string)datos.Lector["NombreArticulo"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnidad = (decimal)datos.Lector["PrecioUnidad"]
                    };

                    lista.Add(detalle);
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

            return lista;
        }

        public bool ActualizarDetalleCompra(int idCompra,int articuloId, int nuevaCantidad, decimal nuevoPrecio)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE DetalleArticulo SET Cantidad = @Cantidad, PrecioUnidad = @PrecioUnidad WHERE DetalleCompraId = @IdCompra AND ArticuloId = @ArticuloId");
                datos.SetearParametro("@Cantidad", nuevaCantidad);
                datos.SetearParametro("@PrecioUnidad", nuevoPrecio);
                datos.SetearParametro("@IdCompra", idCompra);
                datos.SetearParametro("@ArticuloId", articuloId);

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
        public bool EliminarCompra(int idCompra)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Hacer el delate con procedimiento almacenado para que afecte a las tablas
                // de : carrito, detalle y articulo
              //  datos.SetearConsulta("UPDATE DetalleArticulo SET Cantidad = @Cantidad, PrecioUnidad = @PrecioUnidad WHERE DetalleCompraId = @IdCompra AND ArticuloId = @ArticuloId");
                datos.SetearParametro("@IdCompra", idCompra);
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
    }
}
