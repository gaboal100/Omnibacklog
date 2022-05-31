using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OmniBacklog.MODEL
{
    public class GeneroLibro
    {
        public int GeneroId { get; set; }

        public int LibroId { get; set; }

        public virtual Libro Libro { get; set; }

        public virtual Genero Genero { get; set; }
    }
}
