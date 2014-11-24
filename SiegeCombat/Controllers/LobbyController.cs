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
            List<Jugador> jugadoresConectados = bd.Jugador.Where(j => (j.Estatus == "CONECTADO" || j.Estatus == "JUGANDO") && j.IdJugador != usuario.IdJugador).ToList();
            ViewBag.jugadores = jugadoresConectados;
            Jugador jugador = bd.Jugador.Find(usuario.IdJugador);
            List<Jugador> topTen = bd.Jugador.OrderByDescending(j => j.EXP).Take(10).ToList();
            ViewBag.top = topTen;
            return View(jugador);
            
        }

        public ActionResult Cargar() 
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            List<Jugador> jugadoresConectados = bd.Jugador.Where(j => j.Estatus == "CONECTADO" || j.Estatus == "JUGANDO" && j.IdJugador != usuario.IdJugador).ToList();
            return Json(jugadoresConectados);
        }

        public ActionResult CerrarSesion()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = bd.Jugador.Find(usuario.IdJugador);
                jugador.Estatus = "DESCONECTADO";
                bd.Entry(jugador).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                Session["Usuario"] = null;
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { result = true, url = Url.Action("Index", "Login") });
        }

        public ActionResult ObtenerInvitaciones()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                List<Invitaciones> invitacion = null;
                invitacion = bd.Invitaciones.Where(i => i.IdOponente == usuario.IdJugador && i.Estatus != 0).ToList();
                Session["Invitacion"] = invitacion;
                bool valido = false;
                foreach (Invitaciones i in invitacion)
                {
                    if (i.Estatus == 1)
                    {
                        DateTime ahora = DateTime.Now;
                        TimeSpan tiempo = ahora - (DateTime)i.Fecha;
                        int diferencia = tiempo.Minutes;
                        if(diferencia <= 3)
                            return Json(new { result = true, url = Url.Action("Index", "Juego"), id = i.IdInvitaciones });
                    }                        
                }
                return Json(new { result = valido, url = Url.Action("Index", "Juego") });
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
        }

        public ActionResult HacerInvitacion(int oponente) 
        {
            try
            {
                Invitaciones invitacion = new Invitaciones();
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = bd.Jugador.Find(usuario.IdJugador);

                invitacion.Estatus = 1;
                invitacion.Fecha = DateTime.Now;
                invitacion.IdHost = jugador.IdJugador;
                invitacion.IdOponente = oponente;

                bd.Invitaciones.Add(invitacion);
                bd.SaveChanges();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public ActionResult Jugar()
        {
            Invitaciones invitacion = (Invitaciones)Session["Invitacion"];
            Usuario usuario = (Usuario)Session["Usuario"];
            Jugador jugador = (Jugador)Session["Jugador"];
            jugador.Estatus = "JUGANDO";

            Partida partida = new Partida();
            partida.Fecha = DateTime.Now;
            partida.IdJugadorUno = jugador.IdJugador;
            partida.IdJugadorDos = (int)invitacion.IdHost;

            bd.Partida.Add(partida);
            bd.Entry(jugador).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            return Json(new { result = true, url = Url.Action("Index", "Juego") });
        }

        public ActionResult Cancelar() {
            try
            {
                List<Invitaciones> lstInvitacion = (List<Invitaciones>)Session["Invitacion"];
                Invitaciones invitacion = bd.Invitaciones.Find(lstInvitacion[0].IdInvitaciones);
                invitacion.Estatus = 0;
                bd.Entry(invitacion).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(new { result = true });
        }
    }
}
