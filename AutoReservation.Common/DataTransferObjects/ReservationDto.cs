using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto
    {
        private DateTime bis;
        [DataMember]
        public DateTime Bis
        {
            get { return bis; }
            set
            {
                bis = value;
            }
        }

        private int reservationsNr;
        [DataMember]
        public int ReservationsNr
        {
            get { return reservationsNr; }
            set
            {
                reservationsNr = value;
            }
        }

        private byte[] rowVersion;
        [DataMember]
        public byte[] RowVersion
        {
            get { return rowVersion; }
            set
            {
                rowVersion = value;
            }
        }

        private DateTime von;
        [DataMember]
        public DateTime Von
        {
            get { return von; }
            set
            {
                von = value;
            }
        }

        private AutoDto auto;
        [DataMember]
        public AutoDto Auto
        {
            get { return auto; }
            set
            {
                auto = value;
            }
        }

        private KundeDto kunde;
        [DataMember]
        public KundeDto Kunde
        {
            get { return kunde; }
            set
            {
                kunde = value;
            }
        }
        //public override string ToString()
        //    => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
    }
}
