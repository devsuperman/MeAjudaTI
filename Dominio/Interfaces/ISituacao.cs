namespace Dominio.Models
{
    public interface ISituacao
    {
        string Legenda { get; }
        string Icone { get; }
        string Emoticon { get; }
        string CorDoTexto { get; }
    }
}