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
    }
}
