using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware
{

  public class ExceptionMiddleware
  {

    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {

      this._env = env;
      this._logger = logger;
      this._next = next;

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
        context.Response.StatusCode = 500;

        ProblemDetails response = new ProblemDetails
        {
          Status = 500,
          Detail = this._env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
          Title = ex.Message
        };

        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        String json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);

      }
    }

  }

}