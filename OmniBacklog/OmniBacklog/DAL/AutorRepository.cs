using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class AutorRepository : GenericRepository<Autor>
    {
        public AutorRepository(BacklogContext context) : base(context) { }
    }
}
