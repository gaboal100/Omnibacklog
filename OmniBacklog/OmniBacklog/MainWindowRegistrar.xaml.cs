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


namespace OmniBacklog
{
    /// <summary>
    /// Lógica de interacción para MainWindowRegistrar.xaml
    /// </summary>
    public partial class MainWindowRegistrar : Window
    {
        Usuario usuario;
        UnitOfWork bd = new UnitOfWork();

        public MainWindowRegistrar()
        {
            InitializeComponent();
        }

        private void BTRegistrar_Click(object sender, RoutedEventArgs e)
        {
            bool repe = false;
            usuario = new Usuario();

            if (!PBContraseña.Password.Equals(PBContraseñaRepe.Password))
            {
                MessageBox.Show("Las contraseñas no son iguales", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }else if (TBUsuario.Text.Equals(PBContraseña.Password))
            {
                MessageBox.Show("El nombre de usuario y la contraseña no deben ser iguales", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                usuario.Nombre = TBUsuario.Text;
                usuario.Contraseña = Seguridad.Encriptar(PBContraseña.Password);
                string errores = Validacion.errores(usuario);
                if (errores.Equals(""))
                {
                    for (int i = 0; i < MainWindow.usuarios.Count; i++)
                    {
                        if (MainWindow.usuarios[i].Nombre.Equals(usuario.Nombre, StringComparison.OrdinalIgnoreCase))
                        {
                            repe = true;
                        }
                    }
                    if (repe == false)
                    {
                        bd.UsuarioRepository.Añadir(usuario);
                        bd.Save();
                        MessageBox.Show("Usuario creado correctamente", "Enhorabuena!", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow.usuarios.Add(usuario);
                        TBUsuario.Text = "";
                        PBContraseña.Password = "";
                        PBContraseñaRepe.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("El nombre de usuario ya está siendo utilizado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errores, "Usuario no válido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
