using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiegeCombat.Models;

namespace SiegeCombat.Controllers
{
    public class LobbyController : Controller
    {
        //
        // GET: /Lobby/
        SiegeCombat.Models.SiegeCombatEntities bd = new SiegeCombatEntities();

        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            List<Jugador> jugadoresConectados = bd.Jugador.Where(j => j.Estatus == "CONECTADO" && j.IdJugador != usuario.IdJugador).ToList();
            ViewBag.jugadores = jugadoresConectados;
            return View();
            
        }

    }
}
