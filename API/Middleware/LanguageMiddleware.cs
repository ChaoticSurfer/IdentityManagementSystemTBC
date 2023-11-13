namespace API.Middleware;

using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];

        var culture = !string.IsNullOrEmpty(cultureQuery)
            ? new CultureInfo(cultureQuery)
            : new CultureInfo(Constants.DefaultLanguage);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}