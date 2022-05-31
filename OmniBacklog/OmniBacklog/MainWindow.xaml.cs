using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace OmniBacklog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool coinciden = false;
        Usuario usuario;
        UnitOfWork bd = new UnitOfWork();
        public static Usuario login;
        public static List<Usuario> usuarios = new List<Usuario>();
        public static Principal prin;

        public MainWindow()
        {
            InitializeComponent();
            usuarios = bd.UsuarioRepository.GetAll();
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
            usuario = new Usuario();
            usuario.Nombre = TBUsuario.Text;
            usuario.Contraseña = Seguridad.Encriptar(PBContraseña.Password);
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuario.Nombre == usuarios[i].Nombre && usuario.Contraseña == usuarios[i].Contraseña)
                {
                    coinciden = true;
                    i = usuarios.Count;
                }
            }

            if (coinciden == true)
            {
                login = bd.UsuarioRepository.GetUsuario(usuario.Nombre);
                prin = new Principal();
                prin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña no coinciden", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTRegistrar_Click(object sender, RoutedEventArgs e)
        {
            MainWindowRegistrar registrar = new MainWindowRegistrar();
            registrar.ShowDialog();
        }
    }
}
