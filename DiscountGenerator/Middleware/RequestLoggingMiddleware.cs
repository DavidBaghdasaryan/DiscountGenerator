namespace DiscountGenerator.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalRequestBody = context.Request.Body;
            try
            {
                using (var requestBodyStream = new MemoryStream())
                {
                    await originalRequestBody.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
                    _logger.LogInformation($"Received request: {context.Request.Method} {context.Request.Path}");
                    _logger.LogInformation($"Request body: {requestBodyText}");

                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = requestBodyStream;

                    using (var responseBodyStream = new MemoryStream())
                    {
                        var originalResponseBody = context.Response.Body;
                        try
                        {
                            context.Response.Body = responseBodyStream;

                            await _next(context);

                            responseBodyStream.Seek(0, SeekOrigin.Begin);
                            var responseBodyText = new StreamReader(responseBodyStream).ReadToEnd();
                            _logger.LogInformation($"Response body: {responseBodyText}");

                            responseBodyStream.Seek(0, SeekOrigin.Begin);
                            await responseBodyStream.CopyToAsync(originalResponseBody);
                        }
                        finally
                        {
                            context.Response.Body = originalResponseBody;
                        }
                    }
                }
            }
            finally
            {
                context.Request.Body = originalRequestBody;
            }
        }
    }
}
