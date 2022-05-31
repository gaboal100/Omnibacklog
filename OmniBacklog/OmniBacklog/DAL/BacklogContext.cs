using System;
using System.Collections.Generic;
using System.Text;
using OmniBacklog.MODEL;
using Microsoft.EntityFrameworkCore;

namespace OmniBacklog.DAL
{
    public class BacklogContext : DbContext
    {
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<AutorLibro> AutorLibros { get; set; }
        public virtual DbSet<BibliotecaPersonal> BibliotecasPersonales { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<GeneroLibro> GenerosLibros { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Saga> Sagas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=OmniBacklog ;Integrated Security=True"); //User Id = sa; Password = sqlserver
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutorLibro>().HasKey(c => new { c.AutorId, c.LibroId});
            modelBuilder.Entity<BibliotecaPersonal>().HasKey(c => new { c.UsuarioId, c.LibroId});
            modelBuilder.Entity<GeneroLibro>().HasKey(c => new { c.LibroId, c.GeneroId});

            modelBuilder.Entity<Usuario>().HasIndex(u => u.Nombre).IsUnique();
            modelBuilder.Entity<Saga>().HasIndex(u => u.Nombre).IsUnique();
            modelBuilder.Entity<Libro>().HasIndex(u => u.Titulo).IsUnique();
            //modelBuilder.Entity<Genero>().HasIndex(u => u.Nombre).IsUnique();
            //modelBuilder.Entity<Autor>().HasIndex(u => u.Nombre).IsUnique();

            modelBuilder.Entity<Saga>().HasMany(e => e.Libros).WithOne(e => e.Saga).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Genero>().HasData(new Genero { GeneroId = 1, Nombre = "Sin asignar" }); //Obligatoria y ha de ser imborrable
            //modelBuilder.Entity<Genero>().HasData(new Genero { GeneroId = 2, Nombre = "Fantasía medieval" });
            //modelBuilder.Entity<Genero>().HasData(new Genero { GeneroId = 3, Nombre = "Ciencia ficción" });
            //modelBuilder.Entity<Genero>().HasData(new Genero { GeneroId = 4, Nombre = "Ópera espacial" });
            //modelBuilder.Entity<Genero>().HasData(new Genero { GeneroId = 5, Nombre = "Ficción histórica" });

            modelBuilder.Entity<Autor>().HasData(new Autor { AutorId = 1, Nombre = "Sin asignar" }); //Obligatoria y ha de ser imborrable
            //modelBuilder.Entity<Autor>().HasData(new Autor { AutorId = 2, Nombre = "J.K. Rowling" });
            //modelBuilder.Entity<Autor>().HasData(new Autor { AutorId = 3, Nombre = "George R.R. Martin" });
            //modelBuilder.Entity<Autor>().HasData(new Autor { AutorId = 4, Nombre = "Brandon Sanderson" });

            modelBuilder.Entity<Saga>().HasData(new Saga { SagaId = 1, Nombre = "Huérfanos", Numerado = 0}); //Obligatoria y ha de ser imborrable
            modelBuilder.Entity<Saga>().HasData(new Saga { SagaId = 2, Nombre = "Únicos", Numerado = 0}); //Obligatoria y ha de ser imborrable

            //modelBuilder.Entity<Saga>().HasData(new Saga { SagaId = 3, Nombre = "Harry Potter", Numerado = 0});
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 1, Titulo = "Harry Potter y la Piedra Filosofal", Numerado = 1, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 2, Titulo = "Harry Potter y la Cámara de los secretos", Numerado = 2, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 3, Titulo = "Harry Potter y el Prisionero de Azkaban", Numerado = 3, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 4, Titulo = "Harry Potter y el Cáliz de Fuego", Numerado = 4, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 5, Titulo = "Harry Potter y la Orden del Fénix", Numerado = 5, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 6, Titulo = "Harry Potter y el Príncipe Mestizo", Numerado = 6, SagaId = 3 });
                //modelBuilder.Entity<Libro>().HasData(new Libro { LibroId = 7, Titulo = "Harry Potter y las Reliquias de la Muerte", Numerado = 7, SagaId = 3 });
            
            modelBuilder.Entity<Usuario>().HasData(new Usuario { UsuarioId = 1, Nombre = "Usuario", Contraseña = Seguridad.Encriptar("abc123.")});

            //modelBuilder.Entity<Libro>().HasMany(e => e.BibliotecasPersonales).WithOne(e => e.Libro).OnDelete(DeleteBehavior.SetNull);


        }
    }
}
