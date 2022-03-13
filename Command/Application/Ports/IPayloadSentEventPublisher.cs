using Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IPayloadSentEventPublisher
    {
        Task Publish(PayloadSent @event);
    }
}
