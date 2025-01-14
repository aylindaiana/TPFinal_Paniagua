using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                datos.SetearConsulta("SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, AccesoId, Estado FROM Usuarios");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.Id_Usuario = (int)datos.Lector["Id_Usuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
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

        public void Agregar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Usuarios (AccesoId, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, Estado) VALUES (@AccesoId, @Nombre, @Apellido, @Email, @Contra, @Direccion, @Telefono, @Localidad, @FechaNacimiento, 1)");
                datos.SetearParametro("@AccesoId", usuario.IdAcceso);
                datos.SetearParametro("@Nombre", usuario.Nombre);
                datos.SetearParametro("@Apellido", usuario.Apellido);
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Contra", usuario.Pass);
                datos.SetearParametro("@Direccion", usuario.Direccion);
                datos.SetearParametro("@Telefono", usuario.Telefono);
                datos.SetearParametro("@Localidad", usuario.Localidad);
                datos.SetearParametro("@FechaNacimiento", usuario.FechaNacimiento);
                datos.EjecutarLectura();

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

        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Nombre = @Nombre, Apellido= @Apellido, Email = @Email, Contra = @Contra, Direccion = @Direccion, Telefono = @Telefono, Localidad = @Localidad, FechaNacimiento = @FechaNacimiento, Estado= @Estado WHERE Id_Usuario = @Id_Usuario AND AccesoId = @AccesoId");
                datos.SetearParametro("@AccesoId", usuario.IdAcceso);
                datos.SetearParametro("@Id_Usuario", usuario.Id_Usuario);
                datos.SetearParametro("@Nombre", usuario.Nombre);
                datos.SetearParametro("@Apellido", usuario.Apellido);
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Contra", usuario.Pass);
                datos.SetearParametro("@Direccion", usuario.Direccion);
                datos.SetearParametro("@Telefono", usuario.Telefono);
                datos.SetearParametro("@Localidad", usuario.Localidad);
                datos.SetearParametro("@FechaNacimiento", usuario.FechaNacimiento);
                datos.SetearParametro("@Estado", 1);

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

        public void Desactivar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Estado = 0 WHERE Id_Usuario = @Id_Usuario");
                datos.SetearParametro("@Id_Usuario", idUsuario);
                datos.EjecutarLectura();

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

        public void Reactivar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Estado = 1 WHERE Id_Usuario = @Id_Usuario");
                datos.SetearParametro("@Id_Usuario", idUsuario);
                datos.EjecutarLectura();

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

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario aux = new Usuario();
            try
            {
                datos.SetearConsulta("SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, Estado, AccesoId FROM Usuarios WHERE Id_Usuario = @Id_Usuario");
                datos.SetearParametro("@Id_Usuario", idUsuario);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    aux.Id_Usuario = (int)datos.Lector["Id_Usuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Pass = (string)datos.Lector["Contra"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Localidad = (string)datos.Lector["Localidad"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.IdAcceso = (int)datos.Lector["AccesoId"];    
                }

                return aux;

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

        public bool VerificarEmail(string mail )
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE Email = @Email AND Estado = 1");
                datos.SetearParametro("@Email", mail);
                datos.ejecutarEscalar();

                while(datos.Lector.Read())
                {
                    int aux = (int)datos.Lector[0];
                    return aux > 0;
                }
                return false;

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

        public string RecuperarContra(string mail)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Contra FROM Usuarios WHERE Email = @Email AND Estado = 1");
                datos.SetearParametro("@Email", mail);
                datos.ejecutarEscalar();

                while (datos.Lector.Read())
                {
                    return datos.Lector["Contra"].ToString();
                }
                return null;

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


