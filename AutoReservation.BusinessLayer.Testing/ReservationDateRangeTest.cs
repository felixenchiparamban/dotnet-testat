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
            var reservation = new Reservation
            {
                Bis = new DateTime().AddHours(35),
                Von = new DateTime()
            };

            try
            {
                Target.CheckDateRange(reservation);

            }
            catch (Exception)
            {
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void ScenarioOkay02Test()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void ScenarioNotOkay01Test()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void ScenarioNotOkay02Test()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void ScenarioNotOkay03Test()
        {
            Assert.Inconclusive("Test not implemented.");
        }
    }
}
