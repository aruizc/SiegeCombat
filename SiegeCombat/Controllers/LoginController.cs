using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using SiegeCombat.Models;

namespace SiegeCombat.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        SiegeCombat.Models.SiegeCombatEntities bd = new Models.SiegeCombatEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar(string usuario, string password)
        {
            try
            {
                Usuario user = null;
                user = bd.Usuario.Where(u => u.Nombre == usuario && u.Contraseña == password).First();
                if (user != null)
                {
                    Jugador jugador = bd.Jugador.Find(user.IdJugador);
                    jugador.Estatus = "CONECTADO";
                    bd.Entry(jugador).State = System.Data.EntityState.Modified;
                    bd.SaveChanges();
                    Session["Usuario"] = user;
                    return Json(new { result = true, url = Url.Action("Index", "Lobby") });
                }
                else
                    return Json(new { result = false, url = "" });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, url = "" });
            }
        }
        
    }
}
