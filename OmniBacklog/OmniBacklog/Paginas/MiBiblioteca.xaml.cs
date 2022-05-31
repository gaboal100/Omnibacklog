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
    /// Lógica de interacción para MiBiblioteca.xaml
    /// </summary>
    public partial class MiBiblioteca : Page
    {
        UnitOfWork bd = new UnitOfWork();
        Usuario usuario;

        public MiBiblioteca()
        {
            InitializeComponent();
            usuario = MainWindow.login;
            cargar();
        }

        private void cargar()
        {
            DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId); 
            DGLeyendo.ItemsSource = bd.BibliotecaPersonalRepository.getLeyendo(usuario.UsuarioId).ToList();
        }

        private void DGLeyendo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DGBiblio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!TBNombreBuscar.Text.Equals(""))
            {
                if (RBLibro.IsChecked == true)
                {
                    DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId).Where(a => a.Libro.Titulo.ToLower().Contains(TBNombreBuscar.Text.ToLower()));
                }
                else if (RBSaga.IsChecked == true)
                {
                    DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId).Where(a => a.Libro.Saga.Nombre.ToLower().Contains(TBNombreBuscar.Text.ToLower()));
                }
            }
            else
            {
                DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId);
            }
        }

        private void BtNoLeyendo_Click(object sender, RoutedEventArgs e)
        {
            if (DGLeyendo.SelectedIndex != -1)
            {
                foreach (BibliotecaPersonal libro in DGLeyendo.SelectedItems)
                {
                    libro.Leyendo = false;
                    bd.BibliotecaPersonalRepository.Update(libro);
                }
                bd.Save();
                cargar();
            }
        }
        
        private void BTLeer_Click(object sender, RoutedEventArgs e)
        {
            if (DGBiblio.SelectedIndex != -1)
            {
                foreach (BibliotecaPersonal libro in DGBiblio.SelectedItems)
                {
                    libro.Leyendo = true;
                    bd.BibliotecaPersonalRepository.Update(libro);
                }
                bd.Save();
                cargar();
            }
        }

        private void BTFav_Click(object sender, RoutedEventArgs e)
        {
            if (DGBiblio.SelectedIndex != -1)
            {
                foreach (BibliotecaPersonal libro in DGBiblio.SelectedItems)
                {
                    if (libro.Favorito == true)
                    {
                        libro.Favorito = false;
                    }
                    else
                    {
                        libro.Favorito = true;
                    }
                    bd.BibliotecaPersonalRepository.Update(libro);
                }
                bd.Save();
                cargar();
                checkFav.IsChecked = false;
            }
        }

        private void BTFueraBiblio_Click(object sender, RoutedEventArgs e)
        {
            bool leyendo = false;
            if (DGBiblio.SelectedIndex != -1)
            {
                foreach (BibliotecaPersonal libro in DGBiblio.SelectedItems)
                {
                    if (libro.Leyendo == true)
                    {
                        leyendo = true;
                    }
                }
                if (leyendo == true)
                {
                    if (MessageBox.Show("Alguno de esos libros está siendo leídos \n ¿Seguro que quieres sacarlos de tu biblioteca?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        foreach (BibliotecaPersonal libro in DGBiblio.SelectedItems)
                        {
                            bd.BibliotecaPersonalRepository.Delete(libro);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("¿Seguro que quieres sacar los libros seleccionados de tu biblioteca?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        foreach (BibliotecaPersonal libro in DGBiblio.SelectedItems)
                        {
                            bd.BibliotecaPersonalRepository.Delete(libro);
                        }
                    }
                }
                
                bd.Save();
                cargar();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getFavoritos(usuario.UsuarioId);
            DGBiblio.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DGBiblio.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId);
            DGBiblio.Items.Refresh();
        }

        private void BTAñadirABiblio_Click(object sender, RoutedEventArgs e)
        {
            AñadirABiblio añadir = new AñadirABiblio(DGBiblio);
            añadir.ShowDialog();
        }
    }
}
