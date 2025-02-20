using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ArticuloManager
    {

        public List<Articulo> ListarArticulosActivos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosActivos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["Id_Articulo"];
                    Articulo articulo = listaArticulos.FirstOrDefault(a => a.Id_Articulo == idArticulo);

                    if (articulo == null)
                    {
                        articulo = new Articulo
                        {
                            Id_Articulo = idArticulo,
                            Nombre = datos.Lector["NombreArticulo"].ToString(),
                            Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? datos.Lector["Descripcion"].ToString() : string.Empty,
                            Stock = (int)datos.Lector["Stock"],
                            Precio = (decimal)datos.Lector["Precio"],
                            CategoriaId = (int)datos.Lector["CategoriaId"],
                            TipoId = (int)datos.Lector["TipoId"],
                            Estado = true,
                            Imagenes = new List<Imagenes>()
                        };
                        listaArticulos.Add(articulo);
                    }

                    if (datos.Lector["Imagen"] != DBNull.Value)
                    {
                        string urlImagen = datos.Lector["Imagen"].ToString();
                        if (!articulo.Imagenes.Any(img => img.UrlImagen == urlImagen))
                        {
                            articulo.Imagenes.Add(new Imagenes { UrlImagen = urlImagen });
                        }
                    }
                }

                return listaArticulos;
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




        public List<Articulo> ListarArticulosTodos()
        {
            List<Articulo> list = new List<Articulo>();
            Dictionary<int, Articulo> articulosDict = new Dictionary<int, Articulo>(); 
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosTodos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["Id_Articulo"];

                    if (!articulosDict.ContainsKey(idArticulo))
                    {
                        Articulo aux = new Articulo
                        {
                            Id_Articulo = idArticulo,
                            Nombre = (string)datos.Lector["NombreArticulo"],
                            Descripcion = (string)datos.Lector["Descripcion"],
                            Precio = (decimal)datos.Lector["Precio"],
                            Stock = (int)datos.Lector["Stock"],
                            CategoriaId = (int)datos.Lector["CategoriaId"],
                            TipoId = (int)datos.Lector["TipoId"],
                            Estado = (bool)datos.Lector["Estado"]
                        };

                        articulosDict.Add(idArticulo, aux);
                    }

                    string imagenUrl = datos.Lector["ImagenUrl"] as string;
                    if (!string.IsNullOrEmpty(imagenUrl))
                    {
                        articulosDict[idArticulo].Imagenes.Add(new Imagenes { UrlImagen = imagenUrl });
                    }
                }

                list = articulosDict.Values.ToList(); 
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

        public List<Articulo> ListarArticulosDetalleCompra()
        {
            List<Articulo> listaArticulos = new List<Articulo>();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarArticulosDetalleCompra");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo
                    {
                        Id_Articulo = (int)datos.Lector["Id_Articulo"],
                        Nombre = datos.Lector["NombreArticulo"].ToString(),
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? datos.Lector["Descripcion"].ToString() : string.Empty,
                        Stock = (int)datos.Lector["Stock"],
                        Precio = (decimal)datos.Lector["Precio"],
                        CategoriaId = (int)datos.Lector["CategoriaId"],
                        TipoId = (int)datos.Lector["TipoId"],
                        Estado = true,
                        Imagenes = new List<Imagenes>()
                    };

                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Imagenes")))
                    {
                        string imagenesConcatenadas = datos.Lector["Imagenes"].ToString();
                        if (!string.IsNullOrEmpty(imagenesConcatenadas))
                        {
                            // 🔹 Dividir las imágenes separadas por ";"
                            string[] imagenesArray = imagenesConcatenadas.Split(';');
                            foreach (string imagen in imagenesArray)
                            {
                                articulo.Imagenes.Add(new Imagenes { UrlImagen = imagen.Trim() });
                            }
                        }
                    }


                    listaArticulos.Add(articulo);
                }

                return listaArticulos;
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


        public int ObtenerStockTalle(int idArticulo, int idTalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            SELECT Stock 
            FROM Articulos_Talles
            WHERE Id_Articulo = @Id_Articulo AND Id_Talle = @Id_Talle");


                datos.SetearParametro("@Id_Articulo", idArticulo);
                datos.SetearParametro("@Id_Talle", idTalle);
                datos.EjecutarLectura();

                if (datos.Lector.Read() && datos.Lector["Stock"] != DBNull.Value)
                {
                    return (int)datos.Lector["Stock"];
                }
                else
                {
                    return 0;
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

        public int ObtenerStockPorTalle(int idArticulo, int idTalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            SELECT Stock 
            FROM Articulos_Talles 
            WHERE Id_Articulo = @Id_Articulo AND Id_Talle = @Id_Talle");

                datos.SetearParametro("@Id_Articulo", idArticulo);
                datos.SetearParametro("@Id_Talle", idTalle);
                datos.EjecutarLectura();

                if (datos.Lector.Read() && datos.Lector["Stock"] != DBNull.Value)
                {
                    return (int)datos.Lector["Stock"];
                }
                else
                {
                    return 0; // Si no encuentra el stock, devuelve 0
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

        public void ActualizarStockTalle(int idArticulo, int idTalle, int cantidadVendida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarStockTalle @idArticulo, @idTalle, @cantidadVendida");
                datos.SetearParametro("@idArticulo", idArticulo);
                datos.SetearParametro("@idTalle", idTalle);
                datos.SetearParametro("@cantidadVendida", cantidadVendida);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error SQL: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void ActualizarStock(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarStock @idArticulo");
                datos.SetearParametro("@idArticulo", idArticulo);
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

        /*
        public void ActualizarStock(int idArticulo, int cantidadVendida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarStock @idArticulo, @cantidadVendida");
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
        }*/
        /*
        public void ActualizarStock(int idArticulo, int idTalle, int cantidadVendida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Ejecutamos el procedimiento almacenado con los tres parámetros
                datos.SetearConsulta("EXEC sp_ActualizarStockTalle @idArticulo, @idTalle, @cantidadVendida");
                datos.SetearParametro("@idArticulo", idArticulo);
                datos.SetearParametro("@idTalle", idTalle);
                datos.SetearParametro("@cantidadVendida", cantidadVendida);
                datos.ejecutarAccion();

                // Validamos que el stock no haya quedado negativo
                int stockActualizado = ObtenerStock(idArticulo, idTalle);
                if (stockActualizado < 0)
                {
                    throw new InvalidOperationException($"No hay suficiente stock para el artículo con ID: {idArticulo} y Talle: {idTalle}.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */

        public int Agregar(Articulo articulo)
        {
            int articuloId = 0;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DECLARE @Id_Articulo INT; EXEC sp_AgregarArticulo @Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @TipoId, @Id_Articulo OUTPUT; SELECT @Id_Articulo;");
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("@Stock", articulo.Stock);
                datos.SetearParametro("@CategoriaId", articulo.CategoriaId);
                datos.SetearParametro("@TipoId", articulo.TipoId);

                //int
                articuloId = Convert.ToInt32(datos.ejecutarEscalar());

                //----
                articulo.Id_Articulo = articuloId;

                //----

                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    foreach (Imagenes imagen in articulo.Imagenes)
                    {
                        datos.SetearConsulta("INSERT INTO Imagenes (ImagenURL, ArticuloId) VALUES (@ImagenURL, @ArticuloId)");
                        datos.SetearParametro("@ImagenURL", imagen.UrlImagen);
                        datos.SetearParametro("@ArticuloId", articuloId);
                        datos.ejecutarAccion();
                    }
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
            return articuloId;
        }


        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ModificarArticulo @Id_Articulo, @Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @TipoId, @Estado");
                datos.SetearParametro("@Id_Articulo", articulo.Id_Articulo);
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("@Stock", articulo.Stock);
                datos.SetearParametro("@CategoriaId", articulo.CategoriaId);
                datos.SetearParametro("@TipoId", articulo.TipoId);
                datos.SetearParametro("@Estado", articulo.Estado);
                datos.ejecutarAccion();

                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    datos.SetearConsulta("DELETE FROM Imagenes WHERE ArticuloId = @Id_Articulo");
                    datos.SetearParametro("@Id_Articulo", articulo.Id_Articulo);
                    datos.ejecutarAccion();

                    foreach (Imagenes imagen in articulo.Imagenes)
                    {
                        datos.SetearConsulta("INSERT INTO Imagenes (ImagenURL, ArticuloId) VALUES (@ImagenURL, @Id_Articulo)");
                        datos.SetearParametro("@ImagenURL", imagen.UrlImagen);
                        datos.SetearParametro("@Id_Articulo", articulo.Id_Articulo);
                        datos.ejecutarAccion();
                    }
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

        public List<Articulo> ListarArticulosPorCategoria(int idCategoria)
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
                    Articulo aux = new Articulo
                    {
                        Id_Articulo = (int)datos.Lector["Id_Articulo"],
                        Nombre = datos.Lector["NombreArticulo"] as string ?? "Sin nombre",
                        Descripcion = datos.Lector["Descripcion"] as string ?? "Sin descripción",
                        Precio = datos.Lector["Precio"] != DBNull.Value ? (decimal)datos.Lector["Precio"] : 0,
                        Imagenes = new List<Imagenes>() 
                    };
                    string imagenesStr = datos.Lector["Imagenes"] as string ?? "";
                    if (!string.IsNullOrEmpty(imagenesStr))
                    {
                        string[] imagenesArray = imagenesStr.Split(',');
                        foreach (string url in imagenesArray)
                        {
                            aux.Imagenes.Add(new Imagenes { UrlImagen = url });
                        }
                    }

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
                    Articulo aux = new Articulo
                    {
                        Id_Articulo = (int)datos.Lector["Id_Articulo"],
                        Nombre = datos.Lector["NombreArticulo"] as string ?? "Sin nombre",
                        Descripcion = datos.Lector["Descripcion"] as string ?? "Sin descripción",
                        Precio = datos.Lector["Precio"] != DBNull.Value ? (decimal)datos.Lector["Precio"] : 0,
                        Imagenes = new List<Imagenes>() 
                    };

                    string imagenesStr = datos.Lector["Imagenes"] as string ?? "";
                    if (!string.IsNullOrEmpty(imagenesStr))
                    {
                        string[] imagenesArray = imagenesStr.Split(',');
                        foreach (string url in imagenesArray)
                        {
                            aux.Imagenes.Add(new Imagenes { UrlImagen = url });
                        }
                    }

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
