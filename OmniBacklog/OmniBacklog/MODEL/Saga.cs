using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBacklog.MODEL
{
    public class Saga
    {
        public int SagaId { get; set; } //Identificador de las sagas

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(50, ErrorMessage = "Nombre de la saga máximo 50 caracteres")]
        [MinLength(1, ErrorMessage = "Nombre de la saga mínimo 1 caracter")]
        public string Nombre { get; set; } //Nombre de las sagas


        public int? Numerado { get; set; } //Si hay sagas dentro de sagas, hace falta ordenarlas

        public virtual ICollection<Saga> Sagas { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }

        [ForeignKey("Saga1")]
        public int? Saga1Id { get; set; }

        public virtual Saga Saga1 { get; set; }

        public Saga()
        {
            Sagas = new HashSet<Saga>();
            Libros = new HashSet<Libro>();
        }
    }
}
