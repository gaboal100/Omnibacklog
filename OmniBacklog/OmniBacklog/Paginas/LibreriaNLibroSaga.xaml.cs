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
using System.Windows.Shapes;
using OmniBacklog.DAL;
using OmniBacklog.MODEL;
using System.Linq;
using System.Linq.Expressions;

namespace OmniBacklog.Paginas
{
    /// <summary>
    /// Lógica de interacción para LibreriaNLibroSaga.xaml
    /// </summary>
    public partial class LibreriaNLibroSaga : Window
    {
        UnitOfWork bd;
        Saga sagaNueva;
        Saga sagaPadreS;
        Saga sagaPadreL;
        Libro libro;

        List<Saga> SagasS = new List<Saga>();
        List<Saga> SagasL = new List<Saga>();

        TreeView FTree;
        ComboBox combo;

        public LibreriaNLibroSaga(TreeView tree, ComboBox c1, UnitOfWork unit)
        {
            bd = unit;
            InitializeComponent();
            SagasS = bd.SagaRepository.Get(a => a.SagaId != 1 && a.SagaId != 2);
            CBSagasS.ItemsSource = SagasS;
            CBSagasS.DisplayMemberPath = "Nombre";
            CBSagasS.SelectedValuePath = "SagaId";

            SagasL = bd.SagaRepository.Get(a => a.SagaId != 1);
            CBSagasL.ItemsSource = SagasL;
            CBSagasL.DisplayMemberPath = "Nombre";
            CBSagasL.SelectedValuePath = "SagaId";

            LVGeneros.ItemsSource = bd.GeneroRepository.Get(a => a.GeneroId != 1);
            LVGeneros.DisplayMemberPath = "Nombre";
            LVGeneros.SelectedValuePath = "GeneroId";

            LVAutores.ItemsSource = bd.AutorRepository.Get(a => a.AutorId != 1);
            LVAutores.DisplayMemberPath = "Nombre";
            LVAutores.SelectedValuePath = "AutorId";

            NUDNumerado.Value = 0;
            NUDNumeradoS.Value = 0;

            FTree = tree;
            combo = c1;
            
        }

        private void AñadirSaga_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            if (CBSagasS.SelectedIndex == -1 && CheckPadre.IsChecked == false)
            {
                MessageBox.Show("Selecciona una saga padre o haz de la nueva una principal", "Sagas", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }else if (!TBSagaN.Text.Equals(""))
            {
                for (int i = 0; i < Libreria.sagasP.Count; i++)
                {
                    if (Libreria.sagasP[i].Nombre.ToLower().Equals(TBSagaN.Text.ToLower()))
                    {
                        repe = true;
                        i = Libreria.sagasP.Count;
                    }
                }
                if (repe == false)
                {
                    if (TBSagaN.Text.Length > 50)
                    {
                        MessageBox.Show("Nombre de la saga máximo 50 caracteres", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (CheckPadre.IsChecked == true)
                        {
                            addSaga(null, null);
                            borrarSagas();

                        }
                        else if (CheckPadre.IsChecked == false && CBSagasS.SelectedIndex != -1)
                        {
                            sagaPadreS = (Saga)CBSagasS.SelectedItem;
                            addSaga(sagaPadreS.SagaId, (int?)NUDNumeradoS.Value);
                            borrarSagas();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No pueden repetirse sagas", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("La saga debe de tener un nombre", "Sagas", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void addSaga(int? SagaId, int? numerado)
        {
            sagaNueva = new Saga();
            sagaNueva.Nombre = TBSagaN.Text;
            sagaNueva.Numerado = numerado;
            sagaNueva.Saga1Id = SagaId;
            bd.SagaRepository.Añadir(sagaNueva);

            SagasL.Add(sagaNueva);
            SagasS.Add(sagaNueva);
            CBSagasL.Items.Refresh();
            CBSagasS.Items.Refresh();

            bd.Save();

            bruteload();
        }

        private void BorrarSaga_Click(object sender, RoutedEventArgs e)
        {
            borrarSagas();
        }

        public void borrarSagas()
        {
            TBSagaN.Text = "";
            NUDNumeradoS.Value = null;
            CBSagasS.SelectedIndex = -1;
        }

        private void AñadirLibro_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            List<Libro> libros = bd.LibroRepository.GetAll();
            for (int i = 0; i < libros.Count; i++)
            {
                if (libros[i].Titulo.ToLower().Equals(TBTitulo.Text.ToLower()))
                {
                    repe = true;
                    i = libros.Count;
                }
            }
            if (CBSagasL.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona una saga para el libro", "Saga padre", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }else if (TBTitulo.Text.Equals(""))
            {
                MessageBox.Show("Un libro debe tener título", "Título", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }else if(TBTitulo.Text.Length > 50)
            {
                MessageBox.Show("Título máximo 50 caracteres", "Título", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (repe == false)
                {
                    libro = new Libro();
                    sagaPadreL = (Saga)CBSagasL.SelectedItem;
                    libro.SagaId = sagaPadreL.SagaId;
                    libro.Titulo = TBTitulo.Text;
                    libro.Numerado = (int?)NUDNumerado.Value;
                    bd.LibroRepository.Añadir(libro);

                    bd.Save();
                    recorrerListViews(libro);
                    bd.Save();
                    borrarLibros();
                    bruteload();
                }
                else
                {
                    MessageBox.Show("No pueden repetirse libros", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        public void recorrerListViews(Libro libro)
        {
            //Recorrer géneros
            List<GeneroLibro> lisGenLib = new List<GeneroLibro>();
            GeneroLibro genLib;
            List<Genero> generos = new List<Genero>();
            if (LVGeneros.SelectedIndex != -1)
            {
                foreach (var item in LVGeneros.SelectedItems)
                {
                    generos.Add((Genero)item);
                }
                
                for (int i = 0; i < generos.Count; i++)
                {
                    genLib = new GeneroLibro();
                    genLib.GeneroId = generos[i].GeneroId;
                    genLib.LibroId = libro.LibroId;
                    bd.GeneroLibroRepository.Añadir(genLib);
                    lisGenLib.Add(genLib);
                }
                libro.GeneroLibros = lisGenLib;
            }
            else
            {
                genLib = new GeneroLibro();
                genLib.GeneroId = 1;
                genLib.LibroId = libro.LibroId;
                bd.GeneroLibroRepository.Añadir(genLib);
            }

            



            //Recorrer autores
            List<Autor> autores = new List<Autor>();
            List<AutorLibro> lisAuLib = new List<AutorLibro>();
            AutorLibro auLib;

            if (LVAutores.SelectedIndex != -1)
            {
                foreach (var item in LVAutores.SelectedItems)
                {
                    autores.Add((Autor)item);
                }
                for (int i = 0; i < autores.Count; i++)
                {
                    auLib = new AutorLibro();
                    auLib.AutorId = autores[i].AutorId;
                    auLib.LibroId = libro.LibroId;
                    bd.AutorLibroRepository.Añadir(auLib);
                    lisAuLib.Add(auLib);
                }

                bd.LibroRepository.Update(libro);
            }
            else
            {
                auLib = new AutorLibro();
                auLib.AutorId = 1;
                auLib.LibroId = libro.LibroId;
                bd.AutorLibroRepository.Añadir(auLib);
            }
            
        }



        private void BorrarLibro_Click(object sender, RoutedEventArgs e)
        {
            borrarLibros();
        }

        private void borrarLibros()
        {
            TBTitulo.Text = "";
            CBSagasL.SelectedIndex = -1;
            LVGeneros.SelectedItem = null;
            LVAutores.SelectedItem = null;
            NUDNumerado.Value = null;
        }
        


        private void CheckPadre_Checked(object sender, RoutedEventArgs e)
        {
            CBSagasS.IsEnabled = false;
            sagaPadreS = null;
            CBSagasS.SelectedIndex = -1;
            NUDNumeradoS.IsEnabled = false;
            NUDNumeradoS.Value = null;
        }
        
        private void CheckPadre_Unchecked(object sender, RoutedEventArgs e)
        {
            CBSagasS.IsEnabled = true;
            NUDNumeradoS.IsEnabled = true;
        }

        public void MainWindow_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void bruteload()
        {
            //Libreria.sagasP.Clear();
            //Libreria.sagasPT.Clear();
            //Libreria.libros.Clear();

            Libreria.sagasP = bd.SagaRepository.GetAll();
            Libreria.sagasPT = bd.SagaRepository.GetTree();
            Libreria.libros = bd.LibroRepository.GetAll(); //Técnicamente no lo uso en ningún momento pero si lo quito no me aparecen varios libros
            Libreria.sagasP.OrderBy(a => a.Numerado).ThenBy(a => a.SagaId);
            Libreria.libros.OrderBy(a => a.Numerado).ThenBy(a => a.Titulo);

            FTree.ItemsSource = Libreria.sagasPT;
            
            FTree.Items.Refresh();
            combo.Items.Refresh();
            bd.Save();
        }
    }
}
