using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Services;

namespace FogasiNaploAsztaliAlkalmazas.Repositories
{
    public class FelhasznaloRepository : GenericRepository<Felhasznalok, Fogasi_naploContext>
    {
        private Fogasi_naploContext db;
        public FelhasznaloRepository(Fogasi_naploContext context) : base(context)
        {
            db = new Fogasi_naploContext();
        }



        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
        }

        public List<Felhasznalok> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string search = null,
            string sortBy = null,
            bool ascending = true)
        {
            var query = _context.felhasznalok.OrderBy(x => x.azonosito).AsQueryable();

            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                // Ha a keresési kulcsszó szám
                int.TryParse(search, out int szam);


                query = query.Where(x =>
                    x.azonosito.ToString().Contains(search) ||
                    x.email_cim.Contains(search) ||
                    x.szerepkorID == szam ||
                    x.szerepkor.szerepkor_megnev.Contains(search));
            }

            // Sorbarendezés
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "szerepkorID":
                        query = ascending ? query.OrderBy(x => x.szerepkorID) : query.OrderByDescending(x => x.szerepkorID);
                        break;
                    case "azonosito":
                        query = ascending ? query.OrderBy(x => x.azonosito) : query.OrderByDescending(x => x.azonosito);
                        break;
                    case "email_cim":
                        query = ascending ? query.OrderBy(x => x.email_cim) : query.OrderByDescending(x => x.email_cim);
                        break;
                    case "szerepkor":
                        query = ascending ? query.OrderBy(x => x.szerepkor.szerepkor_megnev) : query.OrderByDescending(x => x.szerepkor.szerepkor_megnev);
                        break;
                    case "fogasok":
                        query = ascending ? query.OrderBy(x => x.fogasok) : query.OrderByDescending(x => x.fogasok);
                        break;

                }
            }

            // Összes találat kiszámítása
            _totalItems = query.Count();

            // Oldaltördelés
            // Elkerüljük a minusz oldalszámok feldolgozását
            if (page + itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return query.ToList();
        }

        public bool Exists(Felhasznalok felhasznalok)
        {
            return _context.felhasznalok.Any(x => x.azonosito == felhasznalok.azonosito);
        }


    }
}