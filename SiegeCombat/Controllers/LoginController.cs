using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiegeCombat.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar(string usuario, string password)
        {
            try
            {
                if (usuario == "Angel" && password == "Hola123")
                {
                    return Json(new { result = true, url = Url.Action("Index", "Lobby") });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { result = false, url = Url.Action("Index", "Lobby") });
        }
    }
}
