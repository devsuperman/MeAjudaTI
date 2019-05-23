using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class TipoDeChamado
    {
        public int Id { get; set; }

        [Required, Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;
    }
}