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

        public ActionResult Registrar(string contraseña,string nombre, string nickname, string correo)
        {
            try
            {
                Usuario usuario = new Usuario();
                Jugador jugador = new Jugador();
                usuario.Contraseña = contraseña;
                usuario.Correo = correo;
                usuario.Nombre = nombre;
                jugador.EXP = 0;
                jugador.Nickname = nickname;
                jugador.Mejora1 = false;
                jugador.Mejora2 = false;
                jugador.Mejora3 = false;
                jugador.Nivel = 1;
                jugador.Estatus = "DESCONECTADO";

                bd.Jugador.Add(jugador);
                bd.Usuario.Add(usuario);
                bd.SaveChanges();


            }
            catch (Exception ex)
            {
                
            }
            return Json(true);
        }


        public bool EnviaCorreo(string correo, string mensaje)
        {
            MailMessage _Correo = new MailMessage();
            _Correo.From = new MailAddress("syronleon@gmail.com");
            _Correo.To.Add(correo);
            _Correo.Subject = "Ultimo Movimiento Bancario Realizado";
            _Correo.Body = mensaje;
            _Correo.IsBodyHtml = false;
            _Correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("syronleon@gmail.com", "9920147d");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(_Correo);
                _Correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _Correo.Dispose();
                return false;
            }

        }
    }
}
