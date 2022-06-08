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
using System.Windows.Shapes;
using FogasiNaploAsztaliAlkalmazas.ViewModels;

namespace FogasiNaploAsztaliAlkalmazas.Views
{
    /// <summary>
    /// Interaction logic for BejelentkezoOldal.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel loginViewModel;
        public LoginView()
        {
            InitializeComponent();
            this.loginViewModel = new LoginViewModel();
            this.DataContext = loginViewModel;
           
        }
        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            loginViewModel.Password = pwdBox.Password;

        }
        //Jövőbeni funkció/elfelejtett jelszó 
        private void LostPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A funkció fejlesztés alatt.\n" +
                "Kérjük vegye fel a kapcsolatot az alkalmazás fejlesztőivel!", 
                "Jelszóemlékeztető", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Jelszó mutatása/rejtése + ikon változtatása
        private void Show(object sender, RoutedEventArgs e)
        {


            if (pwdBox.Visibility == Visibility.Hidden)
            {
                if (pwdBox.Password != PasswordUnmask.Text)
                {
                    pwdBox.Password = PasswordUnmask.Text;
                }
                pwdBox.Visibility = Visibility.Visible;
                PasswordUnmask.Visibility = Visibility.Hidden;
                Uri resourceUri = new Uri("/Images/Eye.png", UriKind.Relative);
                PasswordHide.Source = new BitmapImage(resourceUri);
                PasswordHide.ToolTip = "Jelszó megjelenítése";
            }
            else
            {
                if (PasswordUnmask.Text != pwdBox.Password)
                {
                    PasswordUnmask.Text = pwdBox.Password;
                }
                pwdBox.Visibility = Visibility.Hidden;
                PasswordUnmask.Text = pwdBox.Password;
                PasswordUnmask.Visibility = Visibility.Visible;

                Uri resourceUri = new Uri("/Images/ClosedEye.png", UriKind.Relative);
                PasswordHide.Source = new BitmapImage(resourceUri);
                PasswordHide.ToolTip = "Jelszó elrejtése";
            }

        }
        //A jelszómegjelenítő mező tartalma mindig egyezzen a passwordbox-éval
        private void PasswordUnmask_TextChanged(object sender, TextChangedEventArgs e)
        {
            loginViewModel.Password = PasswordUnmask.Text;
            pwdBox.Password = PasswordUnmask.Text;

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //Belépés gomb ikonja változik ha ki van töltve a két mező
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (pwdBox.Password != "" && UserName.Text != "")
            {
                Uri resourceUri2 = new Uri("/Images/Login.png", UriKind.Relative);
                BelepesBtn.Source = new BitmapImage(resourceUri2);
            }
            else
            {
                Uri resourceUri3 = new Uri("/Images/DoNotEnter.png", UriKind.Relative);
                BelepesBtn.Source = new BitmapImage(resourceUri3);
            }
        }

    }
}
