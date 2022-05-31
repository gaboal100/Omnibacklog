using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        public UsuarioRepository(BacklogContext context) : base(context) { }

        public Usuario GetUsuario(string nombre)
        {
            return Get(a => a.Nombre == nombre, includeProperties: "BibliotecasPersonales").FirstOrDefault();
        }
    }
}
