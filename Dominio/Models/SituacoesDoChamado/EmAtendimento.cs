using System;

namespace Dominio.Models
{
    public class EmAtendimento : ISituacao
    {
        public EmAtendimento(Usuario tecnico) => this.Legenda = $"Técnico(a) {tecnico.Nome}";

        public string Legenda { get; }
        public string Icone => "ninja";
        public string Emoticon => "indiferente";
        public string CorDoTexto => "secundaria";
    }
}