using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBacklog.MODEL
{
    public class Libro
    {
        public int LibroId { get; set; }

        [Required(ErrorMessage = "Título obligatorio")]
        [StringLength(50, ErrorMessage = "Título máximo 50 caracteres")]
        [MinLength(1, ErrorMessage = "Título mínimo 1 caracter")]
        public string Titulo { get; set; }

        public int? Numerado { get; set; }

        public virtual ICollection<AutorLibro> AutorLibros { get; set; }
        public virtual ICollection<GeneroLibro> GeneroLibros { get; set; }
        public virtual ICollection<BibliotecaPersonal> BibliotecasPersonales { get; set; }

        [ForeignKey("Saga")]
        public int? SagaId { get; set; }
        public virtual Saga Saga { get; set; }

        public Libro()
        {
            AutorLibros = new HashSet<AutorLibro>();
            GeneroLibros = new HashSet<GeneroLibro>();
            BibliotecasPersonales = new HashSet<BibliotecaPersonal>();
        }
    }
}
