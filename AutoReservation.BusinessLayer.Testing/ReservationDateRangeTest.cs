using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationDateRangeTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioOkay01Test()
        {
            var range24 = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 28) };
            Assert.IsTrue(Target.CheckDateRange(range24));
        }

        [TestMethod]
        public void ScenarioOkay02Test()
        {
            var range24 = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 23) };
            Assert.IsTrue(Target.CheckDateRange(range24));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay01Test()
        {
            var rangeNot24 = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 23).AddHours(-12) };
            Target.CheckDateRange(rangeNot24);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay02Test()
        {
            var rangeNot24 = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 24).AddDays(-5)};
            Target.CheckDateRange(rangeNot24);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioNotOkay03Test()
        {
            var rangeNot24 = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 23).AddHours(-4) };
            Target.CheckDateRange(rangeNot24);
            Assert.Fail();
        }
    }
}
