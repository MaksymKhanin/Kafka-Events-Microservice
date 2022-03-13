using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class KafkaConstants
    {
        public static class MessageHeaderNames
        {
            public const string EventType = "EventType";
            public const string CorrelationId = "CorrelationId";
        }
    }
}
