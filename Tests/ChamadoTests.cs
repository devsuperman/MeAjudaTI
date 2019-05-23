using System;
using System.Linq;
using Dominio.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ChamadoTests
    {
        private Usuario usuario;
        private Usuario tecnico;
        private Chamado chamado;

        [TestInitialize]
        public void Init()
        {
            this.usuario = new Usuario("55555555555", "Mario");
            this.tecnico = new Usuario("66666666666", "Eduardo", Perfis.Tecnico);
            this.chamado = new Chamado(1, "chamado teste", usuario.CPF);                        
        }

        [TestMethod]
        public void SituacaoAguardandoAtendimentoAposSerCriado()
        {
            Assert.IsTrue(chamado.AguardandoAtendimento);            
            Assert.IsFalse(chamado.EmAtendimento);            
            Assert.IsFalse(chamado.Fechado);            
        }

        [TestMethod]        
        public void SituacaoEmAtendimentoAposSerAssumido()
        {
            tecnico.Assumir(chamado);

            Assert.IsFalse(chamado.AguardandoAtendimento);            
            Assert.IsTrue(chamado.EmAtendimento);            
            Assert.IsFalse(chamado.Fechado);                     
        }

        [TestMethod]        
        public void SituacaoFechadoAposSerFechadoPeloTecnico()
        {
            tecnico.Assumir(chamado);
            tecnico.Fechar(chamado, "Fechado com sucesso!");

            Assert.IsFalse(chamado.AguardandoAtendimento);            
            Assert.IsFalse(chamado.EmAtendimento);            
            Assert.IsTrue(chamado.Fechado);                     
        }

        [TestMethod]        
        public void SituacaoFechadoAposSerFechadoPeloUsuario()
        {            
            usuario.Fechar(chamado, "Fechado com sucesso!");

            Assert.IsFalse(chamado.AguardandoAtendimento);            
            Assert.IsFalse(chamado.EmAtendimento);            
            Assert.IsTrue(chamado.Fechado);                     
        }

       
    }
}
