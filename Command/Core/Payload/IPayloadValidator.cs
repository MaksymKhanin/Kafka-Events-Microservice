using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payload
{

    public interface IPayloadValidator
    {
       PayloadValidationResult ValidatePayloadData(Stream payload);
    }
}
