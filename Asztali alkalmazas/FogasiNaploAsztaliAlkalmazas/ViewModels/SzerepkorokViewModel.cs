using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class SzerepkorokViewModel : PagerViewModel
    {
        private SzerepkorokRepository szerepkorRepo;


        private ObservableCollection<Szerepkorok> _szerepkorok;
        // Ezt jelenítjük meg a View rétegen
        public ObservableCollection<Szerepkorok> Szerepkorok
        {
            get { return _szerepkorok; }
            set { SetProperty(ref _szerepkorok, value); }
        }

        private Szerepkorok _selectedSzerepkor;

        public Szerepkorok SelectedSzerepkor
        {
            get { return _selectedSzerepkor; }
            set { SetProperty(ref _selectedSzerepkor, value); }
        }


        public RelayCommand NewCmd { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public SzerepkorokViewModel()
        {
            var context = new Fogasi_naploContext();
            szerepkorRepo = new SzerepkorokRepository(context);
            NewCmd = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedSzerepkor));
            DeleteCmd = new RelayCommand(() => Delete(SelectedSzerepkor));
            LoadData();
        }

        protected override void LoadData()
        {
            // A view-on lévő változókat át kell adni a repositorynak
            var query = szerepkorRepo.GetAll();
            // Értéket kötelező megadni a lapozás működéséhez
            TotalItems = szerepkorRepo.TotalItems;
            Szerepkorok = new ObservableCollection<Szerepkorok>(query);
            New();
        }

        private void New()
        {
            SelectedSzerepkor = new Szerepkorok();
        }

        public void Save(Szerepkorok szerepkor)
        {
            // Ha létezik, akkor módosítás történik
            if (szerepkorRepo.Exists(szerepkor))
            {
                szerepkorRepo.Update(szerepkor);
            }
            // Ha korábban nem létezett, akkor pedig beillesztés
            else
            {
                szerepkorRepo.Insert(szerepkor);
                // Add = Lista végére rakja, Insert = megadott indexű helyre szúrja be
                Szerepkorok.Insert(0, szerepkor);
            }
        }

        public void Delete(Szerepkorok szerepkor)
        {
            szerepkorRepo.Delete(szerepkor.szerepkorID);
            Szerepkorok.Remove(szerepkor);
        }

    }
}