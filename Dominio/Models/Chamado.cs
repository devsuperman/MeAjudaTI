using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Dominio.Models
{
    public class Chamado
    {
        public Chamado(){}

        public Chamado(int tipoId, string descricao, string CPFdoSolicitante)
        {            
            TipoId = tipoId;
            Descricao = descricao;
            SolicitanteCPF = CPFdoSolicitante;
            AdicionarComentario("Chamado aberto.", CPFdoSolicitante);
        }


        public int Id { get; private set; }

        [MaxLength(1000)]
        public string Descricao { get; private set; }        
        public DateTime DataDeRegistro { get; private set; } = DateTime.Now;
        public DateTime? DataDeConclusao { get; private set; }
        
        public int TipoId { get; private set; }

        [MaxLength(11)]
        public string SolicitanteCPF { get; private set; }        

        [MaxLength(11)]
        public string TecnicoCPF { get; private set; }

        public TipoDeChamado Tipo { get; private set; }        
        
        [ForeignKey("TecnicoCPF")]
        [InverseProperty("ChamadosAtendidos")]
        public Usuario Tecnico { get; private set; }        

        [ForeignKey("SolicitanteCPF")]
        [InverseProperty("ChamadosCriados")]
        public Usuario Solicitante { get; private set; }

        public ICollection<Comentario> Comentarios { get; private set; } = new List<Comentario>();
        public ICollection<Anexo> Anexos { get; private set; } = new List<Anexo>();

        public Avaliacao Avaliacao { get; set; }

        public bool AguardandoAtendimento => string.IsNullOrEmpty(this.TecnicoCPF) && (!this.DataDeConclusao.HasValue);
        public bool EmAtendimento => (!string.IsNullOrEmpty(this.TecnicoCPF)) && (!this.DataDeConclusao.HasValue);
        public bool Fechado => (this.DataDeConclusao.HasValue);

        public ISituacao Situacao {
            get
            {
                ISituacao situacao = new AguardandoAtendimento();

                if (this.EmAtendimento)                
                    situacao = new EmAtendimento(this.Tecnico);   
                else if(this.Fechado)                
                    situacao = new Fechado(this.TempoParaExecucao); 
                    
                return situacao;
            }
        }             
        
        public TimeSpan TempoParaExecucao
        {
            get
            {
                var inicio = this.DataDeRegistro;
                var fim = (this.Fechado ? this.DataDeConclusao.Value : DateTime.Now);
                return (fim - inicio);
            }
        }

        public bool PodeRetornarParaCaixa(string CPFdoTecnico) => (this.TecnicoCPF == CPFdoTecnico) && (this.EmAtendimento);
        public bool PodeMoverParaCaixaDoTecnico() => (this.AguardandoAtendimento);   
        public bool PodeComentar(string cpf) => ((this.SolicitanteCPF == cpf) || (this.TecnicoCPF == cpf)) && (!this.Fechado);

        public bool PodeFechar(string cpf) => (this.AguardandoAtendimento && this.SolicitanteCPF == cpf) || (this.EmAtendimento && this.TecnicoCPF == cpf);

        public bool PodeAvaliar(string CPFdoUsuario)
        {
            var temAvaliacao = (this.Avaliacao != null);
            var euQueCrieiOchamado = (this.SolicitanteCPF == CPFdoUsuario);
            var ultimoComentario = this.Comentarios.OrderByDescending(a => a.DataDeRegistro).FirstOrDefault();
            var foiFechadoPeloProprioUsuario = (this.Fechado) && (ultimoComentario != null) && (this.SolicitanteCPF == ultimoComentario.UsuarioCPF);

            return 
                (this.Fechado) && 
                (!temAvaliacao) &&
                (euQueCrieiOchamado) &&
                (!foiFechadoPeloProprioUsuario);
        } 

        public void MoverParaCaixaDoTecnico(string CPFdoTecnico)
        {
            if (!this.PodeMoverParaCaixaDoTecnico())            
                throw new InvalidOperationException();                
            
            this.TecnicoCPF = CPFdoTecnico;
            this.AdicionarComentario("Help assumido pelo técnico.", CPFdoTecnico);
        }

        public void AdicionarComentario(string descricao, string cpf)
        {
            if (!this.PodeComentar(cpf))
                throw new InvalidOperationException();                  
            
            var comentario = new Comentario(descricao, cpf);
            this.Comentarios.Add(comentario);
        }

        public void Fechar(string solucao, string cpf)
        {
            if (!PodeFechar(cpf))            
                throw new InvalidOperationException();            

            if (string.IsNullOrWhiteSpace(solucao))
                throw new ArgumentNullException();                            
            
            var descricaoDoComentario = "Help fechado. " + solucao;
            this.AdicionarComentario(descricaoDoComentario, cpf);
            
            this.DataDeConclusao = DateTime.Now;

        }

        public void Avaliar(string cpf, int pontuacao, string comentario) 
        {            
            if (!this.PodeAvaliar(cpf))
                throw new InvalidOperationException();

            this.Avaliacao = new Avaliacao(pontuacao, comentario);
        }

        public void RetornarParaCaixa(string CPFdoTecnico)
        {            
            if (!PodeRetornarParaCaixa(CPFdoTecnico))            
                throw new InvalidOperationException("O chamado só pode retornar para a caixa se ainda estiver em execução.");
            
            this.AdicionarComentario("Help movido para caixa de entrada da equipe técnica.", CPFdoTecnico);

            this.TecnicoCPF = null;
        }
    

    }

}