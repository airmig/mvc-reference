namespace MVCReferenceProject;
public class MyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory)
    {
        _next = next;
        _logger = logFactory.CreateLogger("MyMiddleware");
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        //await httpContext.Response.Body.WriteAsync("Middleware executed before the request\n");
        _logger.LogInformation("Executing middleware"); 
        await _next(httpContext);
        
        
        // Logic to execute after the request has been handled
        await httpContext.Response.WriteAsync("\n<center>Middleware executed after the response</center>");

    }
}

public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyMiddleware>();
    }
}