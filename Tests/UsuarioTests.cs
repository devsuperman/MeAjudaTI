using System;
using System.Linq;
using Dominio.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UsuarioTests
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
        public void PodeFecharChamadoCorretamente()
        {
            Assert.IsTrue(usuario.PodeFechar(chamado));
        }

        [TestMethod]
        public void NaoPodeFecharChamadoSeNaoForOSolicitante()
        {            
            var outroUsuario = new Usuario("32145655664", "Mariazinha");
            Assert.IsFalse(outroUsuario.PodeFechar(chamado));
        }

        [TestMethod]       
        public void NaoPodeFecharChamadoSeJaEstiverFechado()
        {            
            usuario.Fechar(chamado, "liguei na tomada e resolveu");            
            Assert.IsFalse(usuario.PodeFechar(chamado));                        
        }

        [TestMethod]       
        public void NaoPodeFecharChamadoSeTecnicoAssumiu()
        {            
            tecnico.Assumir(chamado);            
            Assert.IsFalse(usuario.PodeFechar(chamado));                        
        }       

        
        [TestMethod]               
        public void PodeAvaliarChamado()
        {
            tecnico.Assumir(chamado);
            tecnico.Fechar(chamado, "liguei na tomada e resolveu");
            Assert.IsTrue(usuario.PodeAvaliar(chamado));
        }

        [TestMethod]               
        public void NaoPodeAvaliarChamadoSeAindaNaoFoiFechado()
        {   
            Assert.IsFalse(usuario.PodeAvaliar(chamado));
        }

        [TestMethod]                   
        public void NaoPodeAvaliarOChamadoMaisDeUmaVez()
        {
            tecnico.Assumir(chamado);
            tecnico.Fechar(chamado, "liguei na tomada e resolveu");
            
            usuario.Avaliar(chamado, 5, "não tem ocmentário");

            Assert.IsFalse(usuario.PodeAvaliar(chamado));            
        }
        
        [TestMethod]              
        public void NaoPodeAvaliarChamadoDoColeguinha()
        {
            tecnico.Assumir(chamado);
            tecnico.Fechar(chamado, "liguei na tomada e resolveu");

            var usuarioIvone = new Usuario("65498745655", "Ivone");

            Assert.IsTrue(usuario.PodeAvaliar(chamado));
            Assert.IsFalse(usuarioIvone.PodeAvaliar(chamado));
        }

        [TestMethod]              
        public void NaoPodeAvaliarSeFoiEuMesmoQueFechei()
        {            
            usuario.Fechar(chamado, "liguei na tomada e resolveu");
            Assert.IsFalse(usuario.PodeAvaliar(chamado));            
        }
       
    }
}
