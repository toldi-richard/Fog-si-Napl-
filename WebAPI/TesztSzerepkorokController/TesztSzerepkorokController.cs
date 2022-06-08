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
                szerepkor_megnev = "adminisztr�tor",
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
        // GET: api/Szerepkorok, Darabsz�m ellen�rz�se
        public async Task GetSzerepkorok_DarabszamMegfelel()
        {
            // Http Response �zenet
            var response = await controller.GetRoles();

            // List�v� alak�tjuk
            var result = response.Value as List<Szerepkor>;

            Assert.IsNotNull(result);
            int szamlalo = 3; // Ennyi helyszin van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Szerepkorok/1, V�lasz vizterkod ellen�rz�se
        [TestMethod]
        public async Task GetSzerepkor_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az �sszehasonl�t�si alap
            var szerepkor = GetPeldaSzerepkor();
            var response = await controller.GetRole(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.szerepkor_megnev, szerepkor.szerepkor_megnev);
        }


        // POST: api/Szerepkorok, Konfliktus ellen�rz�se
        [TestMethod]
        public async Task PostSzerepkor_KonfliktusValasszal()
        {
            var szerepkor = GetPeldaSzerepkor();
            var response = await controller.PostRole(szerepkor);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // T�pus ellen�rz�s a p�ld�nynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict st�tuszk�d �sszehasonl�t�s
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Szerepkorok, Ugyanazt a v�lasz�rt�ket kapjuk vissza
        [TestMethod]
        public async Task PostSzerepkor_UgyanazonErtekkel()
        {
            var szerepkor = CreateTesztSzerepkor();

            // El�sz�r l�trehozzuk a helyszint
            var response = await controller.PostRole(szerepkor);
            var result = response.Result as CreatedAtActionResult;

            // A v�lasz �ltal visszaadott fog�s
            var valaszSzerepkor = result.Value as Szerepkor;
            Assert.IsNotNull(result);

            // Melyik �tvonalon lehet majd lek�rdezni
            Assert.AreEqual(result.ActionName, "GetSzerepkor");

            // A GetSzerepkor/{id} �tvonalon lesz el�rhet�
            Assert.AreEqual(result.RouteValues["id"], valaszSzerepkor.szerepkorID);

            // A l�trehozott szerepkor megnevez�se megegyezik, az elk�ld�ttel
            Assert.AreEqual(valaszSzerepkor.szerepkor_megnev, szerepkor.szerepkor_megnev);

            // T�r�lj�k az adatb�zisb�l
            await controller.DeleteRole(valaszSzerepkor.szerepkorID);
        }

        //PUT: api/Szerepkorok/111, Hiba elt�r� azonos�t� eset�n
        [TestMethod]
        public async Task PutSzerepkor_HibaElteroIDnal()
        {
            // 111-es id-ra k�ld�m a 1-es id-j� szerepkor adatait
            var response = await controller.PutRole(111, GetPeldaSzerepkor());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvert�l�s is t�rt�nik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Szerepkorok/id, St�tuszk�d v�lasszal
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
            // 204 NoContent st�tuszk�d ellen�rz�se 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
