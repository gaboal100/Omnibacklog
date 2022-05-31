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
    /// Lógica de interacción para Autores.xaml
    /// </summary>
    public partial class Autores : Page
    {
        List<Libro> listaLibros = new List<Libro>();
        List<Libro> librosCambio = new List<Libro>();
        List<Autor> autores;
        List<Autor> todosAutores;
        List<AutorLibro> autLibs;
        List<AutorLibro> autLibsCambio;
        Autor autorSelect = new Autor();
        Autor newAutor;

        Usuario usuario;

        UnitOfWork bd = new UnitOfWork();

        public Autores()
        {
            InitializeComponent();
            cargar();
            CambiarAutores.IsEnabled = false;
            EditarNombre.IsEnabled = false;
            BorrarAutor.IsEnabled = false;
            LVAutoresCambiar.IsEnabled = false;
            CambiarAutores.IsEnabled = false;
            usuario = MainWindow.login;
        }

        private void cargar()
        {
            todosAutores = bd.AutorRepository.GetAll();
            autores = bd.AutorRepository.Get(a => a.AutorId != 1);
            LVAutoresT.ItemsSource = todosAutores;
            LVAutoresT.DisplayMemberPath = "Nombre";
            LVAutoresT.SelectedValuePath = "AutorId";

            LVAutoresCambiar.ItemsSource = autores;
            LVAutoresCambiar.DisplayMemberPath = "Nombre";
            LVAutoresCambiar.SelectedValuePath = "AutorId";
        }

        private void LVAutoresT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVAutoresT.SelectedIndex != -1)
            {
                LVAutoresCambiar.SelectedIndex = -1;
                MostrarLibros();
                EditarNombre.IsEnabled = true;
                BorrarAutor.IsEnabled = true;
            }
            else
            {
                EditarNombre.IsEnabled = false;
                BorrarAutor.IsEnabled = false;
            }
        }

        private void MostrarLibros()
        {
            autorSelect = (Autor)LVAutoresT.SelectedItem;
            if (autorSelect.AutorId == 1)
            {
                checkDefault.IsChecked = false;
                checkDefault.IsEnabled = false;
            }
            else
            {
                checkDefault.IsEnabled = true;
            }
            TBNombreEdit.Text = autorSelect.Nombre;
            listaLibros.Clear();
            
            autLibs = bd.AutorLibroRepository.getLibros(autorSelect.AutorId);
            for (int i = 0; i < autLibs.Count; i++)
            {
                listaLibros.Add(bd.LibroRepository.getSagaFromLibro(autLibs[i].LibroId));
            }

            listaLibros.OrderBy(a => a.SagaId).ThenBy(a => a.Numerado).ThenBy(a => a.Titulo);

            DGLibros.ItemsSource = listaLibros;
            DGLibros.Items.Refresh();
        }

        private void EditarNombre_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            for (int i = 0; i < todosAutores.Count; i++)
            {
                if (todosAutores[i].Nombre.ToLower().Equals(TBNombreEdit.Text.ToLower()))
                {
                    repe = true;
                    i = todosAutores.Count;
                }
            }
            if (repe == false)
            {
                if (autorSelect.AutorId != 1)
                {
                    autorSelect.Nombre = TBNombreEdit.Text;
                    string errores = Validacion.errores(autorSelect);
                    if (errores.Equals(""))
                    {
                        bd.AutorRepository.Update(autorSelect);
                        LVAutoresT.Items.Refresh();
                        LVAutoresCambiar.Items.Refresh();
                        bd.Save();
                    }
                    else MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("No se puede editar el autor Sin Asignar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No pueden repetirse autores", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BorrarAutor_Click(object sender, RoutedEventArgs e)
        {
            if (autorSelect.AutorId != 1)
            {
                if (MessageBox.Show("¡CUIDADO! Al borrar se guarda automáticamente \n ¿Seguro que quieres borrar?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    autorSelect.AutorLibros.Clear();
                    autores.Remove(autorSelect);
                    todosAutores.Remove(autorSelect);
                    bd.AutorRepository.Delete(autorSelect);
                    bd.Save();
                    Defectar.sinAsignar();
                    LVAutoresT.Items.Refresh();
                    LVAutoresCambiar.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("No se puede borrar el autor Sin Asignar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void AñadirAut_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            for (int i = 0; i < todosAutores.Count; i++)
            {
                if (todosAutores[i].Nombre.ToLower().Equals(TBAutAñadir.Text.ToLower()))
                {
                    repe = true;
                    i = todosAutores.Count;
                }
            }
            if (repe == false)
            {
                newAutor = new Autor();
                newAutor.Nombre = TBAutAñadir.Text;
                string errores = Validacion.errores(newAutor);
                if (errores.Equals(""))
                {
                    bd.AutorRepository.Añadir(newAutor);
                    autores.Add(newAutor);
                    todosAutores.Add(newAutor);
                    bd.Save();
                    LVAutoresT.Items.Refresh();
                    LVAutoresCambiar.Items.Refresh();

                    TBAutAñadir.Text = "";
                }
                else MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No pueden repetirse autores", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CambiarAutores_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Esto sobreescribirá TODOS los autores de los libros seleccionados \n ¿Seguro que quieres proseguir?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                librosCambio.Clear();
                List<Autor> autoresSeleccionados = new List<Autor>();
                AutorLibro autorLibro;

                for (int i = 0; i < DGLibros.SelectedItems.Count; i++)
                {
                    librosCambio.Add((Libro)DGLibros.SelectedItems[i]);
                }

                for (int i = 0; i < librosCambio.Count; i++)
                {
                    bd.AutorLibroRepository.Delete(a => a.LibroId == librosCambio[i].LibroId);
                }


                if (checkDefault.IsChecked == false)
                {                    
                    for (int i = 0; i < LVAutoresCambiar.SelectedItems.Count; i++)
                    {
                        autoresSeleccionados.Add((Autor)LVAutoresCambiar.SelectedItems[i]);
                    }                    

                    for (int i = 0; i < librosCambio.Count; i++)
                    {
                        for (int j = 0; j < autoresSeleccionados.Count; j++)
                        {
                            autorLibro = new AutorLibro();
                            autorLibro.LibroId = librosCambio[i].LibroId;

                            autorLibro.AutorId = autoresSeleccionados[j].AutorId;

                            bd.AutorLibroRepository.Añadir(autorLibro);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < librosCambio.Count; i++)
                    {
                        autorLibro = new AutorLibro();

                        autorLibro.LibroId = librosCambio[i].LibroId;

                        autorLibro.AutorId = 1;

                        bd.AutorLibroRepository.Añadir(autorLibro);
                    }
                }
                bd.Save();

                MostrarLibros();

                LVAutoresT.Items.Refresh();
                LVAutoresCambiar.SelectedItem = null;
                checkDefault.IsEnabled = false;
                CambiarAutores.IsEnabled = false;
                DGLibros.Items.Refresh();
            }
        }

        private void DGLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGLibros.SelectedIndex == -1)
            {
                LVAutoresCambiar.IsEnabled = false;
            }
            else
            {
                LVAutoresCambiar.IsEnabled = true;
            }
        }

        private void checkDefault_Checked(object sender, RoutedEventArgs e)
        {
            LVAutoresCambiar.SelectedItem = null;
            LVAutoresCambiar.IsEnabled = false;
            CambiarAutores.IsEnabled = true;
        }

        private void checkDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DGLibros.SelectedIndex != -1)
            {
                LVAutoresCambiar.IsEnabled = true;
                CambiarAutores.IsEnabled = false;
            }
        }

        private void LVAutoresCambiar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVAutoresCambiar.SelectedIndex != -1)
            {
                CambiarAutores.IsEnabled = true;
            }
            else
            {
                CambiarAutores.IsEnabled = false;
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
