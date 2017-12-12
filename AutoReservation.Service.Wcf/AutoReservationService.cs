using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoManager autoManager;
        private KundeManager kundeManager;
        private ReservationManager reservationManager;

        public AutoReservationService()
        {
            this.autoManager = new AutoManager();
            this.kundeManager = new KundeManager();
            this.reservationManager = new ReservationManager();
        }

        private static void WriteActualMethod() 
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public bool DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoManager.DeleteAuto(auto.ConvertToEntity());
            return true;
        }

        public bool DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            kundeManager.DeleteKunde(kunde.ConvertToEntity());
            return true;
        }

        public bool DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            reservationManager.DeleteReservation(reservation.ConvertToEntity());
            return true;
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return autoManager.GetAutoById(id).ConvertToDto();
        }

        public List<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return autoManager.List.ConvertToDtos();
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return kundeManager.GetKundeById(id).ConvertToDto();
        }

        public List<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return kundeManager.List.ConvertToDtos();
        }

        public ReservationDto GetReservationById(int id)
        {
            WriteActualMethod();
            return reservationManager.GetReservationbyID(id).ConvertToDto();
        }

        public List<ReservationDto> GetReservationen()
        {
            WriteActualMethod();
            return reservationManager.List.ConvertToDtos();
        }

        public bool InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoManager.InsertAuto(auto.ConvertToEntity());
            return true;
        }

        public bool InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            kundeManager.InsertKunde(kunde.ConvertToEntity());
            return true;
        }

        public bool InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            reservationManager.InsertReservation(reservation.ConvertToEntity());
            return true;
        }

        public bool IsAutoAvailable(AutoDto auto)
        {
            WriteActualMethod();



            return true;
        }

        public bool UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                autoManager.UpdateAuto(auto.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Auto> ex)
            {
                throw new FaultException<Auto>(ex.MergedEntity);
            }
            return true;
        }

        public bool UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                kundeManager.UpdateKunde(kunde.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Kunde> ex)
            {
                throw new FaultException<Kunde>(ex.MergedEntity);
            }
            return true;
        }

        public bool UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                reservationManager.UpdateReservation(reservation.ConvertToEntity());
            }
            catch (OptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException<Reservation>(ex.MergedEntity);
            }
            catch(InvalidDateRangeException ex)
            {
                throw new FaultException<Reservation>(reservation.ConvertToEntity());
            }
            catch (AutoUnavailableException ex)
            {
                throw new FaultException<Reservation>(reservation.ConvertToEntity());
            }
            return true;
        }
    }
}