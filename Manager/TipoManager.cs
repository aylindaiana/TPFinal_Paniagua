using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class TipoManager
    {
        public List<Tipo> ListarActivos()
        {
            List<Tipo> list = new List<Tipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarTiposActivos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Tipo aux = new Tipo();

                    aux.Id_Tipo = (int)datos.Lector["Id_Tipo"];
                    aux.Nombre = (string)datos.Lector["NombreTipo"];

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

        public List<Tipo> ListarTodos()
        {
            List<Tipo> list = new List<Tipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ListarTiposTodas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Tipo aux = new Tipo();

                    aux.Id_Tipo = (int)datos.Lector["Id_Tipo"];
                    aux.Nombre = (string)datos.Lector["NombreTipo"];
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
        public List<Tipo> ObtenerTiposPorCategoria(int categoriaId)
        {
            List<Tipo> tipos = new List<Tipo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC ObtenerTiposPorCategoria @CategoriaId");
                datos.SetearParametro("@CategoriaId", categoriaId);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Tipo tipo = new Tipo
                    {
                        Id_Tipo = (int)datos.Lector["Id_Tipo"],
                        Nombre = (string)datos.Lector["Nombre"]
                    };

                    tipos.Add(tipo);
                }

                return tipos;
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

        public void Agregar(Tipo tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_AgregarTipo @Nombre, @CategoriaId");
                datos.SetearParametro("@Nombre", tipo.Nombre);
                datos.SetearParametro("@CategoriaId", tipo.CategoriaId);
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

        public void Modificar(Tipo tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ModificarTipo @IdTipo, @Nombre, @CategoriaId");
                datos.SetearParametro("@IdTipo", tipo.Id_Tipo);
                datos.SetearParametro("@Nombre", tipo.Nombre);
                datos.SetearParametro("@CategoriaId", tipo.CategoriaId);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        public void Desactivar(int idTipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_DesactivarTipo @IdTipo");
                datos.SetearParametro("@IdTipo", idTipo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        public void Reactivar(int idTipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_ReactivarTipo @IdTipo");
                datos.SetearParametro("@Id_Tipo", idTipo);
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
