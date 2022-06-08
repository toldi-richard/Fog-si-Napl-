using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesztFelhasznalokController
{
    [TestClass]
    public class TesztFelhasznalokController
    {
          private FelhasznalokController controller = new FelhasznalokController(new FogasiNaploContext());

        private Felhasznalo GetPeldaFelhasznalo()
        {
            return new Felhasznalo()
            {
                azonosito = 111111,
                szerepkorID = 3,
                email_cim = "Agoston1@gmail.com",
                jelszo = "Teszt"
            };
        }

        private Felhasznalo CreateTesztFelhasznalo()
        {
            return new Felhasznalo()
            {
                azonosito = 999999,
                szerepkorID = 3,
                email_cim = "Teszt@gmail.com",
                jelszo = "Teszt"
            };
        }

        [TestMethod]
        // GET: api/Felhasznalok, Darabszám ellenõrzése
        public async Task GetFelhasznalok_DarabszamMegfelel()
        {
            // Http Response üzenet
            var response = await controller.GetUsers();

            // Listává alakítjuk
            var result = response.Value as List<Felhasznalo>;

            Assert.IsNotNull(result);
            int szamlalo = 13; // Ennyi felhasználó van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);

        }

        // GET: api/Felhasznalok/111111, Válasz email ellenõrzése
        [TestMethod]
        public async Task GetFelhasznalo_ValaszUgyanazonEmaillel()
        {
            // Ez lesz az összehasonlítási alap
            var felhasznalo = GetPeldaFelhasznalo();
            var response = await controller.GetUser(111111);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.email_cim, felhasznalo.email_cim);
        }

        // POST: api/Felhasznalok, Konfliktus ellenõrzése
        [TestMethod]
        public async Task PostFelhasznalo_KonfliktusValasszal()
        {
            var felhasznalo = GetPeldaFelhasznalo();
            var response = await controller.PostUser(felhasznalo);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // Típus ellenörzés a példánynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict státuszkód összehasonlítás
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Felhasznalok, Ugyanazt a válaszértéket kapjuk vissza
        [TestMethod]
        public async Task PostFelhasznalo_UgyanazonErtekkel()
        {
            var felhasznalo = CreateTesztFelhasznalo();

            // Elõször létrehozzuk a felhasznalot
            var response = await controller.PostUser(felhasznalo);
            var result = response.Result as CreatedAtActionResult;

            // A válasz által visszaadott felhasználó
            var valaszFelhasznalo = result.Value as Felhasznalo;
            Assert.IsNotNull(result);

            // Melyik útvonalon lehet majd lekérdezni
            Assert.AreEqual(result.ActionName, "GetFelhasznalo");

            // A GetUser/{id} útvonalon lesz elérhetõ
            Assert.AreEqual(result.RouteValues["id"], valaszFelhasznalo.azonosito);

            // A létrehozott felhasználó email címe megegyezik, az elküldöttel
            Assert.AreEqual(valaszFelhasznalo.email_cim, felhasznalo.email_cim);

            // Töröljük az adatbázisból
            await controller.DeleteUser(valaszFelhasznalo.azonosito);
        }

        //PUT: api/Felhasznalok/999999, Hiba eltérõ azonosító esetén
        [TestMethod]
        public async Task PutFelhasznalok_HibaElteroIDnal()
        {
            // 999999-es azonosító-ra küldöm a 111111-es azonosító-jú tanár adatait
            var response = await controller.PutUser(999999, GetPeldaFelhasznalo());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvertálás is történik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Felhasznalok/id, Státuszkód válasszal
        [TestMethod]
        public async Task DeletFelhasznalo_StatuszKodValasszal()
        {
            var felhasznalo = CreateTesztFelhasznalo();
            var postResponse = await controller.PostUser(felhasznalo);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszFelhasznalo = postResult.Value as Felhasznalo;

            var response = await controller.DeleteUser(valaszFelhasznalo.azonosito);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent státuszkód ellenõrzése 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
