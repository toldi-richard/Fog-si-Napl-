using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.DTOs;
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
        private FogasokController controller = new FogasokController(new FogasiNaploContext());

        private Fogas GetPeldaFogas()
        {
            return new Fogas()
            {
                fogasID = 1,
                azonosito = 111111,
                helyszinID = 9,
                datum_idopont = new DateTime(2022,03,17),
                halfaj = "Teszt",
                suly = 6
            };
        }

        private Fogas CreateTesztFogas()
        {
            return new Fogas()
            {
                fogasID = 9999,
                azonosito = 111111,
                helyszinID = 9,
                datum_idopont = new DateTime(2022, 03, 17),
                halfaj = "Teszt",
                suly = 6
            };
        }

        [TestMethod]
        // GET: api/Fogasok, Darabszám ellenõrzése
        public async Task GetFogasok_DarabszamMegfelel()
        {
            // Http Response üzenet
            var response = await controller.GetCatches(0);

            // Listává alakítjuk
            var result = response.Value as List<FogasokDTO>;

            Assert.IsNotNull(result);
            int szamlalo = 67; // Ennyi fogás van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Fogasok/9999, Válasz azonosító ellenõrzése
        [TestMethod]
        public async Task GetFogas_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az összehasonlítási alap
            var fogas = GetPeldaFogas();
            var response = await controller.GetCatch(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.azonosito, fogas.azonosito);
        }


        // POST: api/Fogasok, Konfliktus ellenõrzése
        [TestMethod]
        public async Task PostFogas_KonfliktusValasszal()
        {
            var fogas = GetPeldaFogas();
            var response = await controller.PostCatch(fogas);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // Típus ellenörzés a példánynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict státuszkód összehasonlítás
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Fogasok, Ugyanazt a válaszértéket kapjuk vissza
        [TestMethod]
        public async Task PostFogas_UgyanazonErtekkel()
        {
            var fogas = CreateTesztFogas();

            // Elõször létrehozzuk a fogást
            var response = await controller.PostCatch(fogas);
            var result = response.Result as CreatedAtActionResult;

            // A válasz által visszaadott fogás
            var valaszFogas = result.Value as Fogas;
            Assert.IsNotNull(result);

            // Melyik útvonalon lehet majd lekérdezni
            Assert.AreEqual(result.ActionName, "GetFogas");

            // A GetCatch/{id} útvonalon lesz elérhetõ
            Assert.AreEqual(result.RouteValues["id"], valaszFogas.fogasID);

            // A létrehozott fogas halfaja megegyezik, az elküldöttel
            Assert.AreEqual(valaszFogas.halfaj, fogas.halfaj);

            // Töröljük az adatbázisból
            await controller.DeleteCatch(valaszFogas.fogasID);
        }

        //PUT: api/Fogasok/111, Hiba eltérõ azonosító esetén
        [TestMethod]
        public async Task PutFogasok_HibaElteroIDnal()
        {
            // 111-es id-ra küldöm a 1-es id-jú fogás adatait
            var response = await controller.PutCatch(111, GetPeldaFogas());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvertálás is történik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Fogasok/id, Státuszkód válasszal
        [TestMethod]
        public async Task DeletFogas_StatuszKodValasszal()
        {
            var fogas = CreateTesztFogas();
            var postResponse = await controller.PostCatch(fogas);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszFogas = postResult.Value as Fogas;

            var response = await controller.DeleteCatch(valaszFogas.fogasID);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent státuszkód ellenõrzése 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
