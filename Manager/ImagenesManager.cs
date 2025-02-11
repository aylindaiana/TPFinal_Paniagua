using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ImagenesManager
    {
        public void Guardar(Imagenes imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Imagenes (ArticuloId, Url) VALUES (@ArticuloId, @ImagenURL)");
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

    }
}
