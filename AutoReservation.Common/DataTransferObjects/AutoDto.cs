using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto
    {
        private int basistarif;
        [DataMember]
        public int Basistarif
        {
            get { return basistarif; }
            set
            {
                basistarif = value;
            }
        }

        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        private string marke;
        [DataMember]
        public string Marke
        {
            get { return marke; }
            set
            {
                marke = value;
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

        private int tagestarif;
        [DataMember]
        public int Tagestarif
        {
            get { return tagestarif; }
            set
            {
                tagestarif = value;
            }
        }

        private AutoKlasse autoKlasse;
        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get { return autoKlasse; }
            set
            {
                autoKlasse = value;
            }
        }
        //public override string ToString()
        //    => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
    }
}
