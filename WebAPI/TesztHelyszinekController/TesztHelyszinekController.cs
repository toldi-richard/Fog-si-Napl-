using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesztFelhasznalokController
{
    [TestClass]
    public class TesztFogasokController
    {
        private HelyszinekController controller = new HelyszinekController(new FogasiNaploContext());

        private Helyszin GetPeldaHelyszin()
        {
            return new Helyszin()
            {
                helyszinID = 1,
                vizterulet_neve = "Kórógyi csatorna",
                vizterkod = 600111,    
            };
        }

        private Helyszin CreateTesztHelyszin()
        {
            return new Helyszin()
            {
                helyszinID = 999,
                vizterulet_neve = "Teszt",
                vizterkod = 999999,
            };
        }

        [TestMethod]
        // GET: api/Helyszinek, Darabszám ellenõrzése
        public async Task GetHelyszinek_DarabszamMegfelel()
        {
            // Http Response üzenet
            var response = await controller.GetPlaces();

            // Listává alakítjuk
            var result = response.Value as List<Helyszin>;

            Assert.IsNotNull(result);
            int szamlalo = 13; // Ennyi helyszin van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Helszinek/1, Válasz vizterkod ellenõrzése
        [TestMethod]
        public async Task GetHelyszin_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az összehasonlítási alap
            var fogas = GetPeldaHelyszin();
            var response = await controller.GetPlace(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.vizterkod, fogas.vizterkod);
        }


        // POST: api/Helszinek, Konfliktus ellenõrzése
        [TestMethod]
        public async Task PostHelyszin_KonfliktusValasszal()
        {
            var helyszin = GetPeldaHelyszin();
            var response = await controller.PostPlace(helyszin);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // Típus ellenörzés a példánynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict státuszkód összehasonlítás
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Helyszinek, Ugyanazt a válaszértéket kapjuk vissza
        [TestMethod]
        public async Task PostHelyszin_UgyanazonErtekkel()
        {
            var helyszin = CreateTesztHelyszin();

            // Elõször létrehozzuk a helyszint
            var response = await controller.PostPlace(helyszin);
            var result = response.Result as CreatedAtActionResult;

            // A válasz által visszaadott fogás
            var valaszHelyszin = result.Value as Helyszin;
            Assert.IsNotNull(result);

            // Melyik útvonalon lehet majd lekérdezni
            Assert.AreEqual(result.ActionName, "GetHelyszin");

            // A GetHelyszin/{id} útvonalon lesz elérhetõ
            Assert.AreEqual(result.RouteValues["id"], valaszHelyszin.helyszinID);

            // A létrehozott helyszin vizterkodja megegyezik, az elküldöttel
            Assert.AreEqual(valaszHelyszin.vizterkod, helyszin.vizterkod);

            // Töröljük az adatbázisból
            await controller.DeletePlace(valaszHelyszin.helyszinID);
        }

        //PUT: api/Helyszinek/111, Hiba eltérõ azonosító esetén
        [TestMethod]
        public async Task PutHelyszin_HibaElteroIDnal()
        {
            // 111-es id-ra küldöm a 1-es id-jú helyszín adatait
            var response = await controller.PutPlace(111, GetPeldaHelyszin());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvertálás is történik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Helyszinek/id, Státuszkód válasszal
        [TestMethod]
        public async Task DeletHelyszin_StatuszKodValasszal()
        {
            var helyszin = CreateTesztHelyszin();
            var postResponse = await controller.PostPlace(helyszin);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszHelyszin = postResult.Value as Helyszin;

            var response = await controller.DeletePlace(valaszHelyszin.helyszinID);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent státuszkód ellenõrzése 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
