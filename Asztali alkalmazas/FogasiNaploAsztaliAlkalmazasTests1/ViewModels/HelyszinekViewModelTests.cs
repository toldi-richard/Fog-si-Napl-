using Microsoft.VisualStudio.TestTools.UnitTesting;
using FogasiNaploAsztaliAlkalmazas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogasiNaploAsztaliAlkalmazas.Stores;
using FogasiNaploAsztaliAlkalmazas.Models;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels.Tests
{
    [TestClass()]
    public class HelyszinekViewModelTests
    {
        [TestMethod()]
        public void HelyszinekViewModelTest_GetAllData()
        {
            RunEnvironment.ChangeToTestEnvironment();

            HelyszinekViewModel helyszinekViewModel = new HelyszinekViewModel();

            int expected = 13;
            int actual = helyszinekViewModel.Helyszinek.Count;

            Assert.AreEqual(expected, actual, "Nem lett beolvasva az összes helyszin!");
        }

        [TestMethod()]
        public void HelyszinekViewModelTest_DeleteInsertTest()
        {
            RunEnvironment.ChangeToTestEnvironment();
            HelyszinekViewModel helyszinekViewModel = new HelyszinekViewModel();

            List<Helyszinek> helyszinek = helyszinekViewModel.Helyszinek.ToList();
            if (helyszinek.Count > 0)
            {
                Helyszinek torlendoHelyszin = helyszinek.ElementAt(1);

                helyszinekViewModel.Delete(torlendoHelyszin);

                int expected = 12;
                int actual = helyszinekViewModel.Helyszinek.Count;

                Assert.AreEqual(expected, actual, "Törlések után a helyszinek száma nem csökkent!");

                List<Helyszinek> toroltLista = helyszinekViewModel.Helyszinek.ToList();

                expected = 0;
                actual = toroltLista.FindAll(helyszinek => helyszinek.helyszinID == torlendoHelyszin.helyszinID).Count;
                Assert.AreEqual(expected, actual, "Törlések után a törölt helyszín id-je megtalálható a listában!");

                helyszinekViewModel.Save(torlendoHelyszin);
                expected = 13;
                actual = helyszinekViewModel.Helyszinek.Count;

                Assert.AreEqual(expected, actual, "Új helyszin felvétele után a helyszinek száma nem nőtt!");
            }
        }
    }
}