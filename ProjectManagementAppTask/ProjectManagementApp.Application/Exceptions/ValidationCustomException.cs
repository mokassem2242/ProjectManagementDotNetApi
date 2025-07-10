using System;
using System.Collections.Generic;
using FluentValidation.Results;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Exceptions
{
    public class ValidationCustomException : Exception
    {
        public List<string> ValdationErrors { get; set; }

        public ValidationCustomException(ValidationResult validationResult)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValdationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
