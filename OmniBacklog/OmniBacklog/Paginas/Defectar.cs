using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OmniBacklog.DAL;
using OmniBacklog.MODEL;
using System.Linq;
using System.Linq.Expressions;

namespace OmniBacklog.Paginas
{
    public class Defectar
    {
        
        public static void sinAsignar()
        {
            UnitOfWork bd = new UnitOfWork();
            List<Libro> libros = bd.LibroRepository.getGenerosAutores();
            GeneroLibro genLib;
            AutorLibro autLib;


            for (int i = 0; i < libros.Count; i++)
            {
                if (libros[i].SagaId == null)
                {
                    libros[i].SagaId = 2;
                }

                if (libros[i].GeneroLibros.Count == 0)
                {
                    genLib = new GeneroLibro();
                    genLib.GeneroId = 1;
                    genLib.LibroId = libros[i].LibroId;
                    bd.GeneroLibroRepository.Añadir(genLib);
                }
                if (libros[i].AutorLibros.Count == 0)
                {
                    autLib = new AutorLibro();
                    autLib.AutorId = 1;
                    autLib.LibroId = libros[i].LibroId;
                    bd.AutorLibroRepository.Añadir(autLib);
                }
            }

            bd.Save();
        }
    }
}
