using System;
using System.Collections.Generic;

/*
Modelo para la conexion de la DB funcional 
 */

namespace IoT_PI.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            AdultoMayor = new HashSet<AdultoMayor>();
        }

        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<AdultoMayor> AdultoMayor { get; set; }
    }
}
