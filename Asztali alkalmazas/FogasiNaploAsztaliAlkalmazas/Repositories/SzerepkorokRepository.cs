using FogasiNaploAsztaliAlkalmazas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace FogasiNaploAsztaliAlkalmazas.Repositories
{
    public class SzerepkorokRepository : GenericRepository<Szerepkorok, Fogasi_naploContext>
    {
        public SzerepkorokRepository(Fogasi_naploContext context) : base(context)
        {
        }

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
        }


        public bool Exists(Szerepkorok szerepkor)
        {
            return _context.szerepkorok.Any(x => x.szerepkorID == szerepkor.szerepkorID);
        }
    }
}