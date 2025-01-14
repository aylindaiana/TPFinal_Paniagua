using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class AccesoManager
    {
        public List<Acceso> Listar()
        {
            List<Acceso> lista = new List<Acceso>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id_Acceso, Nombre FROM Acceso");
                datos.EjecutarLectura();

                while(datos.Lector.Read())
                {
                    Acceso aux = new Acceso();
                    aux.Id_Acceso = (int)datos.Lector["Id_Acceso"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(aux);
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
    }
}
