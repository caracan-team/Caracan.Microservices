using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NextCloud.Lib.Exceptions;

namespace MediaService.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Instance = context.Request.Path
                };
                
                HandleException(ref problemDetails, exception);

                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = problemDetails.Status ?? 500;
                
                await response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }
        }

        protected virtual void HandleException(ref ProblemDetails problemDetails, Exception exception)
        {
            switch (exception)
            {
                case NotImplementedException notImplementedException:
                    problemDetails.Title = "Not implemented exception occured!";
                    problemDetails.Status = StatusCodes.Status501NotImplemented;
                    problemDetails.Detail = notImplementedException.Message;
                    break;
                
                case NextCloudException nextCloudException:
                    problemDetails.Title = "NextCloud exception occured!";
                    problemDetails.Status = StatusCodes.Status503ServiceUnavailable;
                    problemDetails.Detail = nextCloudException.Message;
                    break;
                
                case ArgumentException argumentException:
                    problemDetails.Title = "Argument exception occured!";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = argumentException.Message;
                    break;
                
                case FileNotFoundException fileNotFoundException:
                    problemDetails.Title = "File not found exception occured!";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = fileNotFoundException.Message;
                    HandleException(ref problemDetails, fileNotFoundException);
                    break;

                 default:
                    problemDetails.Title = "An unexpected error occurred!";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.ToString();
                    break;
            }
         }
    }
}
