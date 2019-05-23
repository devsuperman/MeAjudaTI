using System;

namespace Dominio.Models
{
    public class AguardandoAtendimento : ISituacao
    {
        public string Legenda => "Aguardando Atendimento";
        public string Icone => "relogio";
        public string Emoticon => "triste";
        public string CorDoTexto => "perigo";
    }
}