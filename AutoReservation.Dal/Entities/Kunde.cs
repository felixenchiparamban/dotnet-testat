using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunde")]
  public class Kunde
    {
        public DateTime Geburtsdatum { get; set; }

        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Nachname { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [MaxLength(20)]
        public string Vorname { get; set; }

        //Navigation Proprety
        public ICollection<Reservation> Reservationen { get; set; }
    }
}
