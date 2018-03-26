using AlperAslanApps.Core.Exceptions;
using AlperAslanApps.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AlperAslanApps.AspNetCore.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AggregateValidationException aggregateValidationException)
            {
                var jsonException = FormatException(aggregateValidationException);

                context.ExceptionHandled = true;
                context.Result = new ObjectResult(jsonException)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (context.Exception is ValidationException validationException)
            {
                var message = "The model passed to the server is not valid. " +
                              "Please make sure to validate the model before passing it to the server. " +
                              $"The validation message is: {validationException.ValidationResult.ErrorMessage}";

                context.ExceptionHandled = true;
                context.Result = new ObjectResult(message)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        private static IEnumerable<JsonException> FormatException(AggregateValidationException exception) =>
            exception.InnerExceptions.Select(e =>
            {
                var validation = (ValidationObject)e.Value;
                return new JsonException
                {
                    ValidationMessage = validation.Message,
                };
            });

        private struct JsonException
        {
            public string ValidationMessage { get; set; }
        }
    }
}
