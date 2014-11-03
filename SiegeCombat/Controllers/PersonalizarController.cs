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


        public ActionResult CambiarImagen(string logo)
         {
             try
             {
                 Jugador jugador = (Jugador)Session["Jugador"];
                 jugador.Imagen = logo;

                 bd.Entry(jugador).State = System.Data.EntityState.Modified;
                 bd.SaveChanges();
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
                Jugador jugador = (Jugador)Session["Jugador"];
                bd.Entry(jugador).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
