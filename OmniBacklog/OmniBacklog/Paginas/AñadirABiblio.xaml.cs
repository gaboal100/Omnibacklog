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
    /// Lógica de interacción para AñadirABiblio.xaml
    /// </summary>
    public partial class AñadirABiblio : Window
    {
        UnitOfWork bd;
        Usuario usuario;
        DataGrid grid;
        List<string> lNombres;
        List<string> allBooks;

        public AñadirABiblio(DataGrid elGrid, UnitOfWork unit, List<string> nombres)
        {
            InitializeComponent();
            grid = elGrid;
            bd = unit;
            lNombres = nombres;
            DGLibros.ItemsSource = bd.LibroRepository.GetAll();
            allBooks = bd.LibroRepository.getTitulos();
            usuario = MainWindow.login;
        }

        private void BTAñadirBiblio_Click(object sender, RoutedEventArgs e)
        {
            if (DGLibros.SelectedIndex != -1)
            {
                List<BibliotecaPersonal> biblioPersonal = bd.BibliotecaPersonalRepository.getBibliotecaPersonal(usuario.UsuarioId);
                foreach (Libro libro in DGLibros.SelectedItems)
                {
                    bool repe = false;
                    for (int j = 0; j < biblioPersonal.Count; j++)
                    {
                        if (libro.LibroId == biblioPersonal[j].LibroId)
                        {
                            repe = true;
                            j = biblioPersonal.Count;
                        }
                    }
                    if (repe == false)
                    {
                        BibliotecaPersonal adicion = new BibliotecaPersonal();
                        adicion.LibroId = libro.LibroId;
                        adicion.UsuarioId = usuario.UsuarioId;
                        lNombres.Add(adicion.Libro.Titulo);
                        bd.BibliotecaPersonalRepository.Añadir(adicion);
                        bd.Save();
                    }
                    grid.ItemsSource = bd.BibliotecaPersonalRepository.getBiblioteca(usuario.UsuarioId);
                }


                //List<Libro> libros = (List<Libro>)DGLibros.SelectedItems;
                //List<BibliotecaPersonal> biblioPersonal = bd.BibliotecaPersonalRepository.getBibliotecaPersonal(usuario.UsuarioId);
                //for (int i = 0; i < libros.Count; i++)
                //{
                //    bool repe = false;
                //    for (int j = 0; j < biblioPersonal.Count; j++)
                //    {
                //        if (libros[i].LibroId == biblioPersonal[j].LibroId)
                //        {
                //            repe = true;
                //            j = biblioPersonal.Count;
                //        }
                //    }
                //    if (repe == false)
                //    {
                //        BibliotecaPersonal adicion = new BibliotecaPersonal();
                //        adicion.LibroId = libros[i].LibroId;
                //        adicion.UsuarioId = usuario.UsuarioId;
                //        bd.BibliotecaPersonalRepository.Añadir(adicion);
                //        bd.Save();
                //    }
                //}
            }
        }

        private void DGBiblio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (TBNombreBuscar.Equals(""))
            {
                DGLibros.ItemsSource = bd.LibroRepository.GetAll();
            }
            else
            {
                DGLibros.ItemsSource = bd.LibroRepository.getSpecificBooks(TBNombreBuscar.Text);
            }
        }

        private void Suggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Suggestion.ItemsSource != null)
            {
                Suggestion.Visibility = Visibility.Collapsed;
                //?
                if (Suggestion.SelectedIndex != -1)
                {
                    TBNombreBuscar.Text = Suggestion.SelectedItem.ToString();
                }
                Suggestion.SelectedIndex = -1;
            }
        }

        private void TBNombreBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = TBNombreBuscar.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in allBooks)
            {
                if (!string.IsNullOrEmpty(TBNombreBuscar.Text))
                {
                    if (item.ToLower().Contains(typedString.ToLower()))
                    {
                        autoList.Add(item);
                    }
                }
            }

            if (autoList.Count > 0)
            {
                Suggestion.ItemsSource = autoList;
                Suggestion.Visibility = Visibility.Visible;
                Suggestion.IsDropDownOpen = true;
            }
            else if (TBNombreBuscar.Text.Equals(""))
            {
                Suggestion.Visibility = Visibility.Collapsed;
                Suggestion.ItemsSource = null;
            }
            else
            {
                Suggestion.Visibility = Visibility.Collapsed;
                Suggestion.ItemsSource = null;
            }
        }
    }
}
