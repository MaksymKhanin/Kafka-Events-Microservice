using Api.Errors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Core.Payload
{
    internal class PayloadValidator : IPayloadValidator
    {
        public PayloadValidationResult ValidatePayloadData(Stream payload)
        {
            try
            {
                _ = XDocument.Load(payload);
            }
            catch (XmlException e)
            {
                return new PayloadValidationResult(new[]
                {
                    new PayloadValidationError(PayloadErrorMessages.InvalidFormat, e)
                });
            }

            return new PayloadValidationResult(Array.Empty<PayloadValidationError>());
        }
    }
}
