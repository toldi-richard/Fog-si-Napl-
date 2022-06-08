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
    public class TesztSzerepkorokController
    {
        private SzerepkorokController controller = new SzerepkorokController(new FogasiNaploContext());

        private Szerepkor GetPeldaSzerepkor()
        {
            return new Szerepkor()
            {
                szerepkorID = 1,
                szerepkor_megnev = "adminisztrátor",
            };
        }

        private Szerepkor CreateTesztSzerepkor()
        {
            return new Szerepkor()
            {
                szerepkorID = 4,
                szerepkor_megnev = "titkar",
            };
        }

        [TestMethod]
        // GET: api/Szerepkorok, Darabszám ellenõrzése
        public async Task GetSzerepkorok_DarabszamMegfelel()
        {
            // Http Response üzenet
            var response = await controller.GetRoles();

            // Listává alakítjuk
            var result = response.Value as List<Szerepkor>;

            Assert.IsNotNull(result);
            int szamlalo = 3; // Ennyi helyszin van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Szerepkorok/1, Válasz vizterkod ellenõrzése
        [TestMethod]
        public async Task GetSzerepkor_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az összehasonlítási alap
            var szerepkor = GetPeldaSzerepkor();
            var response = await controller.GetRole(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.szerepkor_megnev, szerepkor.szerepkor_megnev);
        }


        // POST: api/Szerepkorok, Konfliktus ellenõrzése
        [TestMethod]
        public async Task PostSzerepkor_KonfliktusValasszal()
        {
            var szerepkor = GetPeldaSzerepkor();
            var response = await controller.PostRole(szerepkor);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // Típus ellenörzés a példánynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict státuszkód összehasonlítás
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Szerepkorok, Ugyanazt a válaszértéket kapjuk vissza
        [TestMethod]
        public async Task PostSzerepkor_UgyanazonErtekkel()
        {
            var szerepkor = CreateTesztSzerepkor();

            // Elõször létrehozzuk a helyszint
            var response = await controller.PostRole(szerepkor);
            var result = response.Result as CreatedAtActionResult;

            // A válasz által visszaadott fogás
            var valaszSzerepkor = result.Value as Szerepkor;
            Assert.IsNotNull(result);

            // Melyik útvonalon lehet majd lekérdezni
            Assert.AreEqual(result.ActionName, "GetSzerepkor");

            // A GetSzerepkor/{id} útvonalon lesz elérhetõ
            Assert.AreEqual(result.RouteValues["id"], valaszSzerepkor.szerepkorID);

            // A létrehozott szerepkor megnevezése megegyezik, az elküldöttel
            Assert.AreEqual(valaszSzerepkor.szerepkor_megnev, szerepkor.szerepkor_megnev);

            // Töröljük az adatbázisból
            await controller.DeleteRole(valaszSzerepkor.szerepkorID);
        }

        //PUT: api/Szerepkorok/111, Hiba eltérõ azonosító esetén
        [TestMethod]
        public async Task PutSzerepkor_HibaElteroIDnal()
        {
            // 111-es id-ra küldöm a 1-es id-jú szerepkor adatait
            var response = await controller.PutRole(111, GetPeldaSzerepkor());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvertálás is történik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Szerepkorok/id, Státuszkód válasszal
        [TestMethod]
        public async Task DeletSzerepkor_StatuszKodValasszal()
        {
            var szerepkor = CreateTesztSzerepkor();
            var postResponse = await controller.PostRole(szerepkor);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszSzerepkor = postResult.Value as Szerepkor;

            var response = await controller.DeleteRole(valaszSzerepkor.szerepkorID);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent státuszkód ellenõrzése 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
