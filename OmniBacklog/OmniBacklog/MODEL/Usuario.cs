using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OmniBacklog.MODEL
{
    public class Usuario
    {

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(30, ErrorMessage = "Nombre Max 30 caracteres")]
        [MinLength(6, ErrorMessage = "Nombre min 6 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria")]
        [StringLength(50, ErrorMessage = "Contraseña Max 50 caracteres")]
        [MinLength(6, ErrorMessage = "Contraseña min 6 caracteres")]
        public string Contraseña { get; set; }

        public virtual ICollection<BibliotecaPersonal> BibliotecasPersonales { get; set; }

        public Usuario()
        {
            BibliotecasPersonales = new HashSet<BibliotecaPersonal>();
        }
    }
}
