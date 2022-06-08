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
    public class SzerepkorokViewModelTests
    {
        [TestMethod()]
        public void SzerepkorokViewModelTest_GetAllData()
        {
            RunEnvironment.ChangeToTestEnvironment();

            SzerepkorokViewModel szerepkorokViewModel = new SzerepkorokViewModel();

            int expected = 3;
            int actual = szerepkorokViewModel.Szerepkorok.Count;

            Assert.AreEqual(expected, actual, "Nem lett beolvasva az összes szerepkor!");
        }

        [TestMethod()]
        public void SzerepkorokViewModelTest_DeleteInsertTest()
        {
            RunEnvironment.ChangeToTestEnvironment();
            SzerepkorokViewModel szerepkorokViewModel = new SzerepkorokViewModel();

            List<Szerepkorok> szerepkorok = szerepkorokViewModel.Szerepkorok.ToList();
            if (szerepkorok.Count > 0)
            {
                Szerepkorok torlendoSzerepkor = szerepkorok.ElementAt(1);

                szerepkorokViewModel.Delete(torlendoSzerepkor);

                int expected = 2;
                int actual = szerepkorokViewModel.Szerepkorok.Count;

                Assert.AreEqual(expected, actual, "Törlések után a szerepkörök száma nem csökkent!");

                List<Szerepkorok> toroltLista = szerepkorokViewModel.Szerepkorok.ToList();

                expected = 0;
                actual = toroltLista.FindAll(szerepkorok => szerepkorok.szerepkorID == torlendoSzerepkor.szerepkorID).Count;
                Assert.AreEqual(expected, actual, "Törlések után a törölt szerepkör id-je megtalálható a listában!");

                szerepkorokViewModel.Save(torlendoSzerepkor);
                expected = 3;
                actual = szerepkorokViewModel.Szerepkorok.Count;

                Assert.AreEqual(expected, actual, "Új szerepkör felvétele után a szerepkörök száma nem nőtt!");
            }
        }
    }
}