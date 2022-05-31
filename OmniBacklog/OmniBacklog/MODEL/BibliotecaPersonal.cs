using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OmniBacklog.MODEL
{
    public class BibliotecaPersonal
    {
        public int LibroId { get; set; }
        
        public int UsuarioId { get; set; }

        
        public bool Leyendo { get; set; }

        public bool Favorito { get; set; }

        public virtual Libro Libro { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
