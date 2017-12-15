using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class FaultInvalidDateRange
    {
        private string operation;
        [DataMember]
        public string Operation
        {
            get { return operation; }
            set
            {
                operation = value;
            }
        }
    }
}
