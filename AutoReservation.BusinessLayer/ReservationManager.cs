using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using System.Data.Entity.Core;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager : ManagerBase
    {
        // Alle Entities lesen
        public List<Reservation> List
        {
            get
            {
                using(var context = new AutoReservationContext())
                {
                    return context.Reservationen.ToList();
                }
            }
        }

        //Eine Entity anhand des Primaerschluessels lesen
        public Reservation GetReservationbyID(int id)
        {
            using(var context = new AutoReservationContext())
            {
                return context.Reservationen.SingleOrDefault(r => r.ReservationsNr == id);
            }
        }

        // Einfuegen
        public void InsertReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                CheckDateRange(reservation);
                AvailabilityCheck(context, reservation);
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        // Update
        public void UpdateReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                try
                {
                    CheckDateRange(reservation);
                    AvailabilityCheck(context, reservation);
                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        // loeschen
        public void DeleteReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void CheckDateRange(Reservation reservation)
        {
            if ((reservation.Bis - reservation.Von).TotalHours < 24)
            {
                throw new InvalidDateRangeException("reservation error in ReservationManager");
            }
        }

        private void AvailabilityCheck(AutoReservationContext context, Reservation reservation)
        {
            
        }

        public void AvailabilityCheck(Reservation reservation)
        {

        }
    }
}