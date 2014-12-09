using SiegeCombat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiegeCombat.Controllers
{
    public class JuegoController : Controller
    {
        //
        // GET: /Juego/
        SiegeCombatEntities BD = new SiegeCombatEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Jugada(string coordenada, string[] arreglo)
        {
            try
            {
                Jugada jugada = new Jugada();
                foreach (string coor in arreglo)
                {
                    if (coordenada == coor)
                    {
                        Invitaciones invitacion = (Invitaciones)Session["Invitacion"];
                        Usuario usuario = (Usuario)Session["Usuario"];
                        Jugador jugador = (Jugador)Session["Jugador"];
                        Jugador oponente = (Jugador)Session["Oponente"];
                        Partida partida = (Partida)Session["Partida"];
                        jugada.Acerto = true;
                        jugada.IdJugador = jugador.IdJugador;
                        jugada.IdOponente = oponente.IdJugador;
                        jugada.IdPartida = partida.IdPartida;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(true);
        }

    }
}
