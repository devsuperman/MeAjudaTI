using System.ComponentModel.DataAnnotations;
using System;

namespace Dominio.Models
{
    public class Anexo
    {
        public Anexo(){}

        public Anexo(string fileName, string contentType)
        {
            this.Nome = fileName;
            this.Mime = contentType;
        }

        [Key]
        public string GUID { get; private set; } = Guid.NewGuid().ToString();     

        [Required]   
        public string Mime { get; private set; }

        [Required]
        public string Nome { get; private set; }                

        public void Atualizar(string nome, string mime)
        {
            this.Nome = nome;
            this.Mime = mime;
        }
    }
}