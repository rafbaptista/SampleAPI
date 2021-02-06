using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Enums;
using UserAPI.Domain.Extensions;
using UserAPI.Domain.ViewModels;

namespace UserAPI.Services.WebApi.Middlewares
{
    public class ResponseFormatterMiddleware
    {
        private readonly RequestDelegate _next;
        private int[] _statusCodesHandled = new int[] { StatusCodes.Status401Unauthorized, StatusCodes.Status404NotFound, StatusCodes.Status403Forbidden, StatusCodes.Status415UnsupportedMediaType };

        public ResponseFormatterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //invoke next middleware
            await _next.Invoke(context);

            var request = context.Request;
            var response = context.Response;

            var controllerActionDescriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();

            //get required auth for the requested resource
            var authorizeAttribute = controllerActionDescriptor?.MethodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault() as AuthorizeAttribute;

            string authorizedRoles = authorizeAttribute?.Roles;
            string authorizePolicies = authorizeAttribute?.Policy;

            //statusCode of the resource request
            int statusCode = response.StatusCode;

            //writes custom response
            if (_statusCodesHandled.Contains(statusCode))
                await context.Response.WriteAsJsonAsync(GetCustomResponse(statusCode, authorizedRoles, authorizePolicies));
        }

        private string GetEnumMessageError(int statusCode)
        {
            EHttpStatusCodes status = (EHttpStatusCodes)statusCode;
            return status.GetMessageError();
        }
        private CustomResponseModel GetCustomResponse(int statusCode, string authorizedRoles, string authorizePolicies)
        {
            return new CustomResponseModel(statusCode, GetEnumMessageError(statusCode), authorizedRoles, authorizePolicies);
        }
    }
}
