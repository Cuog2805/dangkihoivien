using dangkihoivien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dangkihoivien.App_Start
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        private DangKiHoiVienDBContext dbContext = new DangKiHoiVienDBContext();
        private List<string> allowedRole = new List<string>();
        public CustomAuthorizeAttribute(string role)
        {
            this.allowedRole.Add(role);
        }
        public CustomAuthorizeAttribute(string[] role) 
        {
            this.allowedRole.AddRange(role);
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(HttpContext.Current.Session["adminSession"] == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            else
            {
                string adminSession = HttpContext.Current.Session["adminSession"].ToString();
                QuanTriVien qtvCurrent = dbContext.Admins.Include("Role").FirstOrDefault(q => q.Username == adminSession);
                if (!this.allowedRole.Contains(qtvCurrent.Role.Name))
                {
                    HandleUnauthorizedRequest(filterContext);
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { permission = false, messageError = "Bạn không có quyền thực hiện chức năng này!" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "AdminMenu",
                            action = "AdminError",
                            area = "Admin"
                        }
                    ));
            }
        }
    }
}