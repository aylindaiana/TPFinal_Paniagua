using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
        public List<DetalleCompra> ObtenerUsuarioPorCompra(int idUsuario)
        {
            List<DetalleCompra> list = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ObtenerUsuarioPorCompra @IdUsuario");
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

                    if (!(datos.Lector["RutaFactura"] is DBNull))
                    {
                        detalle.RutaFactura = datos.Lector["RutaFactura"].ToString();
                    }
                    else
                    {
                        detalle.RutaFactura = string.Empty; 
                    }
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
                datos.SetearConsulta("EXEC sp_InsertarPedido @UsuarioId, @CarritoCompraId, @ImporteTotal, @DireccionEntregar, 1, @RutaFactura");
                datos.SetearParametro("@UsuarioId", detalle.UsuarioId);
                datos.SetearParametro("@CarritoCompraId", detalle.CarritoCompraId);
                datos.SetearParametro("@ImporteTotal", detalle.ImporteTotal);
                datos.SetearParametro("@DireccionEntregar", detalle.DireccionEntregar);
                datos.SetearParametro("@EstadoCompraId", detalle.EstadoCompraId);
                datos.SetearParametro("@RutaFactura", string.IsNullOrEmpty(detalle.RutaFactura) ? (object)DBNull.Value : detalle.RutaFactura);
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
        public void RegistrarCompra(DetalleCompra detalle)
        {
            Agregar(detalle);

            int detalleCompraId = ObtenerUltimoDetalleCompra(detalle.UsuarioId);

            if (detalleCompraId > 0)
            {
                string rutaFactura = "/Facturas/Factura_" + detalleCompraId + ".pdf";

                GenerarFactura(detalleCompraId, rutaFactura);

                GuardarRutaFactura(detalleCompraId, rutaFactura);
            }
        }
        public int ObtenerUltimoDetalleCompra(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            SELECT TOP 1 Id_DetalleCompra 
            FROM DetalleCompra 
            WHERE UsuarioId = @IdUsuario 
            ORDER BY Fecha_Compra DESC");

                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Id_DetalleCompra"];
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último detalle de compra.", ex);
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void GenerarFactura(int detalleCompraId, string rutaFactura)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Facturas (DetalleCompraId, RutaFactura) VALUES (@DetalleCompraId, @RutaFactura)");
                datos.SetearParametro("@DetalleCompraId", detalleCompraId);
                datos.SetearParametro("@RutaFactura", rutaFactura);
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
        public bool GuardarRutaFactura(int idDetalleCompra, string rutaFactura)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            UPDATE DetalleCompra 
            SET RutaFactura = @RutaFactura 
            WHERE Id_DetalleCompra = @IdDetalleCompra");

                datos.SetearParametro("@RutaFactura", rutaFactura);
                datos.SetearParametro("@IdDetalleCompra", idDetalleCompra);
                datos.ejecutarAccion();

                return true; // Si se ejecuta correctamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la factura en la base de datos.", ex);
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
                datos.SetearConsulta(@"
            DELETE FROM DetalleArticulo WHERE DetalleCompraId = @IdCompra;
            DELETE FROM CarritoCompras WHERE Id_CarritoCompra IN 
                (SELECT CarritoCompraId FROM DetalleCompra WHERE Id_DetalleCompra = @IdCompra);
            DELETE FROM DetalleCompra WHERE Id_DetalleCompra = @IdCompra;
        ");
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
