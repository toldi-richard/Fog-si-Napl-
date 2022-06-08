using FogasiNaploAsztaliAlkalmazas.ViewModels;
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
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FogasiNaploAsztaliAlkalmazas.Views
{
    /// <summary>
    /// Interaction logic for AdatMegjelenites.xaml
    /// </summary>
    public partial class FogasokView : Window
    {
        public FogasokView()
        {
            InitializeComponent();

            //Teljes képernyőre helyezésnél a tálca is el lett fedve,így oldottam meg hogy látszódjon

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 4;
        }
        private FogasokViewModel Vm => this.DataContext as FogasokViewModel;

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            Vm.SortBy = column.SortMemberPath;
            // Itt pedig lekezeljük a sorbarendezés megtörténését, hogy megjelenjen a kis háromszög
            e.Handled = true;
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ?
                ListSortDirection.Ascending : ListSortDirection.Descending;
            column.SortDirection = direction;
        }


        //Kijelentkezés gomb-esemény
        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            var mv = new LoginView();
            Application.Current.Windows[0].Close();
            mv.ShowDialog();

        }
        //Ablak mozgatása egérrel  való húzással
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        //Ablak bezárása-gomb esemény
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Tálcára helyezés
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Teljes képernyőre váltás/ a szegély szélesítése teljes képernyőn-jobb láthatóság a széleken
        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.BorderThickness = new Thickness(0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.BorderThickness = new Thickness(5);
            }
        }
        //Teljes képernyős módban a tooltip változik
        private void MaximizeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                MaximizeBtn.ToolTip = "Előző méret";
            }
        }
        //Felhasználok nézetre váltás-esemény
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var mv = new FelhasznalokView();

            if (this.WindowState == WindowState.Maximized)
            {
                Application.Current.Windows[0].Close();
                mv.WindowState = WindowState.Maximized;
                mv.ShowDialog();
            }
            else
            {
                Application.Current.Windows[0].Close();
                mv.ShowDialog();
            }

        }
        //Fogás súlya, bevitel szűrés
        private void Sulykorlat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("^([1-9][0-9]?[0-9]?[.]?[5]?|1000)$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}
