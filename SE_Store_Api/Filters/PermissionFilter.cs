using Google.Apis.Storage.v1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SE_Store_Dto.Enum;
using System.Security.Claims;

namespace SE_Store_Api.Filters
{
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly FuncionalidadEnum[] _funcionalidad;

        public PermissionFilter(FuncionalidadEnum[] funcionalidad)
        {
            _funcionalidad = funcionalidad;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            bool isAuthorized = CheckUserPermission(user);
            if (isAuthorized)
            {
                return;
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user)
        {
            var permissionsString = user.FindFirst("Funcionalidades")?.Value;

            if (string.IsNullOrEmpty(permissionsString))
            {
                return false;
            }

            var permissions = JsonConvert.DeserializeObject<List<long>>(permissionsString);

            // Now check the hierarchical structure for the specific permission action
            return HasRequiredPermission(permissions);
        }

        private bool HasRequiredPermission(List<long> permissions)
        {
            foreach (var item in this._funcionalidad)
            {
                if (permissions.Any(action => action == (long)item))
                {
                    return true;
                }
            }            
            return false;
        }
    }
}
