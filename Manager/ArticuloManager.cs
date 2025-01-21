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
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.CategoriaId = (string)datos.Lector["CategoriaId"];
                    aux.TipoId = (string)datos.Lector["TipoId"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];
                    aux.Estado = (bool)datos.Lector["Estado"];

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



        public void Agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_AgregarArticulo");
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("Stock", articulo.Stock);
                datos.SetearParametro("@Categoria", articulo.CategoriaId);
                datos.SetearParametro("@Tipo", articulo.TipoId);
                datos.SetearParametro("@ImagenURL", articulo.ImagenURL);
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
                datos.SetearConsulta("EXEC sp_ModificarArticulo");
                datos.SetearParametro("@Id_Articulo", articulo.Id_Articulo);
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("Stock", articulo.Stock);
                datos.SetearParametro("@Categoria", articulo.CategoriaId);
                datos.SetearParametro("@Tipo", articulo.TipoId);
                datos.SetearParametro("@ImagenURL", articulo.ImagenURL);
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
                datos.SetearConsulta("EXEC sp_DesactivarArticulo");
                datos.SetearParametro("@ArticuloId", idArticulo);

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
                datos.SetearConsulta("EXEC sp_ReactivarArticulo");
                datos.SetearParametro("@ArticuloId", idArticulo);
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

    }
}
