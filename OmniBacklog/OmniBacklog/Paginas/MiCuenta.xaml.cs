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

namespace OmniBacklog.Paginas
{
    /// <summary>
    /// Lógica de interacción para MiCuenta.xaml
    /// </summary>
    public partial class MiCuenta : Page
    {
        UnitOfWork bd = new UnitOfWork();

        List<Usuario> usuarios = new List<Usuario>();

        public MiCuenta()
        {
            InitializeComponent();
            LBLNombre.Content = MainWindow.login.Nombre;
            TBNumLibros.Text = MainWindow.login.BibliotecasPersonales.Count.ToString();
        }

        private void BTCambiarNombre_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            for (int i = 0; i < MainWindow.usuarios.Count; i++)
            {
                if (MainWindow.usuarios[i].Nombre.Equals(TBNombre.Text, StringComparison.OrdinalIgnoreCase))
                {
                    repe = true;
                    i = MainWindow.usuarios.Count;
                }
            }
            if (repe == false)
            {
                if (TBNombre.Text.Length < 6)
                {
                    MessageBox.Show("Usuario min 6 caracteres", "Usuario no válido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TBNombre.Text.Length > 30)
                {
                    MessageBox.Show("Usuario Max 30 caracteres", "Usuario no válido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MainWindow.login.Nombre = TBNombre.Text;
                    bd.UsuarioRepository.Update(MainWindow.login);
                    bd.Save();
                    MessageBox.Show("El nombre de usuario ha sido cambiado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LBLNombre.Content = TBNombre.Text;
                }
            }
            else
            {
                MessageBox.Show("El nombre de usuario ya está siendo utilizado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTCambiarPassword_Click(object sender, RoutedEventArgs e)
        {
            string OGPassword = MainWindow.login.Contraseña;
            if (PBNContraseña.Password.Equals(PBRContraseña.Password))
            {
                MainWindow.login.Contraseña = Seguridad.Encriptar(PBNContraseña.Password);
                string errores = Validacion.errores(MainWindow.login);
                if (errores.Equals(""))
                {
                    bd.UsuarioRepository.Update(MainWindow.login);
                    bd.Save();
                    MessageBox.Show("La contraseña ha sido cambiada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    PBNContraseña.Password = "";
                    PBRContraseña.Password = "";
                }
                else
                {
                    MessageBox.Show(errores, "Usuario no válido", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow.login.Contraseña = OGPassword;
                }

            }
            else
            {
                MessageBox.Show("Los campos de contraseña deben coincidir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTBorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Borrarás tu cuenta, tus bibliotecas y todos tus datos.\nNo se podrán recuperar. \n ¿Seguro que quieres continuar?", "Borrar cuenta", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                bd.UsuarioRepository.Delete(MainWindow.login);
                bd.Save();
                MainWindow main = new MainWindow();
                main.Show();
                MainWindow.prin.Close();
            }
        }
    }
}
