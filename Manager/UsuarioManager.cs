using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class UsuarioManager
    {
        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id_Usuario, Email, Contra, AccesoId, Estado FROM Usuarios WHERE Email = @Email AND Contra = @Contra AND Estado = 1 ");
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Contra", usuario.Pass);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id_Usuario = (int)datos.Lector["Id_Usuario"];
                    usuario.IdAcceso = (int)datos.Lector["AccesoId"];
                    usuario.Estado = (bool)datos.Lector["Estado"];

                    return true;
                }
                else
                {

                    return false;
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
    }
}
