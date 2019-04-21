using System;
using System.Collections.Generic;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using repository;
using agregat;
using System.Linq;
namespace DDD_Test
{
    [TestClass]
    public class UnitTest1
    {
        //la BDD est reset avant chaque test
        [TestMethod]
        public void createEntretient()
        {
            entretient_control entretient_Control = new entretient_control();
            List<Recruiter> recruiters = entretient_Control.GetRecruteurs().ToList();
            Candidate candidate = entretient_Control.GetCandidatByName("MEYER", "Alexandre"); 
             Creneau creneau = entretient_Control.getCreneauInAnyRoom(new DateTime(2019, 4, 22,16,00,00), new DateTime(2019, 4, 22, 18, 00, 00));
            Entretien entretien= entretient_Control.CreateEntretient(recruiters, candidate, creneau);
            Assert.IsNotNull(entretien);
            Assert.AreEqual(entretien.creneau.heureDebut, new DateTime(2019, 4, 22, 16, 00, 00));
            Assert.AreEqual(entretien.creneau.HeureFin, new DateTime(2019, 4, 22, 18, 00, 00));
            Assert.AreEqual(entretien.recruiter[1].firstname,recruiters[1].firstname);

        }
        [TestMethod]
        public void createCreneauTimeStepOver()
        {
            entretient_control entretient_Control = new entretient_control();
            Creneau creneau = entretient_Control.getCreneauInAnyRoom(new DateTime(2019, 4, 19,16,00,00), new DateTime(2019, 4, 19, 18, 00, 00));
            Creneau creneau2 = entretient_Control.getCreneauInAnyRoom(new DateTime(2019, 4, 19,17,00,00), new DateTime(2019, 4, 19, 19, 00, 00));
            Assert.IsNotNull(creneau);
            Assert.IsNotNull(creneau2);
            Assert.AreNotEqual(creneau.salle,creneau2.salle);

        }
        [TestMethod]
        [ExpectedException(typeof(SpecificRoomNotAvailable))]
        public void createCreneauSpecificRoomFail()
        {
            entretient_control entretient_Control = new entretient_control();
            Creneau creneau = entretient_Control.getCreneau(new DateTime(2019, 4, 19,19,00,00), new DateTime(2019, 4, 19, 21, 00, 00), "A06");
            Creneau creneau2 = entretient_Control.getCreneau(new DateTime(2019, 4, 19,20,00,00), new DateTime(2019, 4, 19, 22, 00, 00), "A06");
            Assert.IsNotNull(creneau);
            Assert.IsNotNull(creneau2);
            Assert.AreNotEqual(creneau.salle,creneau2.salle);

        }
        [TestMethod]
        public void testStates()
        {
            entretient_control entretient_Control = new entretient_control();
            Creneau creneau = entretient_Control.getCreneauInAnyRoom(new DateTime(2019, 4, 19,19,00,00), new DateTime(2019, 4, 19, 21, 00, 00));
            Assert.IsNotNull(creneau);

        }
    }
}
