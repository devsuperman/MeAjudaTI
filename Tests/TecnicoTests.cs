using System;
using System.Linq;
using Dominio.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TecnicoTests
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
        public void PodeAssumirChamadoCorretamente()
        {
            Assert.IsTrue(tecnico.PodeAssumir(chamado)); 
        }

        [TestMethod]
        public void NaoPodeAssumirChamadoSeNaoForTecnico()
        {            
            Assert.IsFalse(usuario.PodeAssumir(chamado));
        }

        [TestMethod]
        public void NaoPodeAssumirChamadoDoColeguinha()
        {
            tecnico.Assumir(chamado);             
            var outroTecnico = new Usuario("65478955664", "Marcelo", Perfis.Tecnico);
            Assert.IsFalse(outroTecnico.PodeAssumir(chamado));
        }

        [TestMethod]
        public void NaoPodeAssumirChamadoFechado()
        {
            usuario.Fechar(chamado, "Resolvi sozinho! hihihi");            
            Assert.IsFalse(tecnico.PodeAssumir(chamado));
        }

        [TestMethod]
        public void PodeComentarCorretamente()
        {
            tecnico.Assumir(chamado);
            Assert.IsTrue(tecnico.PodeComentar(chamado));
        }

        [TestMethod]
        public void NaoPodeComentarSeNaoAssumirChamado()
        {            
            Assert.IsFalse(tecnico.PodeComentar(chamado));
        }

        [TestMethod]
        public void NaoPodeComentarSeChamadoJaFoiFechado()
        {            
            tecnico.Assumir(chamado);
            tecnico.Fechar(chamado, "resolvido!");
            Assert.IsFalse(tecnico.PodeComentar(chamado));
        }

        [TestMethod]
        public void PodeFecharChamadoCorretamente()
        {
            tecnico.Assumir(chamado);
            Assert.IsTrue(tecnico.PodeFechar(chamado));
        }

        [TestMethod]
        public void NaoPodeFecharChamadoSeNaoTiverAssumido()
        {   
            Assert.IsFalse(tecnico.PodeFechar(chamado));
        }

        [TestMethod]
        public void NaoPodeFecharChamadoSeOutroTecnicoAssumiu()
        {   
            var outroTecnico = new Usuario("65478955664", "Marcelo", Perfis.Tecnico);
            outroTecnico.Assumir(chamado);
            
            Assert.IsFalse(tecnico.PodeFechar(chamado));
        }

        [TestMethod]       
        public void NaoPodeFecharChamadoSeJaEstiverFechado()
        {            
            usuario.Fechar(chamado, "liguei na tomada e resolveu");            
            Assert.IsFalse(tecnico.PodeFechar(chamado));                        
        }           
       
    }
}
