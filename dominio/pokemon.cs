using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dominio
{
    public class pokemon
    {
        public int id { get; set; }
        
        [DisplayName("Número")]
        public int numero { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        
        public string urlImagen { get; set; }
        [DisplayName("Tipo")]
        public elementos tipo { get; set; }
        [DisplayName("Debilidad")]
        public elementos debilidad {get; set; }
        public bool Activo { get; set; }
    }
}
