using SiegeCombat.Models;
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
        SiegeCombatEntities bd = new SiegeCombatEntities();
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            Jugador jugador = bd.Jugador.Find(usuario.IdJugador);
            return View(jugador);
        }

        public ActionResult ComprarMejora(int mejora)
        {
            bool valido = false;
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = bd.Jugador.Find(usuario.IdJugador);
                if (mejora == 1)
                    jugador.Mejora1 = true;
                else if (mejora == 2)
                    jugador.Mejora2 = true;
                else if (mejora == 3)
                    jugador.Mejora3 = true;
                bd.Entry(jugador).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                valido = true;
            }
            catch (Exception ex)
            {
               
            }
            return Json(new { result = valido, url = "" });
        }
    }
}
