using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoGaleri.Utils
{
    public class CarController : System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
           
            bool iscontroller = /*(controllerName == "Yonetim" && actionName == "Index") ||*/
                //(controllerName == "Yonetim" && actionName == "Delete") ||
                //(controllerName == "Yonetim" && actionName == "Create") ||
                //(controllerName == "Yonetim" && actionName == "Details") ||
                //(controllerName == "Yonetim" && actionName == "Edit") ||
                (controllerName == "Arabalar" && actionName == "Index") ||
                (controllerName == "Arabalar" && actionName == "PersonelSatilanArabalar") ||
                (controllerName == "Pesonel" && actionName == "Index") ||
                (controllerName == "Pesonel" && actionName == "Create") ||
                (controllerName == "Pesonel" && actionName == "Details") ||
                (controllerName == "Arabalar" && actionName == "PersonelKiralananArabalar");

            bool iscontroller1 = /*(controllerName == "Yonetim" && actionName == "Index") ||*/
                                 //(controllerName == "Yonetim" && actionName == "Delete") ||
                                 //(controllerName == "Yonetim" && actionName == "Create") ||
                                 //(controllerName == "Yonetim" && actionName == "Details") ||
              (controllerName == "Arabalar" && actionName == "BegenilenAraba") ||
              (controllerName == "Arabalar" && actionName == "KullaniciSifir") ||
              (controllerName == "Arabalar" && actionName == "KullaniciKiraladigim") ||
               (controllerName == "Home" && actionName == "MesajKutusuK") ||
              (controllerName == "Arabalar" && actionName == "KullaniciIkinciEl");

            if (Session["loginy"] == null)
            {
                if (!iscontroller)
                {
                    if (iscontroller1 && Session["logink"] != null)
                    {

                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Home/ErrorPage404/");
                        return;
                    }
                }
                else
                {
                    if (Session["loginp"] == null)
                    {
                        filterContext.Result = new RedirectResult("~/Home/ErrorPage404/");
                        return;
                    }
                }
            }

         
            base.OnActionExecuting(filterContext);
        }
    }
}