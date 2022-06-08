using FogasiNaploAsztaliAlkalmazas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.Repositories
{
    public class FogasRepository : GenericRepository<Fogasok, Fogasi_naploContext>
    {
        public FogasRepository(Fogasi_naploContext context) : base(context)
        {
        }

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
        }

        public List<Fogasok> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string search = null,
            string sortBy = null,
            bool ascending = true)
        {
            var query = _context.fogasok.OrderBy(x => x.fogasID).AsQueryable();

            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                // Ha a keresési kulcsszó szám
                int.TryParse(search, out int szam);
                // Ha dátum
                DateTime.TryParse(search, out DateTime datum);

                query = query.Where(x =>
                    x.fogasID.ToString().Contains(search) ||
                    x.helyszin.vizterulet_neve.Contains(search)||
                    x.suly == szam ||
                    x.azonosito == szam ||
                    x.datum_idopont.Equals(datum) ||
                    x.halfaj.ToLower().Contains(search));
            }

            // Sorbarendezés
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "fogasID":
                        query = ascending ? query.OrderBy(x => x.fogasID) : query.OrderByDescending(x => x.fogasID);
                        break;
                    case "azonosito":
                        query = ascending ? query.OrderBy(x => x.azonosito) : query.OrderByDescending(x => x.azonosito);
                        break;
                    case "helyszin.vizterulet_neve":
                        query = ascending ? query.OrderBy(x => x.helyszin.vizterulet_neve) : query.OrderByDescending(x => x.helyszin.vizterulet_neve);
                        break;
                    case "horgaszhely":
                        query = ascending ? query.OrderBy(x => x.horgaszhely) : query.OrderByDescending(x => x.horgaszhely);
                        break;
                    case "datum_idopont":
                        query = ascending ? query.OrderBy(x => x.datum_idopont) : query.OrderByDescending(x => x.datum_idopont);
                        break;
                    case "halfaj":
                        query = ascending ? query.OrderBy(x => x.halfaj) : query.OrderByDescending(x => x.halfaj);
                        break;
                    case "suly":
                        query = ascending ? query.OrderBy(x => x.suly) : query.OrderByDescending(x => x.suly);
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

        public bool Exists(Fogasok fogas)
        {
            return _context.fogasok.Any(x => x.fogasID == fogas.fogasID);
        }
    }
}

