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
    /// Lógica de interacción para BorrarCosas.xaml
    /// </summary>
    public partial class BorrarCosas : Window
    {
        UnitOfWork bd;
        TreeView FTree;
        ComboBox combo;
        List<Saga> sagasP = new List<Saga>();

        public BorrarCosas(TreeView tree, ComboBox c1, UnitOfWork unit)
        {
            InitializeComponent();
            bd = unit;
            FTree = tree;
            combo = c1;
            Cargar();
        }

        private void Cargar()
        {
            DGSagas.ItemsSource = bd.SagaRepository.Get(a => !a.Nombre.Equals("Huérfanos") && !a.Nombre.Equals("Únicos")).OrderBy(a => a.Nombre).ThenBy(a => a.Numerado);
            DGLibros.ItemsSource = bd.LibroRepository.GetAll();

            CBSagasS.ItemsSource = bd.SagaRepository.Get(a => !a.Nombre.Equals("Huérfanos") && !a.Nombre.Equals("Únicos"));
            CBSagasL.ItemsSource = bd.SagaRepository.GetAll();

            CBSagasS.DisplayMemberPath = "Nombre";
            CBSagasS.SelectedValuePath = "SagaId";
            CBSagasS.SelectedIndex = 0;

            CBSagasL.DisplayMemberPath = "Nombre";
            CBSagasL.SelectedValuePath = "SagaId";
            CBSagasL.SelectedIndex = 0;

            sagasP = bd.SagaRepository.Get(a => !a.Nombre.Equals("Huérfanos") && !a.Nombre.Equals("Únicos")).OrderBy(a => a.Nombre).ThenBy(a => a.Numerado).ToList();

            DGSagas.Items.Refresh();
            DGLibros.Items.Refresh();
            CBSagasS.Items.Refresh();
            CBSagasL.Items.Refresh();
        }

        private void BorrarSagas_Click(object sender, RoutedEventArgs e)
        {
            List<Saga> sagasDelete = new List<Saga>();
            if(DGSagas.SelectedIndex != -1)
            {
                if (MessageBox.Show("¿Seguro que quieres borrar las sagas seleccionadas?", "Borrar sagas", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    foreach (Saga saga in DGSagas.SelectedItems)
                    {
                        foreach (Libro libroH in saga.Libros)
                        {
                            libroH.SagaId = 1;
                        }

                        if (saga.Saga1Id != null && saga.Sagas.Count > 0)
                        {

                            if (MessageBox.Show("¿Quieres que las sagas hijas se vuelvan principales?", "Sagas hijas", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                            {
                                foreach (Saga sagaH in saga.Sagas)
                                {
                                    Libreria.sagasPT.Add(sagaH);
                                }
                            }
                            else
                            {
                                foreach (Saga sagaH in saga.Sagas)
                                {
                                    sagaH.Saga1Id = saga.Saga1Id;
                                }
                            }
                        }
                        else if (saga.Sagas.Count > 0 && saga.Saga1Id == null)
                        {
                            foreach (Saga sagaH in saga.Sagas)
                            {
                                Libreria.sagasPT.Add(sagaH);
                            }
                        }

                        sagasDelete.Add(saga);
                    }
                    foreach (Saga saga in sagasDelete)
                    {
                        bd.SagaRepository.Delete(saga);
                    }
                    bd.Save();

                    Cargar();
                    bruteload();
                }
            }
        }

        private void CSPadres_Click(object sender, RoutedEventArgs e)
        {
            if (DGSagas.SelectedIndex != -1)
            {
                foreach (Saga saga in DGSagas.SelectedItems)
                {
                    saga.Saga1 = (Saga)CBSagasS.SelectedItem;
                    bd.SagaRepository.Update(saga);
                    bd.Save();
                }
                bruteload();
                Cargar();
            }
        }

        private void BorrarLibros_Click(object sender, RoutedEventArgs e)
        {
            if(DGLibros.SelectedIndex != -1)
            {
                if (MessageBox.Show("¿Seguro que quieres borrar los libros seleccionados?", "Borrar libros", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    foreach (Libro libro in DGLibros.SelectedItems)
                    {
                        bd.LibroRepository.Delete(libro);
                        bd.Save();
                    }
                    bruteload();
                    Cargar();
                }
            }
        }

        private void CLPadres_Click(object sender, RoutedEventArgs e)
        {
            if (DGLibros.SelectedIndex != -1 && CBSagasL.SelectedItem != null)
            {
                foreach (Libro libro in DGLibros.SelectedItems)
                {
                    libro.Saga = (Saga)CBSagasL.SelectedItem;
                    bd.LibroRepository.Update(libro);
                    bd.Save();
                }
                bruteload();
                Cargar();
            }
        }

        private void DGSagas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Saga> sagasBorrar = new List<Saga>();
            List<Saga> familia = new List<Saga>();
            sagasP = bd.SagaRepository.Get(a => !a.Nombre.Equals("Huérfanos") && !a.Nombre.Equals("Únicos")).OrderBy(a => a.Nombre).ThenBy(a => a.Numerado).ToList();
            if (DGSagas.SelectedIndex != -1)
            {
                if(DGSagas.SelectedItems.Count > 1)
                {
                    foreach (Saga saga in DGSagas.SelectedItems)
                    {
                        sagasP.Remove(saga);
                        familia = QuitarFamilia(saga.Sagas.ToList(), sagasBorrar);
                    }
                    for (int i = 0; i < familia.Count; i++)
                    {
                        sagasP.Remove(familia[i]);
                    }
                }
                else if (DGSagas.SelectedItems.Count == 1)
                {
                    Saga saga = (Saga)DGSagas.SelectedItem;
                    sagasP.Remove(saga);
                    familia = QuitarFamilia(saga.Sagas.ToList(), sagasBorrar);
                    for (int i = 0; i < familia.Count; i++)
                    {
                        sagasP.Remove(familia[i]);
                    }
                }
                CBSagasS.SelectedIndex = -1;
            }
            CBSagasS.ItemsSource = sagasP;
            CBSagasS.Items.Refresh();
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

        private void DGLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSagasL.SelectedIndex = -1;
        }

        private void BSPadres_Click(object sender, RoutedEventArgs e)
        {
            List<Saga> sagasDespadradas = new List<Saga>();
            if (DGSagas.SelectedIndex != -1)
            {
                foreach (Saga saga in DGSagas.SelectedItems)
                {
                    sagasDespadradas.Add(saga);
                }
            }
            foreach(Saga saga in sagasDespadradas)
            {
                saga.Saga1Id = null;
                bd.SagaRepository.Update(saga);
                bd.Save();

            }
            bruteload();
            Cargar();
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
