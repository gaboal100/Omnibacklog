﻿using System;
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
    /// Lógica de interacción para Libreria.xaml
    /// </summary>
    public partial class Libreria : Page
    {
        UnitOfWork bd = new UnitOfWork();
        Saga saga = new Saga();
        Libro libro = new Libro();

        Usuario usuario;

        public static List<Libro> libros = new List<Libro>();
        public static List<Saga> sagasP = new List<Saga>();
        public static List<Saga> sagasPT = new List<Saga>();
        public static List<string> nombresSaga = new List<string>();
        public static List<string> nombresLibros = new List<string>();
        public static List<GeneroLibro> genLib;
        public static List<AutorLibro> autLib;

        LibreriaNLibroSaga nuevo;

        public Libreria()
        {
            InitializeComponent();
            cargar();
            usuario = MainWindow.login;
        }

        private void cargar()
        {
            sagasP.Clear();
            sagasPT.Clear();
            libros.Clear();
            nombresSaga.Clear();

            sagasP = bd.SagaRepository.GetAll();
            sagasPT = bd.SagaRepository.GetTree();
            libros = bd.LibroRepository.GetAll();//Técnicamente no lo uso en ningún momento pero si lo quito no me aparecen varios libros
            sagasP.OrderBy(a => a.Numerado).ThenBy(a => a.SagaId);
            libros.OrderBy(a => a.Numerado).ThenBy(a => a.Titulo);

            TVSagaP.ItemsSource = sagasPT/*.OrderBy(a => a.Numerado)*/;

            foreach (Saga saga in sagasP)
            {
                nombresSaga.Add(saga.Nombre);
                foreach(Libro libro in saga.Libros)
                {
                    nombresLibros.Add(libro.Titulo);
                }
            }
            //CBSagas.ItemsSource = sagasP;
            //CBSagas.DisplayMemberPath = "Nombre";
            //CBSagas.SelectedValuePath = "SagaId";
        }

        private void TVSagaP_SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TVSagaP.SelectedItem != null)
            {
                sagasP = bd.SagaRepository.GetAll();

                if (TVSagaP.SelectedItem.GetType() == libro.GetType())
                {

                    libro = new Libro();
                    libro = (Libro)TVSagaP.SelectedItem;
                    //gridLibro.DataContext = libro;
                    TBNombre.IsEnabled = true;
                    TBNombre.Text = libro.Titulo;
                    TBNum.Value = libro.Numerado;

                    CBSagas.Text = libro.Saga.Nombre;

                    LVAutores.IsEnabled = true;
                    LVAutoresT.IsEnabled = true;

                    LVGeneros.IsEnabled = true;
                    LVGenerosT.IsEnabled = true;

                    AñadirAu.IsEnabled = true;
                    AñadirGen.IsEnabled = true;
                    QuitarAu.IsEnabled = true;
                    QuitarGen.IsEnabled = true;

                    genLib = bd.GeneroLibroRepository.getGeneros(libro.LibroId);
                    LVGeneros.ItemsSource = genLib;
                    LVGeneros.DisplayMemberPath = "Genero.Nombre";
                    LVGeneros.SelectedValuePath = "GeneroId";

                    LVGenerosT.ItemsSource = bd.GeneroRepository.Get(a => a.GeneroId != 1);
                    LVGenerosT.DisplayMemberPath = "Nombre";
                    LVGenerosT.SelectedValuePath = "GeneroId";

                    autLib = bd.AutorLibroRepository.getAutores(libro.LibroId);
                    LVAutores.ItemsSource = autLib;
                    LVAutores.DisplayMemberPath = "Autor.Nombre";
                    LVAutores.SelectedValuePath = "AutorId";

                    LVAutoresT.ItemsSource = bd.AutorRepository.Get(a => a.AutorId != 1);
                    LVAutoresT.DisplayMemberPath = "Nombre";
                    LVAutoresT.SelectedValuePath = "AutorId";
                }
                else if (TVSagaP.SelectedItem.GetType() == saga.GetType())
                {
                    saga = new Saga();
                    saga = (Saga)TVSagaP.SelectedItem;
                    if(saga.Saga1 != null)
                    {
                        CBSagas.Text = saga.Saga1.Nombre;
                    }
                    else
                    {
                        CBSagas.Text = "";
                    }
                    
                    TBNum.Value = saga.Numerado;
                    TBNombre.Text = saga.Nombre;
                    LVAutores.IsEnabled = false;
                    LVAutoresT.IsEnabled = false;
                    LVGeneros.IsEnabled = false;
                    LVGenerosT.IsEnabled = false;

                    sagasP.Remove(saga);
                    

                    AñadirAu.IsEnabled = false;
                    AñadirGen.IsEnabled = false;
                    QuitarAu.IsEnabled = false;
                    QuitarGen.IsEnabled = false;
                }
            }            
        }

        private List<Saga> QuitarFamilia(List<Saga> sagasHijas, List<Saga> sagasBorrar) //Pide una lista de las sagas hijas de un padre y una lista para borrar (vacía) que será rellenada con los hijos y nietos de la saga escogida
        {
            if (sagasHijas != null)
            {
                for (int i = 0; i < sagasHijas.Count; i++)
                {
                    sagasBorrar.Add(sagasHijas[i]);
                    QuitarFamilia(sagasHijas[i].Sagas.ToList(), sagasBorrar);
                }
                
            }
            return sagasBorrar;
        }

        private void BTModificar_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;

            if (TVSagaP.SelectedItem != null)
            {
                if (TVSagaP.SelectedItem.GetType() == libro.GetType())
                {
                    for (int i = 0; i < libros.Count; i++)
                    {
                        if (libros[i].Titulo.ToLower().Equals(TBNombre.Text.ToLower()))
                        {
                            repe = true;
                            i = libros.Count;
                        }
                    }
                    if (repe == false)
                    {
                        if (TBNombre.Text.Length < 1)
                        {
                            MessageBox.Show("Título mínimo 1 caracter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (TBNombre.Text.Length > 50)
                        {
                            MessageBox.Show("Título máximo 50 caracteres", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            int? sagaId = libro.SagaId;
                            libro.Titulo = TBNombre.Text;
                            libro.Numerado = (int?)TBNum.Value;

                            string errores = Validacion.errores(libro);
                            if (errores.Equals(""))
                            {
                                bd.LibroRepository.Update(libro);
                                bd.Save();
                                TVSagaP.Items.Refresh();
                                Defectar.sinAsignar(); //Por si las moscas
                            }
                            else MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No pueden repetirse libros", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else //Debido a que el binding está hecho para libros tengo que hacer los cambios y validaciones manualmente
                {
                    if (saga.SagaId == 1 || saga.SagaId == 2)
                    {
                        MessageBox.Show("No se pueden editar ni Huérfanos ni Únicos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        for (int i = 0; i < sagasP.Count; i++)
                        {
                            if (sagasP[i].Nombre.ToLower().Equals(TBNombre.Text.ToLower()))
                            {
                                repe = true;
                                i = sagasP.Count;
                            }
                        }
                        if (repe == false)
                        {
                            if (TBNombre.Text.Length < 1)
                            {
                                MessageBox.Show("Nombre de la saga mínimo 1 caracter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }else if (TBNombre.Text.Length > 50)
                            {
                                MessageBox.Show("Nombre de la saga máximo 50 caracteres", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                saga.Nombre = TBNombre.Text;
                                saga.Numerado = (int?)TBNum.Value;
                                bd.SagaRepository.Update(saga);
                                bd.Save();
                                cargar();
                                TVSagaP.Items.Refresh();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No pueden repetirse sagas", "Repetido", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    
                }
                
            }
        }

        /*
        Borrar el botón de borrar seleccionado, quitar la opción de cambiar padre, y simplemente dejar esas dos opciones en la otra ventana
        Guardar Cambios guardará sólo los cambios individuales del objeto que no afecten a su pasado, presente o futuro padre
        */
        private void BTNew_Click(object sender, RoutedEventArgs e)
        {
            //bd = new UnitOfWork();
            nuevo = new LibreriaNLibroSaga(TVSagaP, nombresSaga, nombresLibros, Suggestion/*, bd*/); //Voy a borrar la UnitOfWork cada vez que lo cierro a ver qué pasa, si no funca, pues a recomenzar esto
            nuevo.ShowDialog();
        }

        private void BTElim_Click(object sender, RoutedEventArgs e) //No funciona si antes añado un elemento nuevo a menos que refresque del todo la página
        {
            //bd = new UnitOfWork();
            BorrarCosas borrar = new BorrarCosas(TVSagaP, bd);
            borrar.ShowDialog();
            //if (MessageBox.Show("¡CUIDADO! Al borrar se guarda automáticamente \n ¿Seguro que quieres borrar?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            //{
            //    if (TVSagaP.SelectedItem.GetType() == saga.GetType())
            //    {
            //        if (saga.SagaId > 2)
            //        {
            //            foreach (Libro libroH in saga.Libros)
            //            {
            //                libroH.SagaId = 1;
            //            }

            //            if (saga.Saga1Id != null && saga.Sagas.Count > 0)
            //            {
            //                if (MessageBox.Show("¿Quieres que las sagas hijas se vuelvan principales?", "Sagas hijas", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            //                {
            //                    foreach (Saga sagaH in saga.Sagas)
            //                    {
            //                        sagasPT.Add(sagaH);
            //                    }
            //                }
            //                else
            //                {
            //                    foreach (Saga sagaH in saga.Sagas)
            //                    {
            //                        sagaH.Saga1Id = saga.Saga1Id;
            //                    }
            //                }
            //            }
            //            else if (saga.Sagas.Count > 0 && saga.Saga1Id == null)
            //            {
            //                foreach (Saga sagaH in saga.Sagas)
            //                {
            //                    sagasPT.Add(sagaH);
            //                }
            //            }

            //            sagasPT.Remove(saga);
            //            sagasP.Remove(saga);

            //            bd.SagaRepository.Delete(saga);

            //            bd.Save();

            //            TVSagaP.Items.Refresh();
            //            CBSagas.Items.Refresh();
            //        }
            //        else
            //        {
            //            MessageBox.Show("No se puede borrar ni Huérfanos ni Únicos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //        CBSagas.Items.Refresh();

            //    }
            //    else if (TVSagaP.SelectedItem.GetType() == libro.GetType())
            //    {
            //        bd.LibroRepository.Delete(libro);
            //        bd.Save();
            //        TVSagaP.Items.Refresh();
            //    }
            //}
        }

        public void MainWindow_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        

        private void AñadirGen_Click(object sender, RoutedEventArgs e)
        {
            if (LVGenerosT.SelectedItems.Count != 0)
            {
                bool permitido = true;

                List<Genero> generos = new List<Genero>();

                foreach (var item in LVGenerosT.SelectedItems)
                {
                    generos.Add((Genero)item);
                }

                //List<GeneroLibro> lisGenLib = new List<GeneroLibro>();
                GeneroLibro nGenLib;

                if (genLib[0].GeneroId == 1)
                {
                    bd.GeneroLibroRepository.Delete(genLib[0]);
                    genLib.Remove(genLib[0]);                
                }

                for (int i = 0; i < generos.Count; i++)
                {
                    for (int j = 0; j < genLib.Count; j++)
                    {
                        if (genLib[j].GeneroId == generos[i].GeneroId)
                        {
                            permitido = false;
                            j = genLib.Count;
                        }
                        else
                        {
                            permitido = true;
                        }
                    }
                    if (permitido == true)
                    {
                        nGenLib = new GeneroLibro();
                        nGenLib.GeneroId = generos[i].GeneroId;
                        nGenLib.LibroId = libro.LibroId;
                        bd.GeneroLibroRepository.Añadir(nGenLib);

                        genLib.Add(nGenLib);

                        libro.GeneroLibros.Add(nGenLib);
                        generos[i].GeneroLibros.Add(nGenLib);
                        bd.GeneroRepository.Update(generos[i]);
                    }
                }


                bd.LibroRepository.Update(libro);

                LVGeneros.Items.Refresh();
                LVGenerosT.Items.Refresh();

                bd.Save();
            }
        }

        private void QuitarGen_Click(object sender, RoutedEventArgs e)
        {
            if (LVGeneros.SelectedItems.Count != 0)
            {
                GeneroLibro glBorrar;

                Genero genero;

                List<GeneroLibro> listaGenLibs = new List<GeneroLibro>();

                foreach (var item in LVGeneros.SelectedItems)
                {
                    listaGenLibs.Add((GeneroLibro)item);
                }

                for (int i = 0; i < listaGenLibs.Count; i++)
                {
                    glBorrar = listaGenLibs[i];
                    genero = listaGenLibs[i].Genero;
                    libro.GeneroLibros.Remove(listaGenLibs[i]);
                    genero.GeneroLibros.Remove(listaGenLibs[i]);

                    bd.LibroRepository.Update(libro);
                    bd.GeneroRepository.Update(genero);

                    bd.GeneroLibroRepository.Delete(glBorrar);

                    genLib.Remove(glBorrar);
                }

                if (genLib.Count == 0)
                {
                    GeneroLibro defecto = new GeneroLibro();
                    defecto.GeneroId = 1;
                    defecto.LibroId = libro.LibroId;

                    bd.LibroRepository.Update(libro);
                    bd.GeneroRepository.Update(bd.GeneroRepository.Get(a => a.GeneroId == 1).FirstOrDefault());
                    genLib.Add(defecto);
                    bd.GeneroLibroRepository.Añadir(defecto);
                }

                LVGeneros.Items.Refresh();
                LVGenerosT.Items.Refresh();
                bd.Save();
            }
        }

        private void AñadirAu_Click(object sender, RoutedEventArgs e)
        {
            if (LVAutoresT.SelectedItems.Count != 0)
            {
                bool permitido = true;

                List<Autor> autores = new List<Autor>();

                foreach (var item in LVAutoresT.SelectedItems)
                {
                    autores.Add((Autor)item);
                }

                //List<GeneroLibro> lisGenLib = new List<GeneroLibro>();
                AutorLibro nAutLib;

                if (autLib[0].AutorId == 1)
                {
                    bd.AutorLibroRepository.Delete(autLib[0]);
                    autLib.Remove(autLib[0]);
                }

                for (int i = 0; i < autores.Count; i++)
                {
                    for (int j = 0; j < autLib.Count; j++)
                    {
                        if (autLib[j].AutorId == autores[i].AutorId)
                        {
                            permitido = false;
                            j = autLib.Count;
                        }
                        else
                        {
                            permitido = true;
                        }
                    }
                    if (permitido == true)
                    {
                        nAutLib = new AutorLibro();
                        nAutLib.AutorId = autores[i].AutorId;
                        nAutLib.LibroId = libro.LibroId;
                        bd.AutorLibroRepository.Añadir(nAutLib);

                        autLib.Add(nAutLib);

                        libro.AutorLibros.Add(nAutLib);
                        autores[i].AutorLibros.Add(nAutLib);
                        bd.AutorRepository.Update(autores[i]);
                    }
                }

                bd.LibroRepository.Update(libro);

                LVAutores.Items.Refresh();
                LVAutoresT.Items.Refresh();

                bd.Save();

            }
           
        }

        private void QuitarAu_Click(object sender, RoutedEventArgs e)
        {
            if (LVAutores.SelectedItems.Count != 0)
            {
                AutorLibro alBorrar;

                Autor autor;

                List<AutorLibro> listaAutLibs = new List<AutorLibro>();

                foreach (var item in LVAutores.SelectedItems)
                {
                    listaAutLibs.Add((AutorLibro)item);
                }

                for (int i = 0; i < listaAutLibs.Count; i++)
                {
                    alBorrar = listaAutLibs[i];
                    autor = listaAutLibs[i].Autor;
                    libro.AutorLibros.Remove(listaAutLibs[i]);
                    autor.AutorLibros.Remove(listaAutLibs[i]);

                    bd.LibroRepository.Update(libro);
                    bd.AutorRepository.Update(autor);

                    bd.AutorLibroRepository.Delete(alBorrar);

                    autLib.Remove(alBorrar);

                }

                if (autLib.Count == 0)
                {
                    AutorLibro defecto = new AutorLibro();
                    defecto.AutorId = 1;
                    defecto.LibroId = libro.LibroId;

                    bd.LibroRepository.Update(libro);
                    bd.AutorRepository.Update(bd.AutorRepository.Get(a => a.AutorId == 1).FirstOrDefault());
                    autLib.Add(defecto);
                    bd.AutorLibroRepository.Añadir(defecto);
                }

                LVAutores.Items.Refresh();
                LVAutoresT.Items.Refresh();
                bd.Save();
            }
        }

        private void BTBuscar_Click(object sender, RoutedEventArgs e)
        {
            List<Libro> librosBuscar = new List<Libro>();
            HashSet<Saga> SagasBuscadas = new HashSet<Saga>();
            if (TBNombreBuscar.Text.Equals(""))
            {
                TVSagaP.ItemsSource = sagasPT;
            }
            else
            {
                if (RBLibro.IsChecked == true)
                {
                    librosBuscar = bd.LibroRepository.Get(a => a.Titulo.ToLower().Contains(TBNombreBuscar.Text.ToLower()));
                    if (librosBuscar != null)
                    {
                        for (int i = 0; i < librosBuscar.Count; i++)
                        {
                            SagasBuscadas.Add(bd.SagaRepository.Get(a => a.SagaId == (int)librosBuscar[i].SagaId).FirstOrDefault());
                        }
                        TVSagaP.ItemsSource = SagasBuscadas;
                    }
                }
                else
                {
                    SagasBuscadas = bd.SagaRepository.Get(a => a.Nombre.ToLower().Contains(TBNombreBuscar.Text.ToLower())).ToHashSet();
                    TVSagaP.ItemsSource = SagasBuscadas;
                }
            }
        }

        private void FamiliaSaga(List<Saga> sagasHijas, List<Libro> familia)
        {
            if (sagasHijas != null)
            {
                for (int i = 0; i < sagasHijas.Count; i++)
                {
                    //familia.Add(bd.SagaRepository.getLibrosFromSagas(sagasHijas[i].SagaId));
                    //FamiliaSaga(sagasHijas[i].Sagas.ToList(), familia);
                    Saga sagaConLibros = bd.SagaRepository.getLibrosFromSagas(sagasHijas[i].SagaId);
                    familia.AddRange(sagaConLibros.Libros);
                    FamiliaSaga(sagasHijas[i].Sagas.ToList(), familia);
                }
            }
        }

        private void CBSagas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        //private void BTBorrarSel_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MessageBox.Show("¡CUIDADO! Al borrar se guarda automáticamente \n ¿Seguro que quieres borrar?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
        //    {
        //        if (TVSagaP.SelectedItem.GetType() == saga.GetType())
        //        {
        //            if (saga.SagaId > 2)
        //            {
        //                foreach (Libro libroH in saga.Libros)
        //                {
        //                    libroH.SagaId = 1;
        //                }

        //                if (saga.Saga1Id != null && saga.Sagas.Count > 0)
        //                {
        //                    if (MessageBox.Show("¿Quieres que las sagas hijas se vuelvan principales?", "Sagas hijas", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
        //                    {
        //                        foreach (Saga sagaH in saga.Sagas)
        //                        {
        //                            sagasPT.Add(sagaH);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        foreach (Saga sagaH in saga.Sagas)
        //                        {
        //                            sagaH.Saga1Id = saga.Saga1Id;
        //                        }
        //                    }
        //                }
        //                else if (saga.Sagas.Count > 0 && saga.Saga1Id == null)
        //                {
        //                    foreach (Saga sagaH in saga.Sagas)
        //                    {
        //                        sagasPT.Add(sagaH);
        //                    }
        //                }

        //                sagasPT.Remove(saga);
        //                sagasP.Remove(saga);

        //                bd.SagaRepository.Delete(saga);

        //                bd.Save();

        //                TVSagaP.Items.Refresh();
        //            }
        //            else
        //            {
        //                MessageBox.Show("No se puede borrar ni Huérfanos ni Únicos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }

        //        }
        //        else if (TVSagaP.SelectedItem.GetType() == libro.GetType())
        //        {
        //            bd.LibroRepository.Delete(libro);
        //            bd.Save();
        //            TVSagaP.Items.Refresh();
        //        }
        //    }       
        //}

        private void Suggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Suggestion.ItemsSource != null)
            {
                Suggestion.Visibility = Visibility.Collapsed;
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
            

            if(RBSaga.IsChecked == true)
            {
                autoList.Clear();
                foreach (string item in nombresSaga)
                {
                    if (!string.IsNullOrEmpty(TBNombreBuscar.Text))
                    {
                        if (item.ToLower().Contains(typedString.ToLower()))
                        {
                            autoList.Add(item);
                        }
                    }
                }
            }
            else if(RBLibro.IsChecked == true)
            {
                autoList.Clear();
                foreach (string item in nombresLibros)
                {
                    if (!string.IsNullOrEmpty(TBNombreBuscar.Text))
                    {
                        if (item.ToLower().Contains(typedString.ToLower()))
                        {
                            autoList.Add(item);
                        }
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

        private void UnselectTreeViewItem()
        {
            if (TVSagaP.SelectedItem == null)
                return;

            if (TVSagaP.SelectedItem is TreeViewItem)
            {
                (TVSagaP.SelectedItem as TreeViewItem).IsSelected = false;
            }
            else
            {
                TreeViewItem item = TVSagaP.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
                if (item != null)
                {
                    item.IsSelected = true;
                    item.IsSelected = false;
                }
            }
        }
    }
}
