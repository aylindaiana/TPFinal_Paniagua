using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Manager
{
    public class ImagenesManager
    {
        public void Guardar(Imagenes imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Imagenes (ArticuloId, ImagenURL) VALUES (@ArticuloId, @ImagenURL)");
                datos.SetearParametro("@ArticuloId", imagen.ArticuloId);
                datos.SetearParametro("@ImagenURL", imagen.UrlImagen);
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
        public List<Imagenes> ListarPorArticulo(int articuloId)
        {
            List<Imagenes> lista = new List<Imagenes>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id_Imagen, ArticuloId, ImagenURL FROM Imagenes WHERE ArticuloId = @ArticuloId");
                datos.SetearParametro("@ArticuloId", articuloId);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagenes imagen = new Imagenes();
                    imagen.Id_Imagen = (int)datos.Lector["Id_Imagen"];
                    imagen.ArticuloId = (int)datos.Lector["ArticuloId"];
                    imagen.UrlImagen = datos.Lector["ImagenURL"].ToString();

                    lista.Add(imagen);
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
        public void Eliminar(string urlImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM Imagenes WHERE ImagenURL = @ImagenURL");
                datos.SetearParametro("@ImagenURL", urlImagen);
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
