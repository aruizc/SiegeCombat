//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiegeCombat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Jugador
    {
        public Jugador()
        {
            this.Partida = new HashSet<Partida>();
            this.Partida1 = new HashSet<Partida>();
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int IdJugador { get; set; }
        public int EXP { get; set; }
        public Nullable<bool> Mejora1 { get; set; }
        public Nullable<bool> Mejora2 { get; set; }
        public Nullable<bool> Mejora3 { get; set; }
        public Nullable<int> Nivel { get; set; }
        public string Estatus { get; set; }
        public string Nickname { get; set; }
    
        public virtual ICollection<Partida> Partida { get; set; }
        public virtual ICollection<Partida> Partida1 { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
