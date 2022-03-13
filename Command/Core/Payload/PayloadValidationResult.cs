using Api.Errors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payload
{
    public record PayloadValidationResult(PayloadValidationError[] ValidationErrors)
    {
        public bool IsValid => !ValidationErrors.Any();
    }
}
