
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Avaliacao
    {
        public Avaliacao(int pontuacao, string comentario)
        {
            Pontuacao = pontuacao;
            Comentario = comentario;
        }

        [Key]        
        public int ChamadoId { get; private set; }           
        public int Pontuacao { get; private set; }
        public System.DateTime DataDeRegistro { get; private set; } = System.DateTime.Now;
        
        [MaxLength(500)]
        public string Comentario { get; private set; }        
    }    
}