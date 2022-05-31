using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBacklog.MODEL
{
    public class Autor
    {
        public int AutorId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(50, ErrorMessage = "Nombre Max 50 caracteres")]
        [MinLength(1, ErrorMessage = "Nombre min 1 caracter")]
        public string Nombre { get; set; }

        public virtual ICollection<AutorLibro> AutorLibros { get; set; }

        public Autor()
        {
            AutorLibros = new HashSet<AutorLibro>();
        }
    }
}
