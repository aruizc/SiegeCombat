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
        SiegeCombatEntities bd = new SiegeCombatEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Jugada(string coordenada)
        {
            Jugada jugada = new Jugada();  
            try
            {
                
                Invitaciones invitacion = (Invitaciones)Session["Invitacion"];
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = (Jugador)Session["Jugador"];
                Jugador oponente = (Jugador)Session["Oponente"];
                Partida partida = (Partida)Session["Partida"];
                List<Casillas> casillas = bd.Casillas.Where(c => c.IdPartida == partida.IdPartida && c.IdJugador == oponente.IdJugador).ToList();
                jugada.Acerto = false;
                foreach (Casillas ca in casillas)
                {
                    if(ca.Coordenada == coordenada)
                        jugada.Acerto = true;
                }
                jugada.IdJugador = jugador.IdJugador;
                jugada.IdOponente = oponente.IdJugador;
                jugada.IdPartida = partida.IdPartida;
                bd.Jugada.Add(jugada);
                bd.SaveChanges();
                Hubs.HubJuego.Jugada(jugada.IdJugada);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(jugada.Acerto);
        }

        public ActionResult SubirCoordenadas(string coordenadas)
        {
            bool esvalido = false;
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = (Jugador)Session["Jugador"];
                Partida partida = (Partida)Session["Partida"]; 

                string[] coor  = coordenadas.Split('|');
                for (int i = 0; i < coor.Length - 1; i++)                     
	            {
                    Casillas casillas = new Casillas();
                    casillas.IdPartida = partida.IdPartida;
                    casillas.IdJugador = jugador.IdJugador;
                    casillas.Coordenada = coor[i];
                    bd.Casillas.Add(casillas);
                }
                bd.SaveChanges();
                esvalido = true;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return Json(esvalido);
        }

        public ActionResult AcabarPartida()
        {
            try
            {
                Jugador jugador = (Jugador)Session["Jugador"];
                Jugador oponente = (Jugador)Session["Oponente"];
                Partida partida = (Partida)Session["Partida"];
                partida = bd.Partida.Find(partida.IdPartida);
                partida.IdGanador = jugador.IdJugador;
                bd.Entry(partida).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(true);
        }

        public ActionResult ObtenerJugada(int idJugada)
        {
            bool esValido = false;
            try
            {
                Jugada jugada = bd.Jugada.Find(idJugada);
                Jugador jugador = (Jugador)Session["Jugador"];
                if (jugada.IdOponente == jugador.IdJugador)
                    esValido = true;
            }
            catch (Exception)
            {
                Json(false);
            }
            return Json(esValido);
        }
    }
}
