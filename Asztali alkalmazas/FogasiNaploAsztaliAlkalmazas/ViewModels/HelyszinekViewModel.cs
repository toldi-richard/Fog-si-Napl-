using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class HelyszinekViewModel : PagerViewModel
    {
        private HelyszinekRepository helyszinRepo;


        private ObservableCollection<Helyszinek> _helyszinek;
        // Ezt jelenítjük meg a View rétegen
        public ObservableCollection<Helyszinek> Helyszinek
        {
            get { return _helyszinek; }
            set { SetProperty(ref _helyszinek, value); }
        }

        private Helyszinek _selectedHelyszin;

        public Helyszinek SelectedHelyszin
        {
            get { return _selectedHelyszin; }
            set { SetProperty(ref _selectedHelyszin, value); }
        }


        public RelayCommand NewCmd { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public HelyszinekViewModel()
        {
            var context = new Fogasi_naploContext();
            helyszinRepo = new HelyszinekRepository(context);
            NewCmd = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedHelyszin));
            DeleteCmd = new RelayCommand(() => Delete(SelectedHelyszin));
            LoadData();
        }

        protected override void LoadData()
        {
            // A view-on lévő változókat át kell adni a repositorynak
            var query = helyszinRepo.GetAll();
            // Értéket kötelező megadni a lapozás működéséhez
            TotalItems = helyszinRepo.TotalItems;
            Helyszinek = new ObservableCollection<Helyszinek>(query);
            New();
        }

        private void New()
        {
            SelectedHelyszin = new Helyszinek();
        }

        public void Save(Helyszinek helyszin)
        {
            // Ha létezik, akkor módosítás történik
            if (helyszinRepo.Exists(helyszin))
            {
                helyszinRepo.Update(helyszin);
            }
            // Ha korábban nem létezett, akkor pedig beillesztés
            else
            {
                helyszinRepo.Insert(helyszin);
                // Add = Lista végére rakja, Insert = megadott indexű helyre szúrja be
                Helyszinek.Insert(0, helyszin);
            }
        }

        public void Delete(Helyszinek helyszin)
        {
            helyszinRepo.Delete(helyszin.helyszinID);
            Helyszinek.Remove(helyszin);
        }

    }
}