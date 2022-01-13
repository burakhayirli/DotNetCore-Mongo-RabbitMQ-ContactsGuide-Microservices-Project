using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static ContactGuide.Shared.Extensions.ErrorDetails;

namespace ContactGuide.Shared.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path;
                Console.WriteLine(message);

                await _next(httpContext);
                watch.Stop();

                message = "[Response] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path + " responded " + httpContext.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                Console.WriteLine(message);
            }
            catch (Exception E)
            {
                watch.Stop();
                await HandleExceptionAsync(httpContext, E, watch);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e, Stopwatch watch)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _ = e.Message;
            string message;
            // = "[Error] HTTP " + httpContext.Request.Method + " - " + httpContext.Response.StatusCode + " - Error Message " + e.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";

            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = e.Message;
                errors = ((ValidationException)e).Errors;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());
            }
            else
            if (e.GetType() == typeof(TimeoutException))
            {
                message = "Timeout. Can not connect to the service: \n" + e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            if (e.GetType() == typeof(MongoConnectionException))
            {
                message = "Can not connect to MongoDb: \n" + e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            if (e.GetType() == typeof(ApplicationException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                message = e.Message + " \n " + e.StackTrace;
            }

            return httpContext.Response.WriteAsync(message);
        }
    }
}
