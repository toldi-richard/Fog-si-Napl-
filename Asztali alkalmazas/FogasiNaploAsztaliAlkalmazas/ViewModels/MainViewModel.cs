using FogasiNaploAsztaliAlkalmazas.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableObject _selectedViewModel;
        public ObservableObject SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }
        public RelayCommand<object> UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new RelayCommand<object>(e => Execute(e));
            // SelectedViewModel = new HorgaszokViewModel();
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Felhasznalok")
            {
                SelectedViewModel = new FelhasznalokViewModel();
            }
            else if (parameter.ToString() == "Fogas")
            {
                SelectedViewModel = new FogasokViewModel();
            }
        }
    }
}