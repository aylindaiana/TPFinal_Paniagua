using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class TalleManager
    {
        public List<Talles> ListarActivos()
        {
            List<Talles> list = new List<Talles>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarTallesActivos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Talles aux = new Talles();

                    aux.Id_Talle = (int)datos.Lector["Id_Talle"];
                    aux.Nombre = (string)datos.Lector["NombreTalle"];

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

        public List<Talles> ListarTodos()
        {
            List<Talles> list = new List<Talles>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarTallesTodas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Talles aux = new Talles();

                    aux.Id_Talle = (int)datos.Lector["Id_Talle"];
                    aux.Nombre = (string)datos.Lector["NombreTalle"];
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

        public void Agregar(Talles talle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_AgregarTalle @Nombre");
                datos.SetearParametro("@Nombre", talle.Nombre);

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
        public int ObtenerUltimoIdTalle()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT MAX(Id_Talle) FROM Talles");
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return datos.Lector.GetInt32(0);
                }
                return 0;
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


        public void Modificar(Talles talle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ModificarTalle @IdTalle, @Nombre");
                datos.SetearParametro("@IdTalle", talle.Id_Talle);
                datos.SetearParametro("@Nombre", talle.Nombre);
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
        public void Desactivar(int idTalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_DesactivarTalle @IdTalle");
                datos.SetearParametro("@IdTalle", idTalle);
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
        public void Reactivar(int idTalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ReactivarTalle @IdTalle");
                datos.SetearParametro("@IdTalle", idTalle);
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
        public List<Talles> ObtenerStockPorTalle(int idArticulo)
        {
            List<Talles> lista = new List<Talles>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT T.Id_Talle, T.Nombre, AT.Stock FROM Articulos_Talles AT INNER JOIN Talles T ON AT.Id_Talle = T.Id_Talle WHERE AT.Id_Articulo = @Id_Articulo");

                datos.SetearParametro("@Id_Articulo", idArticulo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Talles item = new Talles
                    {
                        Id_Talle = (int)datos.Lector["Id_Talle"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Stock = (int)datos.Lector["Stock"]
                    };

                    lista.Add(item);
                }

                return lista;
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
        public void AsociarStockArticuloTalle(int idArticulo, int idTalle, int stock)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Debug de los parámetros
                Console.WriteLine($"DEBUG - ArticuloId = {idArticulo}, TalleId = {idTalle}, Stock = {stock}");

                datos.SetearConsulta("EXEC sp_AsociarStockArticuloTalle @ArticuloId, @TalleId, @Stock");

                datos.SetearParametro("@ArticuloId", idArticulo);
                datos.SetearParametro("@TalleId", idTalle);
                datos.SetearParametro("@Stock", stock);

                datos.ejecutarAccion();

                Console.WriteLine($"DEBUG - Stock actualizado para ArticuloId = {idArticulo}, TalleId = {idTalle}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        public List<int> ObtenerArticulosPorTalle(int idTalle)
        {
            List<int> lista = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id_Articulo FROM Articulos_Talles WHERE Id_Talle = @idTalle");
                datos.SetearParametro("@idTalle", idTalle);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add((int)datos.Lector["Id_Articulo"]);
                }

                return lista;
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

        public void EliminarArticulosDeTalle(int idTalle, List<int> articulosDesmarcados)
        {
            if (articulosDesmarcados == null || articulosDesmarcados.Count == 0)
                return; // Evita eliminar todo si la lista está vacía

            AccesoDatos datos = new AccesoDatos();
            try
            {
                string listaEliminar = string.Join(",", articulosDesmarcados);
                Console.WriteLine($"DEBUG - Eliminando artículos: {listaEliminar}");

                datos.SetearConsulta("EXEC sp_EliminarArticulosDeTalle @idTalle, @listaEliminar");
                datos.SetearParametro("@idTalle", idTalle);
                datos.SetearParametro("@listaEliminar", listaEliminar);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en EliminarArticulosDeTalle: {ex.Message}");
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void ActualizarArticulosDeTalle(int idTalle, List<int> articulosSeleccionados, List<int> articulosAnteriores)
        {
            if (articulosSeleccionados == null) articulosSeleccionados = new List<int>();
            if (articulosAnteriores == null) articulosAnteriores = new List<int>();

            List<int> articulosDesmarcados = articulosAnteriores.Except(articulosSeleccionados).ToList(); 
            List<int> articulosNuevos = articulosSeleccionados.Except(articulosAnteriores).ToList(); 

            string listaEliminar = articulosDesmarcados.Count > 0 ? string.Join(",", articulosDesmarcados) : "";
            string listaAgregar = articulosNuevos.Count > 0 ? string.Join(",", articulosNuevos) : "";

            Console.WriteLine("Lista para eliminar: " + listaEliminar);
            Console.WriteLine("Lista para agregar: " + listaAgregar);
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarArticulosDeTalle @idTalle, @listaEliminar, @listaAgregar");
                datos.SetearParametro("@idTalle", idTalle);
                datos.SetearParametro("@listaEliminar", listaEliminar);
                datos.SetearParametro("@listaAgregar", listaAgregar);

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
        public void AsociarStockArticuloTalle(int idArticulo, int idTalle, int stock)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            IF EXISTS (SELECT 1 FROM Articulos_Talles WHERE ArticuloId = @ArticuloId AND TalleId = @TalleId)
                UPDATE Articulos_Talles SET Stock = @Stock WHERE ArticuloId = @ArticuloId AND TalleId = @TalleId
            ELSE
                INSERT INTO Articulos_Talles (ArticuloId, TalleId, Stock) VALUES (@ArticuloId, @TalleId, @Stock)");

                datos.SetearParametro("@ArticuloId", idArticulo);
                datos.SetearParametro("@TalleId", idTalle);
                datos.SetearParametro("@Stock", stock);

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
        }*/

        public void ActualizarStockTotal(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            UPDATE Articulos
            SET Stock = (
                SELECT SUM(Stock) 
                FROM Articulo_Talle 
                WHERE Id_Articulo = @Id_Articulo
            )
            WHERE Id_Articulo = @Id_Articulo");

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


    }
}
