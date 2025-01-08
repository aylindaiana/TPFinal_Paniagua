using Dominio;
using System;
using System.Collections;
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

        public List<Usuario> Listar()
        {
            List<Usuario> list = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, Fecha_nacimiento, AccesoId, Estado FROM Usuarios");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.Id_Usuario = (int)datos.Lector["Id_Usuario"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Pass = (string)datos.Lector["Contra"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Localidad = (string)datos.Lector["Localidad"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.IdAcceso = (int)datos.Lector["AccesoId"];

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
