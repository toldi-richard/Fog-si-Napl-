using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public abstract class PagerViewModel : ObservableObject
    {
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            // Ha a nézeten akarjuk megjeleníteni a változást
            // Vagy a nézetről változtatjuk az adatot
            set { SetProperty(ref _searchKey, value); page = 1; }
        }

        private string _sortBy;
        public string SortBy
        {
            get { return _sortBy; }
            set 
            {
                SetProperty(ref _sortBy, value);
                if (value == _sortBy)
                {
                    ascending = !ascending;
                }
                LoadData(); 
            }
        }

        protected bool ascending;

        // A view-ban használható, adatok újratöltésére
        public RelayCommand LoadDataCmd { get; set; }

        // A gyermek osztály fogja definiálni
        protected abstract void LoadData();

        #region Oldaltördelés
        private string _currentPage; // 1/10
        public string CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public ObservableCollection<int> ItemsPerPageList { get; set; }

        private int _itemsPerPage = 20;
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set 
            { 
                SetProperty(ref _itemsPerPage, value);
                LoadData();
            }
        }
        protected int page = 1;
        private int pageCount;

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set 
            {
                pageCount = (value - 1) / ItemsPerPage + 1;
                CurrentPage = page + "/" + pageCount;
                SetProperty(ref _totalItems, value);
                // Ha módosítja az ItemsPerPage változót, és túllépi az oldalak számát, akkor lapozva átrakja az utolsó oldalra
                if (page > pageCount)
                {
                    page = pageCount;
                }
            }
        }
        public RelayCommand FirstPageCmd { get; set; }
        public RelayCommand NextPageCmd { get; set; }
        public RelayCommand PreviousPageCmd { get; set; }
        public RelayCommand LastPageCmd { get; set; }

        private void FirstPage()
        {
            page = 1;
            LoadData();
        }
        private void PrevPage()
        {
            if (page > 1)
            {
                page--;
                LoadData();
            }
        }
        private void NextPage()
        {
            if (page < pageCount)
            {
                page++;
                LoadData();
            }
        }
        private void LastPage()
        {
            page = pageCount;
            LoadData();
        }
        #endregion

        public PagerViewModel()
        {
            LoadDataCmd = new RelayCommand(() => LoadData());
            FirstPageCmd = new RelayCommand(() => FirstPage());
            PreviousPageCmd = new RelayCommand(() => PrevPage());
            NextPageCmd = new RelayCommand(() => NextPage());
            LastPageCmd = new RelayCommand(() => LastPage());
            ItemsPerPageList = new ObservableCollection<int>(new List<int>() { 10, 20, 50 });
        }
    }
}
