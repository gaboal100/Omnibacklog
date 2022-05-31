using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class GeneroLibroRepository : GenericRepository<GeneroLibro>
    {
        public GeneroLibroRepository(BacklogContext context) : base(context) { }

        public List<GeneroLibro> getGeneros(int id)
        {
            return Get(a => a.LibroId == id, includeProperties: "Genero");
        }

        public List<GeneroLibro> getLibros(int id)
        {
            return Get(a => a.GeneroId == id, includeProperties: "Libro");
        }

        public List<GeneroLibro> getEverything()
        {
            return Get(includeProperties: "Libro,Genero");
        }
    }
}
