using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Marca
    {
        public int Id_Marca {  get; set; }
        public string Nombre { get; set; }
        public bool Estado  { get; set; }
        // tener en cuenta que puede usarse con los mismos pasos que se uso para talles

        // teniendo un propio stock, agregandolo al crear, mod y baja del articulo, y en pagos.
    }
}
