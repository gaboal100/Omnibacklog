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

namespace OmniBacklog
{
    /// <summary>
    /// Lógica de interacción para PrincipalPDF.xaml
    /// </summary>
    public partial class PrincipalPDF : Window
    {
        public PrincipalPDF()
        {
            InitializeComponent();
            string path = System.IO.Directory.GetCurrentDirectory() + @"\Manual de usuario.pdf";
            Uri PDFPath = new Uri(path, UriKind.Absolute);
            WebBro.Navigate(PDFPath);
        }

        public PrincipalPDF(Uri PDFPath)
        {
            InitializeComponent();
            WebBro.Navigate(PDFPath);
        }
    }
}
