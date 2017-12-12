using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto
    {
        private DateTime geburtsdatum;
        [DataMember]
        public DateTime Geburtsdatum
        {
            get { return geburtsdatum; }
            set
            {
                geburtsdatum = value;
            }
        }

        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                Id = value;
            }
        }
        private string nachname;
        [DataMember]
        public string Nachname
        {
            get { return nachname; }
            set
            {
                nachname = value;
            }
        }

        private int rowVersion;
        [DataMember]
        public int RowVersion
        {
            get { return rowVersion; }
            set
            {
                rowVersion = value;
            }
        }

        private string vorname;
        [DataMember]
        public string Vorname
        {
            get { return vorname; }
            set
            {
                vorname = value;
            }
        }
        //public override string ToString()
        //    => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}
