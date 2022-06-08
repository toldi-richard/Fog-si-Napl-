using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class FogasokViewModel : PagerViewModel
    {
        

        private FogasRepository fogasRepo;
        private GenericRepository<Helyszinek, Fogasi_naploContext> helyszinRepo;

        private ObservableCollection<Fogasok> _fogasok;
        // Ezt jelenítjük meg a View rétegen
        public ObservableCollection<Fogasok> Fogasok
        {
            get { return _fogasok; }
            set { SetProperty(ref _fogasok, value); }
        }

        private Fogasok _selectedFogas;

        public Fogasok SelectedFogas
        {
            get { return _selectedFogas; }
            set { SetProperty(ref _selectedFogas, value); }
        }

        public ObservableCollection<Helyszinek> Helyszinek { get; set; }
        public RelayCommand NewCmd { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public FogasokViewModel()
        {
            var context = new Fogasi_naploContext();
            fogasRepo = new FogasRepository(context);
            helyszinRepo = new GenericRepository<Helyszinek, Fogasi_naploContext>(context);
            Helyszinek = new ObservableCollection<Helyszinek>(helyszinRepo.GetAll());

            NewCmd = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedFogas));
            DeleteCmd = new RelayCommand(() => Delete(SelectedFogas));
            LoadData();
        }

        protected override void LoadData()
        {
            // A view-on lévő változókat át kell adni a repositorynak
            var query = fogasRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            // Értéket kötelező megadni a lapozás működéséhez
            TotalItems = fogasRepo.TotalItems;
            Fogasok = new ObservableCollection<Fogasok>(query);
            New();
        }

        private void New()
        {
            SelectedFogas = new Fogasok();
        }

        public void Save(Fogasok fogas)
        {
            // Ha létezik, akkor módosítás történik
            if (fogasRepo.Exists(fogas))
            {
                fogasRepo.Update(fogas);
            }
            // Ha korábban nem létezett, akkor pedig beillesztés
            else
            {
                fogasRepo.Insert(fogas);
                // Add = Lista végére rakja, Insert = megadott indexű helyre szúrja be
                Fogasok.Insert(0, fogas);
            }
        }

        public void Delete(Fogasok fogas)
        {
            fogasRepo.Delete(fogas.fogasID);
            Fogasok.Remove(fogas);
        }
        
        
    }
}