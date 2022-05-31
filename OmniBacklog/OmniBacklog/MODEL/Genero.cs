using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBacklog.MODEL
{
    public class Genero
    {
        public int GeneroId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(30, ErrorMessage = "Nombre Max 30 caracteres")]
        [MinLength(1, ErrorMessage = "Nombre min 1 caracter")]
        public string Nombre { get; set; }

        public virtual ICollection<GeneroLibro> GeneroLibros { get; set; }

        public Genero()
        {
            GeneroLibros = new HashSet<GeneroLibro>();
        }
    }
}
