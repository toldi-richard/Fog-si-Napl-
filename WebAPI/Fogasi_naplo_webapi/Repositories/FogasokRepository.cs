using BasicAuth;
using Fogasi_naplo_webapi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fogasi_naplo_webapi.Repositories
{
    public class FogasokRepository : IUserService
    {
        private FogasiNaploContext _context;
        public FogasokRepository(FogasiNaploContext Context)
        {
            _context = Context;
        }
        public async Task<bool> Authenticate(string username, string password)
        {
            var dbUser = await _context.felhasznalok
                .SingleOrDefaultAsync(x =>
                x.azonosito.ToString() == username);
            if (dbUser != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, dbUser.jelszo);
                return verified;
            }
            return false;
        }

        public async Task<string> GetIdentifier(string username)
        {
            var dbUser = await _context.felhasznalok
                .SingleOrDefaultAsync(x => x.azonosito.ToString() == username);
            if (dbUser != null)
            {
                return dbUser.azonosito.ToString();
            }
            return string.Empty;
        }

        public async Task<string> GetRole(string username)
        {
            var dbUser = await _context.felhasznalok
                    .SingleOrDefaultAsync(x => x.azonosito.ToString() == username);
            if (dbUser != null)
            {
                return dbUser.szerepkor.szerepkor_megnev;
            }
            return string.Empty;
        }
    }
}
