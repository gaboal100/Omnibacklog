using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class BibliotecaPersonalRepository : GenericRepository<BibliotecaPersonal>
    {
        public BibliotecaPersonalRepository(BacklogContext context) : base(context) { }
        public List<BibliotecaPersonal> getBibliotecaPersonal(int id)
        {
            return Get(a => a.UsuarioId == id);
        }

        public List<BibliotecaPersonal> getBiblioteca(int id)
        {
            return Get(a => a.UsuarioId == id, includeProperties: "Libro,Libro.Saga");
        }

        public List<BibliotecaPersonal> getLeyendo(int id)
        {
            return Get(a => a.UsuarioId == id && a.Leyendo == true, includeProperties: "Libro,Libro.Saga");
        }

        public List<BibliotecaPersonal> getFavoritos(int id)
        {
            return Get(a => a.UsuarioId == id && a.Favorito == true, includeProperties: "Libro,Libro.Saga");
        }

        public List<BibliotecaPersonal> getEverything()
        {
            return Get(includeProperties: "Libro,Usuario");
        }
    }

    
}
