using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiegeCombat.Controllers
{
    public class TiendaController : Controller
    {
        //
        // GET: /Tienda/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComprarMejora(int mejora)
        {
            bool valido = false;
            try
            {

            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
    }
}
