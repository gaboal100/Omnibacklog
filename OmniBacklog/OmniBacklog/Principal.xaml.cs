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
using OmniBacklog.Paginas;
using OmniBacklog.DAL;
using OmniBacklog.MODEL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;

namespace OmniBacklog
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// Cada botón lleva a una página distinta, excepto por Ayuda, que abre el manual de Usuario, y el de PDF, que genera un PDF.
    /// </summary>
    public partial class Principal : Window
    {
        UnitOfWork bd = new UnitOfWork();

        public Principal()
        {
            InitializeComponent();
            Defectar.sinAsignar();
        }

        private void BTAutores_Click(object sender, RoutedEventArgs e)
        {
            Frame.Source = new Uri("Paginas/Autores.xaml", UriKind.Relative);
        }

        private void BTBiblio_Click(object sender, RoutedEventArgs e)
        {
            Frame.Source = new Uri("Paginas/MiBiblioteca.xaml", UriKind.Relative);
        }

        private void BTBookBase_Click(object sender, RoutedEventArgs e)
        {
            Frame.Source = new Uri("Paginas/Libreria.xaml", UriKind.Relative);
        }

        private void BTGeneros_Click(object sender, RoutedEventArgs e)
        {
            Frame.Source = new Uri("Paginas/Generos.xaml", UriKind.Relative);
        }

        private void BTMicuenta_Click(object sender, RoutedEventArgs e)
        {
            Frame.Source = new Uri("Paginas/MiCuenta.xaml", UriKind.Relative);
        }

        public void MainWindow_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void BTCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres cerrar sesión?", "Cerrar sesión", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                MainWindow inicio = new MainWindow();
                inicio.Show();
                this.Close();
            }
        }

        private void BTpdf_Click(object sender, RoutedEventArgs e)
        {
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 130, 50);

            SaveFileDialog oFD = new SaveFileDialog();
            oFD.Filter = "PDF document (*.pdf)|*.pdf";

            oFD.Title = "Selecciona el archivo para guardar el PDF";
            oFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            oFD.FileName = "Informe";
            //if (pdf.IsActive == true)
            //{
            //    pdf.Close();
            //}
            if (oFD.ShowDialog() == true)
            {
                //Logica de creación del pdf
                #region
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(oFD.FileName, FileMode.Create));
                MyPageEvents events = new MyPageEvents();
                writer.PageEvent = events;

                PdfPCell cell;
                PdfPTable table;

                document.Open();

                Font titulo = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);
                Font cellHeader = new Font(Font.FontFamily.HELVETICA, 16);

                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Tabla Libros", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.7f, 0.4f, 0.7f });

                cell = new PdfPCell(new Phrase("ID", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Título", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Numerado", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Saga", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<Libro> libros = bd.LibroRepository.getSagaAndLibro();
                libros.OrderBy(a => a.SagaId).ThenBy(a => a.Numerado);

                for (int i = 0; i < libros.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(libros[i].LibroId.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(libros[i].Titulo.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(libros[i].Numerado.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(libros[i].Saga.Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.NewPage();

                title = new iTextSharp.text.Paragraph("Tabla Sagas", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.7f, 0.4f, 0.7f });

                cell = new PdfPCell(new Phrase("ID", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Numerado", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Saga padre", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<Saga> sagas = bd.SagaRepository.GetAll();
                sagas.OrderBy(a => a.SagaId).ThenBy(a => a.Numerado);

                for (int i = 0; i < sagas.Count; i++)
                {
                    Phrase padre = new Phrase();
                    cell = new PdfPCell(new Phrase(sagas[i].SagaId.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(sagas[i].Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(sagas[i].Numerado.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    if (sagas[i].Saga1 != null)
                    {
                        padre = new Phrase(sagas[i].Saga1.Nombre.ToString());
                    }
                    else
                    {
                        padre = new Phrase("NULL");
                    }

                    cell = new PdfPCell(padre);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.NewPage();

                title = new iTextSharp.text.Paragraph("Tabla Autores", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.8f });

                cell = new PdfPCell(new Phrase("ID", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<Autor> autores = bd.AutorRepository.Get(a => a.AutorId != 1);

                for (int i = 0; i < autores.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(autores[i].AutorId.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(autores[i].Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.NewPage();


                title = new iTextSharp.text.Paragraph("Tabla AutorLibro", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.5f, 0.5f });

                cell = new PdfPCell(new Phrase("Autor", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Libro", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<AutorLibro> autorLibros = bd.AutorLibroRepository.getEverything();
                autorLibros.OrderBy(a => a.AutorId);

                for (int i = 0; i < autorLibros.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(autorLibros[i].Autor.Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(autorLibros[i].Libro.Titulo.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.NewPage();


                title = new iTextSharp.text.Paragraph("Tabla Géneros", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.8f });

                cell = new PdfPCell(new Phrase("ID", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<Genero> generos = bd.GeneroRepository.Get(a => a.GeneroId != 1);

                for (int i = 0; i < generos.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(generos[i].GeneroId.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(generos[i].Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);


                document.NewPage();


                title = new iTextSharp.text.Paragraph("Tabla GéneroLibros", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.8f });

                cell = new PdfPCell(new Phrase("Género", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Libro", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<GeneroLibro> generoLibro = bd.GeneroLibroRepository.getEverything();
                generoLibro.OrderBy(a => a.GeneroId);

                for (int i = 0; i < generoLibro.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(generoLibro[i].Genero.Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(generoLibro[i].Libro.Titulo.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);


                document.NewPage();

                title = new iTextSharp.text.Paragraph("Tabla Usuarios", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(3);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.2f, 0.4f, 0.4f });

                cell = new PdfPCell(new Phrase("ID", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Contraseña", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<Usuario> usuarios = bd.UsuarioRepository.GetAll();

                for (int i = 0; i < usuarios.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(usuarios[i].UsuarioId.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(usuarios[i].Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(usuarios[i].Contraseña.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.NewPage();


                title = new iTextSharp.text.Paragraph("Tabla BibliotecaPersonal", titulo);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);

                //TableHeader
                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.33f, 0.33f, 0.33f, 0.33f });

                cell = new PdfPCell(new Phrase("Usuario", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Libro", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Leyendo", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Favorito", cellHeader));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                List<BibliotecaPersonal> bibliotecaPersonals = bd.BibliotecaPersonalRepository.getEverything();
                bibliotecaPersonals.OrderBy(a => a.UsuarioId);

                for (int i = 0; i < bibliotecaPersonals.Count; i++)
                {
                    Phrase estado;

                    cell = new PdfPCell(new Phrase(bibliotecaPersonals[i].Usuario.Nombre.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(bibliotecaPersonals[i].Libro.Titulo.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    if (bibliotecaPersonals[i].Leyendo == true)
                    {
                        estado = new Phrase("Leyendo");
                    }
                    else
                    {
                        estado = new Phrase("No leyendo");
                    }

                    cell = new PdfPCell(new Phrase(estado));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    if (bibliotecaPersonals[i].Favorito == true)
                    {
                        estado = new Phrase("Favorito");
                    }
                    else
                    {
                        estado = new Phrase("No favorito");
                    }

                    cell = new PdfPCell(new Phrase(estado));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                document.Add(table);

                document.Close();
                #endregion

                Uri PDFPath = new Uri(oFD.FileName, UriKind.Absolute);
                PrincipalPDF pdf = new PrincipalPDF(PDFPath);
                if (pdf.IsActive == false)
                {
                    pdf.Show();
                }
                //pdf.Close();

            }
        }

        private void BTAyuda_Click(object sender, RoutedEventArgs e)
        {
            PrincipalPDF pdfAyuda = new PrincipalPDF();
            if (pdfAyuda.IsActive == false)
            {
                pdfAyuda.Show();
            }
            //pdf.Close();
            //if (IsWindowOpen<Window>("pdfAyuda"))
            //{

            //}
            //else
            //{
            //    pdfAyuda.Show();
            //}

        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
