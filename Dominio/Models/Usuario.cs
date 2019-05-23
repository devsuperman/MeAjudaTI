using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Usuario
    {
        public Usuario(){}
        
        public Usuario(string cpf, string nome, string perfil = Perfis.Usuario)
        {
            this.CPF = cpf;
            this.Nome = AbreviarNome(nome);
            this.Perfil = perfil;
        }
        

        [Key, MaxLength(11)]
        public string CPF { get; set; }        

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, MaxLength(100)]
        public string Perfil { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }        

        [MaxLength(100)]
        public string Departamento { get; set; }

        public ICollection<Chamado> ChamadosAtendidos {get; set; } = new List<Chamado>();

        public ICollection<Chamado> ChamadosCriados {get; set; } = new List<Chamado>();
        
        public bool PodeComentar(Chamado chamado) => chamado.PodeComentar(this.CPF);
        public bool PodeDevolverParaCaixaDeEntrada(Chamado chamado) => (this.Perfil == Perfis.Tecnico) && chamado.PodeRetornarParaCaixa(this.CPF);
        public bool PodeAvaliar(Chamado chamado) => chamado.PodeAvaliar(this.CPF);
        public bool PodeAssumir(Chamado chamado) => (chamado.PodeMoverParaCaixaDoTecnico()) && (this.Perfil is Perfis.Tecnico);
        public bool PodeFechar(Chamado chamado) => chamado.PodeFechar(this.CPF);

        public void Comentar(Chamado chamado, string descricao) => chamado.AdicionarComentario(descricao: descricao, cpf: this.CPF);

        public void Avaliar(Chamado chamado, int pontuacao, string comentario) => chamado.Avaliar(this.CPF, pontuacao, comentario);

        public void Atualizar(string departamento, string telefone)
        {
            this.Departamento = departamento;
            this.Telefone = telefone;
        }

        public void Assumir(Chamado chamado) => chamado.MoverParaCaixaDoTecnico(this.CPF);

        public void Fechar(Chamado chamado, string comentario) => chamado.Fechar(comentario, this.CPF);

        public void DevolverChamadoParaCaixa(Chamado chamado) => chamado.RetornarParaCaixa(this.CPF);

        private string AbreviarNome(string nomeCompleto)
        {
            var array = nomeCompleto.Split(' ');
            var primeiroNome = array[0];
            var nomeAbreviado = primeiroNome;
            
            if (array.Length > 1)            {
                var ultimoNome = array[array.Length-1];                            
                nomeAbreviado += " " + ultimoNome;
            }

            return nomeAbreviado;
        }
    }
}