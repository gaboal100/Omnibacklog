using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBacklog.DAL
{
    public class UnitOfWork //: IDisposable
    {
        private BacklogContext context = new BacklogContext();
        private bool disposed = false;


        private AutorLibroRepository autorLibrosRepository;
        private AutorRepository autoresRepository;
        private BibliotecaPersonalRepository biblioRepository;
        private GeneroLibroRepository genLibRepository;
        private GeneroRepository generoRepository;
        private LibroRepository libroRepository;
        private SagaRepository sagaRepository;
        private UsuarioRepository usuarioRepository;

        public AutorLibroRepository AutorLibroRepository
        {
            get
            {
                if (autorLibrosRepository == null)
                {
                    autorLibrosRepository = new AutorLibroRepository(context);
                }

                return autorLibrosRepository;
            }
        }

        public AutorRepository AutorRepository
        {
            get
            {
                if (autoresRepository == null)
                {
                    autoresRepository = new AutorRepository(context);
                }

                return autoresRepository;
            }
        }

        public BibliotecaPersonalRepository BibliotecaPersonalRepository
        {
            get
            {
                if (biblioRepository == null)
                {
                    biblioRepository = new BibliotecaPersonalRepository(context);
                }

                return biblioRepository;
            }
        }

        public GeneroLibroRepository GeneroLibroRepository
        {
            get
            {
                if (genLibRepository == null)
                {
                    genLibRepository = new GeneroLibroRepository(context);
                }

                return genLibRepository;
            }
        }

        public GeneroRepository GeneroRepository
        {
            get
            {
                if (generoRepository == null)
                {
                    generoRepository = new GeneroRepository(context);
                }

                return generoRepository;
            }
        }

        public LibroRepository LibroRepository
        {
            get
            {
                if (libroRepository == null)
                {
                    libroRepository = new LibroRepository(context);
                }

                return libroRepository;
            }
        }

        public SagaRepository SagaRepository
        {
            get
            {
                if (sagaRepository == null)
                {
                    sagaRepository = new SagaRepository(context);
                }

                return sagaRepository;
            }
        }

        public UsuarioRepository UsuarioRepository
        {
            get
            {
                if (usuarioRepository == null)
                {
                    usuarioRepository = new UsuarioRepository(context);
                }

                return usuarioRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public UnitOfWork()
        {
            context.Database.EnsureCreated();
        }
    }
}
