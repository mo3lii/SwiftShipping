using E_CommerceAPI.Errors;
using System.Net;
using System.Text.Json;

namespace E_CommerceAPI.Middleware
{
    public class ExceptionMeddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMeddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMeddleware(
            RequestDelegate next, ILogger<ExceptionMeddleware> logger, IHostEnvironment environment)
        {
            _next = next;
           _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                      ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options =  new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
