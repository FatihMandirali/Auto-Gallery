using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoGaleri.Utils
{
    public class BaseController:System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            //bool iscontroller = controllerName == "Arabalar" && actionName == "Index";
            //bool iscontroller1 = controllerName == "Arabalar" && actionName == "PersonelSatilanArabalar";
            //bool iscontroller2 = controllerName == "Arabalar" && actionName == "PersonelKiralananArabalar";
            bool iscontroller = (controllerName == "Yonetim" && actionName == "Index") ||
                //(controllerName == "Yonetim" && actionName == "Delete") ||
                //(controllerName == "Yonetim" && actionName == "Create") ||
                //(controllerName == "Yonetim" && actionName == "Details") ||
                //(controllerName == "Yonetim" && actionName == "Edit") ||
                (controllerName == "Arabalar" && actionName == "Index")||
                (controllerName == "Arabalar" && actionName == "PersonelSatilanArabalar")||
                (controllerName == "Arabalar" && actionName == "PersonelKiralananArabalar");
          

            //if (iscontroller && Session["loginp"]==null || Session["logink"] == null)
            //{
                if (Session["loginy"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/ErrorPage404/");
                    return;
                }

          //  }
            base.OnActionExecuting(filterContext);
        }
    }
}