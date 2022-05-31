using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class AutorLibroRepository : GenericRepository<AutorLibro>
    {
        public AutorLibroRepository(BacklogContext context) : base(context) { }

        public List<AutorLibro> getAutores(int id)
        {
            return Get(a => a.LibroId == id, includeProperties: "Autor");
        }

        public List<AutorLibro> getLibros(int id)
        {
            return Get(a => a.AutorId == id, includeProperties: "Libro");
        }

        public List<AutorLibro> getEverything()
        {
            return Get(includeProperties: "Libro,Autor");
        }
    }
}
