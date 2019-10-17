using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTAdultosMayores.Models
{
    /*
    La clase Anciano se basa en la informacion principal que se puede recabar de una persona
    Podemos cambiar el nombre a Adulto Mayor pero se me hace mas facil asi.
     */
    public class Anciano
    {
        public int An_Id { get; set; }
        public string An_primerNombre { get; set; }
        public string An_segundoNombre { get; set; }
        public string An_primerApellido { get; set; }
        public string An_segundoApellido { get; set; }
        public string An_relacion { get; set; } //Relacion entre el usuario(responsable del adulto mayor) y el adulto mayor
        public DateTime An_fechaNacimiento { get; set; }
        public float An_valMinNormal { get; set; } //valor minimo dentro del rango aceptable
        public float An_valMaxNormal { get; set; } //valor maximo dentro del rango aceptable
        public float An_valMinIrregular { get; set; } //valor minimo a partir del cual cuenta como un valor irregular
        public float An_valMaxIrregular { get; set; } //valor maximo a partir del cual cuenta como un valor irregular
    }
}
