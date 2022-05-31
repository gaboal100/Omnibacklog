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
    /// <summary>
    /// Lógica de interacción para Generos.xaml
    /// </summary>
    public partial class Generos : Page
    {
        List<Libro> listaLibros = new List<Libro>();
        List<Libro> librosCambio = new List<Libro>();
        List<Genero> generos;
        List<Genero> generosT;
        List<GeneroLibro> genLibs;
        List<GeneroLibro> genLibsCambio;
        Genero generoSelect = new Genero();
        Genero newGenero;
        Usuario usuario;

        UnitOfWork bd = new UnitOfWork();

        public Generos()
        {
            InitializeComponent();
            cargar();
            CambiarGeneros.IsEnabled = false;
            EditarNombre.IsEnabled = false;
            BorrarGenero.IsEnabled = false;
            LVGenerosCambiar.IsEnabled = false;
            CambiarGeneros.IsEnabled = false;
            usuario = MainWindow.login;
        }

        private void cargar()
        {
            generosT = bd.GeneroRepository.GetAll();
            generos = bd.GeneroRepository.Get(a => a.GeneroId != 1);
            LVGenerosT.ItemsSource = generosT;
            LVGenerosT.DisplayMemberPath = "Nombre";
            LVGenerosT.SelectedValuePath = "GeneroId";

            LVGenerosCambiar.ItemsSource = generos;
            LVGenerosCambiar.DisplayMemberPath = "Nombre";
            LVGenerosCambiar.SelectedValuePath = "GeneroId";
        }

        private void LVGenerosT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVGenerosT.SelectedIndex != -1)
            {
                LVGenerosCambiar.SelectedIndex = -1;
                MostrarLibros();
                EditarNombre.IsEnabled = true;
                BorrarGenero.IsEnabled = true;
            }
            else
            {
                EditarNombre.IsEnabled = false;
                BorrarGenero.IsEnabled = false;
            }

        }

        private void MostrarLibros()
        {
            //GridGeneros.DataContext = LVGenerosT.SelectedItem;
            generoSelect = (Genero)LVGenerosT.SelectedItem;
            if (generoSelect.GeneroId == 1)
            {
                checkDefault.IsChecked = false;
                checkDefault.IsEnabled = false;
            }
            else
            {
                checkDefault.IsEnabled = true;
            }
            TBNombreEdit.Text = generoSelect.Nombre;
            listaLibros.Clear();
            //DGLibros.ItemsSource = listaLibros;
            genLibs = bd.GeneroLibroRepository.getLibros(generoSelect.GeneroId);
            for (int i = 0; i < genLibs.Count; i++)
            {
                listaLibros.Add(bd.LibroRepository.getSagaFromLibro(genLibs[i].LibroId));
            }

            listaLibros.OrderBy(a => a.SagaId).ThenBy(a => a.Titulo).ThenBy(a => a.Numerado);

            DGLibros.ItemsSource = listaLibros;
            DGLibros.Items.Refresh();
        }

        private void EditarNombre_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            for (int i = 0; i < generosT.Count; i++)
            {
                if (generosT[i].Nombre.ToLower().Equals(TBNombreEdit.Text.ToLower()))
                {
                    repe = true;
                    i = generosT.Count;
                }
            }

            if (repe == false)
            {
                if (generoSelect.GeneroId != 1)
                {
                    generoSelect.Nombre = TBNombreEdit.Text;
                    string errores = Validacion.errores(generoSelect);
                    if (errores.Equals(""))
                    {
                        bd.GeneroRepository.Update(generoSelect);
                        LVGenerosT.Items.Refresh();
                        LVGenerosCambiar.Items.Refresh();
                        bd.Save();
                    }
                    else MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("No se puede editar el genero Sin Asignar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No pueden repetirse géneros", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BorrarGenero_Click(object sender, RoutedEventArgs e)
        {
            if (generoSelect.GeneroId != 1)
            {
                if (MessageBox.Show("¡CUIDADO! Al borrar se guarda automáticamente \n ¿Seguro que quieres borrar?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    generoSelect.GeneroLibros.Clear();
                    generos.Remove(generoSelect);
                    generosT.Remove(generoSelect);
                    bd.GeneroRepository.Delete(generoSelect);
                    bd.Save();
                    Defectar.sinAsignar();
                    LVGenerosT.Items.Refresh();
                    LVGenerosCambiar.Items.Refresh();
                }
            }
            else MessageBox.Show("No se puede borrar el genero Sin Asignar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AñadirGen_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            for (int i = 0; i < generosT.Count; i++)
            {
                if (generosT[i].Nombre.ToLower().Equals(TBGenAñadir.Text.ToLower()))
                {
                    repe = true;
                    i = generosT.Count;
                }
            }
            if (repe == false)
            {
                newGenero = new Genero();
                newGenero.Nombre = TBGenAñadir.Text;
                string errores = Validacion.errores(newGenero);
                if (errores.Equals(""))
                {
                    bd.GeneroRepository.Añadir(newGenero);
                    generos.Add(newGenero);
                    generosT.Add(newGenero);
                    bd.Save();
                    LVGenerosT.Items.Refresh();
                    LVGenerosCambiar.Items.Refresh();

                    TBGenAñadir.Text = "";
                }
                else MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No pueden repetirse géneros", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CambiarGeneros_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Esto sobreescribirá TODOS los géneros de los libros seleccionados \n ¿Seguro que quieres proseguir?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                //librosCambio = (List<Libro>)DGLibros.SelectedItems;
                librosCambio.Clear();
                List<Genero> generosSeleccionados = new List<Genero>();
                GeneroLibro generoLibro;

                for (int i = 0; i < DGLibros.SelectedItems.Count; i++)
                {
                    librosCambio.Add((Libro)DGLibros.SelectedItems[i]);
                }

                for (int i = 0; i < librosCambio.Count; i++)
                {
                    bd.GeneroLibroRepository.Delete(a => a.LibroId == librosCambio[i].LibroId);
                }

                if (checkDefault.IsChecked == false)
                {
                    for (int i = 0; i < LVGenerosCambiar.SelectedItems.Count; i++)
                    {
                        generosSeleccionados.Add((Genero)LVGenerosCambiar.SelectedItems[i]);
                    }

                    for (int i = 0; i < librosCambio.Count; i++)
                    {
                        for (int j = 0; j < generosSeleccionados.Count; j++)
                        {
                            generoLibro = new GeneroLibro();
                            generoLibro.LibroId = librosCambio[i].LibroId;

                            generoLibro.GeneroId = generosSeleccionados[j].GeneroId;

                            bd.GeneroLibroRepository.Añadir(generoLibro);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < librosCambio.Count; i++)
                    {
                        generoLibro = new GeneroLibro();

                        generoLibro.LibroId = librosCambio[i].LibroId;

                        generoLibro.GeneroId = 1;

                        bd.GeneroLibroRepository.Añadir(generoLibro);
                    }
                }
                bd.Save();

                MostrarLibros();

                LVGenerosT.Items.Refresh();
                LVGenerosCambiar.SelectedItem = null;
                checkDefault.IsEnabled = false;
                CambiarGeneros.IsEnabled = false;
                DGLibros.Items.Refresh();
            }
        }

        private void DGLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGLibros.SelectedIndex == -1)
            {
                LVGenerosCambiar.IsEnabled = false;
            }
            else
            {
                LVGenerosCambiar.IsEnabled = true;
            }
        }

        private void checkDefault_Checked(object sender, RoutedEventArgs e)
        {
            LVGenerosCambiar.SelectedItem = null;
            LVGenerosCambiar.IsEnabled = false;
            CambiarGeneros.IsEnabled = true;
        }

        private void checkDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DGLibros.SelectedIndex != -1)
            {
                LVGenerosCambiar.IsEnabled = true;
                CambiarGeneros.IsEnabled = false;
            }
        }

        private void LVGenerosCambiar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVGenerosCambiar.SelectedIndex != -1)
            {
                CambiarGeneros.IsEnabled = true;
            }
            else
            {
                CambiarGeneros.IsEnabled = false;
            }
        }

        //private void BTBiblio_Click(object sender, RoutedEventArgs e)
        //{
        //    librosCambio.Clear();

        //    for (int i = 0; i < DGLibros.SelectedItems.Count; i++)
        //    {
        //        librosCambio.Add((Libro)DGLibros.SelectedItems[i]);
        //    }

        //    List<BibliotecaPersonal> bibliotecaPersonal = bd.BibliotecaPersonalRepository.getBibliotecaPersonal(usuario.UsuarioId);
        //    BibliotecaPersonal añadido;

        //    for (int i = 0; i < librosCambio.Count; i++)
        //    {
        //        bool repetido = false;
        //        añadido = new BibliotecaPersonal();
        //        añadido.LibroId = librosCambio[i].LibroId;
        //        añadido.UsuarioId = usuario.UsuarioId;
        //        for (int j = 0; j < bibliotecaPersonal.Count; j++)
        //        {
        //            if (bibliotecaPersonal[j].LibroId != añadido.LibroId)
        //            {
        //                repetido = false;
        //            }
        //            else
        //            {
        //                repetido = true;
        //                j = bibliotecaPersonal.Count;
        //            }
        //        }
        //        if (repetido == false)
        //        {
        //            usuario.BibliotecasPersonales.Add(añadido);
        //            bd.BibliotecaPersonalRepository.Añadir(añadido);
        //            bd.Save();
        //        }
        //    }
        //}
    }
}
