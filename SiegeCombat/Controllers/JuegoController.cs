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

        public ActionResult Jugada(string coordenada,int turno)
        {
            Jugada jugada = new Jugada();  
            string usu = "";
            try
            {
                
                Invitaciones invitacion = (Invitaciones)Session["Invitacion"];
                Usuario usuario = (Usuario)Session["Usuario"];
                Jugador jugador = (Jugador)Session["Jugador"];
                Jugador oponente = (Jugador)Session["Oponente"];
                Partida partida = (Partida)Session["Partida"];
                int id = 0;
                if (partida.IdJugadorUno == jugador.IdJugador)
                    id = partida.IdJugadorDos;
                else
                    id = partida.IdJugadorUno;
                usu = jugador.Nickname;
                List<Casillas> casillas = bd.Casillas.Where(c => c.IdPartida == partida.IdPartida && c.IdJugador == id).ToList();
                jugada.Acerto = false;
                foreach (Casillas ca in casillas)
                {
                    if(ca.Coordenada == coordenada)
                        jugada.Acerto = true;
                }
                jugada.IdJugador = jugador.IdJugador;
                jugada.IdOponente = oponente.IdJugador;
                jugada.IdPartida = partida.IdPartida;
                jugada.Turno = turno;
                jugada.Coordenada = coordenada;
                bd.Jugada.Add(jugada);
                bd.SaveChanges();
                Hubs.HubJuego.Jugada(jugada.IdJugada);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { result = jugada.Acerto,usuario = usu , acerto = jugada.Acerto, turno = jugada.Turno, coor = jugada.Coordenada });
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
                partida.Estatus = 1;
                bd.Entry(partida).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(new { result = true, url = Url.Action("Index", "Lobby") });
        }

        public ActionResult ObtenerJugada(int idJugada)
        {
            bool esValido = false;
            Jugada jugada = bd.Jugada.Find(idJugada);
            try
            {
                
                Jugador jugador = (Jugador)Session["Jugador"];
                if (jugada.IdOponente == jugador.IdJugador)
                {
                    esValido = true;
                    
                }
            }
            catch (Exception)
            {
                Json(false);
            }
            return Json(new { result = true, acerto = jugada.Acerto, turno = jugada.Turno, coor = jugada.Coordenada });
        }
    }
}
