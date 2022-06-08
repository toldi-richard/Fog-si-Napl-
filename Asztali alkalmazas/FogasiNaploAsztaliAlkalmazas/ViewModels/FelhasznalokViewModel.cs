using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class FelhasznalokViewModel : PagerViewModel
    {
        private FelhasznaloRepository felhasznaloRepo;
        private GenericRepository<Szerepkorok, Fogasi_naploContext> szerepkorRepo;
        private GenericRepository<Fogasok, Fogasi_naploContext> fogasRepo;

        private ObservableCollection<Felhasznalok> _felhasznalok;
      
        // Ezt jelenítjük meg a View rétegen
        public ObservableCollection<Felhasznalok> Felhasznalok
        {
            get { return _felhasznalok; }
            set { SetProperty(ref _felhasznalok, value); }
        }

        private Felhasznalok _selectedFelhasznalo;

        public Felhasznalok SelectedFelhasznalo
        {
            get { return _selectedFelhasznalo; }
            set { SetProperty(ref _selectedFelhasznalo, value); }
        }

        public ObservableCollection<Szerepkorok> Szerepkorok { get; set; }
        public ObservableCollection<Fogasok> Fogasok { get; set; }
        public RelayCommand NewCmd { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public FelhasznalokViewModel()
        {
            var context = new Fogasi_naploContext();
            felhasznaloRepo = new FelhasznaloRepository(context);
            szerepkorRepo = new GenericRepository<Szerepkorok, Fogasi_naploContext>(context);
            Szerepkorok = new ObservableCollection<Szerepkorok>(szerepkorRepo.GetAll());
            
            fogasRepo = new GenericRepository<Fogasok, Fogasi_naploContext>(context);
            Fogasok = new ObservableCollection<Fogasok>(fogasRepo.GetAll());

            NewCmd = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedFelhasznalo));
            DeleteCmd = new RelayCommand(() => Delete(SelectedFelhasznalo));
            LoadData();
        }

        protected override void LoadData()
        {
            // A view-on lévő változókat át kell adni a repositorynak
            var query = felhasznaloRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            // Értéket kötelező megadni a lapozás működéséhez
            TotalItems = felhasznaloRepo.TotalItems;
            Felhasznalok = new ObservableCollection<Felhasznalok>(query);
            New();
        }

        private void New()
        {
            SelectedFelhasznalo = new Felhasznalok();
        }

        public void Save(Felhasznalok felhasznalok)
        {
            // Ha létezik, akkor módosítás történik
            if (felhasznaloRepo.Exists(felhasznalok))
            {
                felhasznaloRepo.Update(felhasznalok);
            }
            // Ha korábban nem létezett, akkor pedig beillesztés
            else
            {
                felhasznaloRepo.Insert(felhasznalok);
                // Add = Lista végére rakja, Insert = megadott indexű helyre szúrja be
                Felhasznalok.Insert(0, felhasznalok);
            }
        }

        public void Delete(Felhasznalok felhasznalo)
        {
            felhasznaloRepo.Delete(felhasznalo.azonosito);
            Felhasznalok.Remove(felhasznalo);
        }

    }
}