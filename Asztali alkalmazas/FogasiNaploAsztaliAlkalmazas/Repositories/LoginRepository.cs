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
    public class LoginRepository
    {
        private Fogasi_naploContext db;
        public LoginRepository()
        {
            db = new Fogasi_naploContext();
        }




        public string Authenticate(string azonosito, string password)
        {
            if (!db.Database.CanConnect())
            {
                return Application.Current.Resources["dbFail"].ToString();
            }

            int.TryParse(azonosito, out int azonositoSzamjegy);
            var dbUser = db.felhasznalok.AsNoTracking().SingleOrDefault(x => x.azonosito == azonositoSzamjegy);
            if (dbUser != null && !string.IsNullOrWhiteSpace(azonosito) && !string.IsNullOrWhiteSpace(password))
            {
                // Begépelt jelszó titkosítása, ezt el kell menteni az adatbázisba!
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                // Jelszó ellenőrzése
                bool verified = BCrypt.Net.BCrypt.Verify(password, dbUser.jelszo);
                if (verified)
                {
                    CurrentUser.szerepkorId = dbUser.szerepkorID;
                    CurrentUser.azonosito = dbUser.azonosito;
                    return Application.Current.Resources["loginSuccess"].ToString();
                }
                else
                {
                    // Hibás login
                    return Application.Current.Resources["loginFail"].ToString();
                }
            }
            else if (string.IsNullOrWhiteSpace(azonosito) && string.IsNullOrWhiteSpace(password))
            {
                return Application.Current.Resources["loginNoData"].ToString();

            }
            else
            {
                // Felhasználó nem létezik
                return Application.Current.Resources["loginNoUser"].ToString();
            }
        }

    }
}
