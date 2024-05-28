using Microsoft.AspNetCore.Mvc.Filters;
using backend.Entities;

namespace backend.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(string controller = null) : Attribute, IAuthorizationFilter
{
    private string _controller = controller;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var account = (Account)context.HttpContext.Items["Account"];

        if (account == null)
        {
            Console.WriteLine("Account context not found");
            // not logged in or role not authorized
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<DataContext>();

        if (_controller != null)
        {
            var menu = dbContext.NavigationMenus.Where(x => x.ControllerName == _controller).FirstOrDefault();

            // If controller name is found on menu data, then check the permission for current controller
            if (menu != null)
            {
                try
                {
                    var permission = dbContext.RolePermissions.Where(x => x.RoleId == account.RoleId && x.NavigationMenuId == menu.Id && x.Permitted == true).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw new UnauthorizedAccessException("Unauthorized");
                }
            }
        }
    }
}