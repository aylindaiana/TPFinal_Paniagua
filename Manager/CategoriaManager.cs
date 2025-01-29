using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CategoriaManager
    {
        public List<Categoria> ListarActivos()
        {
            List<Categoria> list = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarCategoriasActivos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id_Categoria = (int)datos.Lector["Id_Categoria"];
                    aux.Nombre = (string)datos.Lector["NombreCategoria"];

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

        public List<Categoria> ListarTodos()
        {
            List<Categoria> list = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarCategoriasTodas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id_Categoria = (int)datos.Lector["Id_Categoria"];
                    aux.Nombre = (string)datos.Lector["NombreCategoria"];
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

        public void Agregar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_AgregarCategoria @Nombre");
                datos.SetearParametro("@Nombre", categoria.Nombre);

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

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ModificarCategoria @IdCategoria, @Nombre");
                datos.SetearParametro("@IdCategoria", categoria.Id_Categoria);
                datos.SetearParametro("@Nombre", categoria.Nombre);
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
        public void Desactivar(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_DesactivarCategoria @IdCategoria");
                datos.SetearParametro("@IdCategoria", idCategoria);
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
        public void Reactivar(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ReactivarCategoria @IdCategoria");
                datos.SetearParametro("@IdCategoria", idCategoria);
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
