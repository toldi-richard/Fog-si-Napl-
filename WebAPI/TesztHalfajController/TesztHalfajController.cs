using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fogasi_naplo_WebAPI_Test
{
    // Nem adatbázis alapú egység tesztek a Halfaj controllerre
    [TestClass]
    public class TesztHalfajController
    {
        private List<Halfaj> GetTestFishes()
        {
            var testFishes = new List<Halfaj>();

            testFishes.Add(new Halfaj { Id = 1, Nev = "Ponty", LatinNev = "Cyprinus carpio", Elettartam = 20 });
            testFishes.Add(new Halfaj { Id = 2, Nev = "Keszeg", LatinNev = "Abramis brama", Elettartam = 7 });
            testFishes.Add(new Halfaj { Id = 3, Nev = "Csuka", LatinNev = "Esox lucius", Elettartam = 10 });
            testFishes.Add(new Halfaj { Id = 4, Nev = "Harcsa", LatinNev = "Silurus glanis", Elettartam = 60 });

            return testFishes;
        }

        [TestMethod]
        public void GetAllFishes_VisszaadMindenHalfajt()
        {
            var testFishes = GetTestFishes();
            var controller = new HalfajController(testFishes);
            var result = controller.GetAllFishes() as List<Halfaj>;
            Assert.AreEqual(testFishes.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllFishesAsync_VisszaadMindenHalfajt()
        {
            var testFishes = GetTestFishes();
            var controller = new HalfajController(testFishes);
            var result = await controller.GetAllFishesAsync() as List<Halfaj>;
            Assert.AreEqual(testFishes.Count, result.Count);
        }

        [TestMethod]
        public void GetFish_VisszaadPontosHalfajt()
        {
            var testFishes = GetTestFishes();
            var controller = new HalfajController(testFishes);
            var result = controller.GetFish(3) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Csuka", (result.Value as Halfaj).Nev);
        }

        [TestMethod]
        public async Task GetFishAsync_VisszaadPontosHalfaj()
        {
            var testFishes = GetTestFishes();
            var controller = new HalfajController(testFishes);
            var result = await controller.GetFishAsync(3) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Csuka", (result.Value as Halfaj).Nev);
        }

        [TestMethod]
        public void GetFish_NemTalalHalfajok()
        {
            var controller = new HalfajController(GetTestFishes());
            var result = controller.GetFish(999);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.
           NotFoundResult));
        }
    }
}
