using Avro;
using Avro.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PayloadSentEvent : ISpecificRecord
    {
        public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"PayloadSentEvent\",\"namespace\":\"Common\",\"fields\":[{\"name\":\"ticketId\",\"doc\":\"the Id representing the upload of a single Payload\",\"type\":{\"type\":\"string\",\"logicalType\":\"uuid\"}}]}");

        private Guid _ticketId;

        public virtual Schema Schema => _SCHEMA;

        public Guid ticketId
        {
            get
            {
                return _ticketId;
            }
            set
            {
                _ticketId = value;
            }
        }

        public virtual object Get(int fieldPos)
        {
            if (fieldPos == 0)
            {
                return ticketId;
            }

            throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
        }

        public virtual void Put(int fieldPos, object fieldValue)
        {
            if (fieldPos == 0)
            {
                ticketId = (Guid)fieldValue;
                return;
            }

            throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
        }
    }
}
