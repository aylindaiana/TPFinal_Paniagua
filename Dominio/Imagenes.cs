﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Imagenes
    {
        public int Id_Imagen {  get; set; }
        public int ArticuloId { get; set; }
        public string UrlImagen { get; set; }
        public bool Estado { get; set; }

    }
}
