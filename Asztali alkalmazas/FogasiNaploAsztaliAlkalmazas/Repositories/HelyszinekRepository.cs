using FogasiNaploAsztaliAlkalmazas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.Repositories
{
    public class HelyszinekRepository : GenericRepository<Helyszinek, Fogasi_naploContext>
    {
        public HelyszinekRepository(Fogasi_naploContext context) : base(context)
        {
        }

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
        }


        public bool Exists(Helyszinek helyszin)
        {
            return _context.helyszinek.Any(x => x.helyszinID == helyszin.helyszinID);
        }
    }
}

