using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExclelToSql.Filters
{
    public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var username = context.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    message = "Please login first."
                });
            }
        }
    }
}