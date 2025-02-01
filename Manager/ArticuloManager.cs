using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ArticuloManager
    {
        
        public List<Articulo> ListarArticulosActivos()
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosActivos");
                datos.EjecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["NombreArticulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.CategoriaId = (int)datos.Lector["CategoriaId"];
                    aux.TipoId = (int)datos.Lector["TipoId"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

                    list.Add(aux);
                }
                return list;
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
        /*
        public List<Articulo> ListarArticulosActivos(string sp, int idCategoria)
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(sp);
                datos.SetearParametro("@CategoriaId", idCategoria);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["NombreArticulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.CategoriaId = (string)datos.Lector["NombreCategoria"];
                    aux.TipoId = (string)datos.Lector["NombreTipo"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        } */

        public List<Articulo> ListarArticulosTodos()
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosTodos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["NombreArticulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.CategoriaId = (int)datos.Lector["CategoriaId"];
                    aux.TipoId = (int)datos.Lector["TipoId"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];
                    aux.Estado = (bool)datos.Lector["Estado"];

                    list.Add(aux);
                }
                return list;
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

        public int ObtenerStock(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Stock FROM Articulos WHERE Id_Articulo = @Id_Articulo");
                datos.SetearParametro("@Id_Articulo", idArticulo);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Stock"];
                }
                else
                {
                    throw new InvalidOperationException($"No se encontró el artículo con ID: {idArticulo}.");
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
        }

        public void ActualizarStock(int idArticulo, int cantidadVendida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarStock");
                datos.SetearParametro("@idArticulo", idArticulo);
                datos.SetearParametro("@cantidadVendida", cantidadVendida);
                datos.ejecutarAccion();
                int stockActualizado = ObtenerStock(idArticulo);
                if (stockActualizado < 0)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el stock para el artículo con ID: {idArticulo}. Verifica que haya suficiente stock disponible.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_AgregarArticulo @Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @TipoId, @ImagenesURL");
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("Stock", articulo.Stock);
                datos.SetearParametro("@CategoriaId", articulo.CategoriaId);
                datos.SetearParametro("@TipoId", articulo.TipoId);
                datos.SetearParametro("@ImagenesURL", articulo.ImagenURL);
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

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ModificarArticulo @Id_Articulo, @Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @TipoId,1, @ImagenesURL");
                datos.SetearParametro("@Id_Articulo", articulo.Id_Articulo);
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("Stock", articulo.Stock);
                datos.SetearParametro("@CategoriaId", articulo.CategoriaId);
                datos.SetearParametro("@TipoId", articulo.TipoId);
                datos.SetearParametro("@ImagenesURL", articulo.ImagenURL);
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

        public void Desactivar(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_DesactivarArticulo @Id_Articulo");
                datos.SetearParametro("@Id_Articulo", idArticulo);

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

        public void Reactivar(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ReactivarArticulo @Id_Articulo");
                datos.SetearParametro("@Id_Articulo", idArticulo);
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

        public List<Articulo> ListarArticulosPorCategoria( int idCategoria)
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosPorCategoria @CategoriaId");
                datos.SetearParametro("@CategoriaId", idCategoria);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["NombreArticulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

                    list.Add(aux);
                }
                return list;
            }
            catch (Exception EX)
            {

                throw EX;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public List<Articulo> ListarArticulosPorTipo(int idTipo)
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosPorTipo @TipoId");
                datos.SetearParametro("@TipoId", idTipo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["NombreArticulo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

                    list.Add(aux);
                }
                return list;
            }
            catch (Exception EX)
            {

                throw EX;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public int CantidadClientesActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) AS Cantidad FROM Usuarios WHERE AccesoId = 3 AND Estado= 1");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["Cantidad"];
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

        public int CantidadEmpleadosActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) AS Cantidad FROM Usuarios WHERE AccesoId = 2 AND Estado= 1");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["Cantidad"];
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
        public int CantidadArticulosActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) AS Cantidad FROM Articulos WHERE Estado= 1");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["Cantidad"];
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

        public decimal ImportePrecioTotal()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT SUM(Stock * Precio) AS PrecioTotales FROM Articulos WHERE Estado= 1");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (Decimal)datos.Lector["PrecioTotales"];
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
        public List<Articulo> ListarMenor40()
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("SELECT a.Id_Articulo, a.Nombre, a.Stock, a.Precio from Articulos a WHERE stock < 50");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Precio = (decimal)datos.Lector["Precio"];


                    list.Add(aux);
                }

                return list;
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

        public List<Articulo> ListarMayor50()
        {
            List<Articulo> list = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta("SELECT a.Id_Articulo, a.Nombre, a.Stock, a.Precio from Articulos a WHERE stock > 40");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id_Articulo = (int)datos.Lector["Id_Articulo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    list.Add(aux);
                }

                return list;
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
    }
}
