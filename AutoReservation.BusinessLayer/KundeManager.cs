using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using System.Data.Entity.Core;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager : ManagerBase
    {
        // Alle Entities lesen
        public List<Kunde> List
        {
            get
            {
                using(var context = new AutoReservationContext())
                {
                    return context.Kunden.ToList();
                }
            }
        }

        //Eine Entity anhand des Primaerschluessels lesen
        public Kunde GetKundeById(int id)
        {
            var result = from l in List
                         where l.Id == id
                         select l;
            return result as Kunde;
        }

        // Einfuegen
        public void InsertKunde(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        // Update
        public void UpdateKunde(Kunde kunde)
        {
            using(var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(kunde).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw new OptimisticConcurrencyException();
                }
            }
        }

        //loeschen
        public void DeleteKunde(Kunde kunde)
        {
            using(var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}