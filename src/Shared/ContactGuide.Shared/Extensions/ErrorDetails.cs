using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ContactGuide.Shared.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        public class ValidationErrorDetails : ErrorDetails
        {
            public IEnumerable<ValidationFailure> Errors { get; set; }
        }
    }
}
