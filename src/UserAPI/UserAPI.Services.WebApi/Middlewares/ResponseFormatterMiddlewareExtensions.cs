using Microsoft.AspNetCore.Builder;

namespace UserAPI.Services.WebApi.Middlewares
{
    public static class ResponseFormatterMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomResponseFormatter(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ResponseFormatterMiddleware>();
        }
    }
}
