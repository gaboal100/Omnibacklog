using OmniBacklog.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class SagaRepository : GenericRepository<Saga>
    {
        public SagaRepository(BacklogContext context) : base(context) { }

        public Saga getLibrosFromSagas(int id)
        {
            return Get(a => a.SagaId == id, includeProperties: "Libros").FirstOrDefault();
        }

        public List<Saga> GetTree()
        {
            return Get(a => a.Saga1Id == null, includeProperties: "Sagas,Libros,Saga1");
        }

    }
}
