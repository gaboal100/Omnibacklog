using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class LibroRepository : GenericRepository<Libro>
    {
        public LibroRepository(BacklogContext context) : base(context) { }

        public List<Libro> getSpecificBooks(string titulo)
        {
            return Get(a => a.Titulo.Contains(titulo));
        }

        public Libro getSagaFromLibro(int id)
        {
            return Get(a => a.LibroId == id, includeProperties: "Saga").FirstOrDefault();
        }

        public List<Libro> getSagaAndLibro()
        {
            return Get(includeProperties: "Saga");
        }

        public List<Libro> getGenerosAutores()
        {
            return Get(includeProperties: "AutorLibros,GeneroLibros");
        }
    }
}
