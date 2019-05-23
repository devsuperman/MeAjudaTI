using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models
{
    public class Comentario
    {
        public Comentario() { }

        public Comentario(string descricao, string cpf)
        {
            Descricao = descricao;
            UsuarioCPF = cpf;
        }

        public int Id { get; private set; }    

        [MaxLength(500)]            
        public string Descricao { get; private set; }
        public DateTime DataDeRegistro { get; private set; } = DateTime.Now;
        
        [MaxLength(11)]
        public string UsuarioCPF { get; private set; }                
        
        public Usuario Usuario { get; private set; }                

        public string Autor => (string.IsNullOrEmpty(this.UsuarioCPF) ? "MeAjudaTI" : this.Usuario.Nome);
    }
}