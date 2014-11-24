using SiegeCombat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiegeCombat.Controllers
{
    public class PersonalizarController : Controller
    {
        //
        // GET: /Personalizar/
        SiegeCombat.Models.SiegeCombatEntities bd = new Models.SiegeCombatEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CambiarImagen(string imagen)
         {
             try
             {
                 Jugador temp = (Jugador)Session["Jugador"];
                 Jugador jugador = bd.Jugador.Find(temp.IdJugador);
                 jugador.Imagen = imagen + ".png";
                 bd.Entry(jugador).State = System.Data.EntityState.Modified;
                 bd.SaveChanges();
                 return Json(true);
             }
             catch (Exception ex)
             {
              return null;
             }
         }

        public ActionResult CambiarNickname(string nickname)
        {
            try
            {
                Jugador temp = (Jugador)Session["Jugador"];
                Jugador jugador = bd.Jugador.Find(temp.IdJugador);
                jugador.Nickname = nickname;
                bd.Entry(jugador).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}
