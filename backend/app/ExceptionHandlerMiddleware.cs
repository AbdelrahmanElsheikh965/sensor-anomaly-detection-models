using System.Net;
using System.Text.Json;

namespace Early_warning
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext request)
        {
            try
            {
                await _next.Invoke(request);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

                request.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500;
                request.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                    new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message
                    , ex.StackTrace.ToString())
                    :
                    new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
                var JsonOption = new JsonSerializerOptions()
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var Json = JsonSerializer.Serialize(response, JsonOption);
                await request.Response.WriteAsync(Json);
            }

        }
    }
}
