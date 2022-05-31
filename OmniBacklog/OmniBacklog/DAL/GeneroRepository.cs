using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class GeneroRepository : GenericRepository<Genero>
    {
        public GeneroRepository(BacklogContext context) : base(context) { }

        public Genero getOneGenero(int id)
        {
            return Get(a => a.GeneroId == id).FirstOrDefault();
        }
    }


}
