using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using todosproject.Entities.Enums;

namespace todosproject.Helpers
{
    public class RoleAuth : Attribute, IAuthorizationFilter
    {
        private readonly UserRole[] _allowedRoles;

        public RoleAuth(params UserRole[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role == null) 
            {
                context.Result = new ForbidResult();
            }
            var userRole = (UserRole)int.Parse(role.Value);
            if (!_allowedRoles.Contains(userRole)) 
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
