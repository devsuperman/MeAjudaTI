using System;
using Dominio.Extensions;

namespace Dominio.Models
{
    public class Fechado : ISituacao
    {
        public Fechado(string legenda) => this.Legenda = legenda;                
        public Fechado(TimeSpan tempoParaExecucao) => this.Legenda = $"Atendido em {tempoParaExecucao.ToShortTimeString()}";

        public string Legenda { get; }
        public string Icone => "concluido";
        public string Emoticon => "feliz";
        public string CorDoTexto => "secundaria";
    }
}