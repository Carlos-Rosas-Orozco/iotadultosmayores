using System;
using System.Collections.Generic;

namespace IoT_PI.Models
{
    public partial class AdultoMayor
    {
        public int IdAdulto { get; set; }
        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string DescripcionGeneral { get; set; }
        public bool? Estado { get; set; }

        public virtual Usuarios IdusuarioNavigation { get; set; }
    }
}
