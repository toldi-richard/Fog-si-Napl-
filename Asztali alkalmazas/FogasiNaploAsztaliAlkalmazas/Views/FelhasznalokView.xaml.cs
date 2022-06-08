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
    public partial class FelhasznalokView : Window
    {
        public FelhasznalokView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight-4;
        }
        private FelhasznalokViewModel Vm => this.DataContext as FelhasznalokViewModel;
        
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

        //Kijelentkezés gomb
        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            var mv = new LoginView();
            Application.Current.Windows[0].Close();
            mv.ShowDialog();
        }

        //Ablak mozgatása egérrel való húzással
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        //Egyedi ablak bezárása gomb esemény
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //Egyedi tálcára helyezés esemény
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        //Egyedi teljes képernyőre helyezés gomb-esemény
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
        //Ha már teljes képernyős módban van, akkor helyette az előző méret tooltip jelenik meg
        private void MaximizeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                MaximizeBtn.ToolTip = "Előző méret";
            }
        }

        //Fogások/Felhasználók nézet váltása
        private void ChangeContentButton_Click(object sender, RoutedEventArgs e)
        {
            var mv = new FogasokView();

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
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            //Ne fusson hibára, ha töröltem egy kijelölt felhasználot(Trypharse-nál volt hiba utána)
            if (SelectedFogasokSzama.HasContent)
            {

                int.TryParse(SelectedFogasokSzama.Content.ToString(), out int SelectedFogasokSzamaInt);
                if (SelectedFogasokSzamaInt == 0)
                {
                    DeleteBtn.IsEnabled = true;
                    DeleteBtn.ToolTip = "Törlés";
                    TorlesTiltott.Visibility = Visibility.Hidden;
                }
                else
                {
                    DeleteBtn.IsEnabled = false;
                    TorlesTiltott.Visibility = Visibility.Visible;

                }
            }
            else
            {

            }
           
            
        }
    }
}
