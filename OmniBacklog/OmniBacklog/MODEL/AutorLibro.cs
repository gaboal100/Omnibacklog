using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OmniBacklog.MODEL
{
    public class AutorLibro
    {
        public int AutorId { get; set; }

        public int LibroId { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Libro Libro { get; set; }
    }
}
