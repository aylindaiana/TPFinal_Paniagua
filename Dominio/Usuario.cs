using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int Id_Usuario {  get; set; }
        public string Nombre { get; set; }
        public string Apellido  { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Direccion {  get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
