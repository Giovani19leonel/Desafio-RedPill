using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedPill.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RedPill.Negocio;

namespace RedPill.Controllers
{
    [ApiController]
    public class UsuarioController : Controller
    {

        // Instancia da classe Usuarios
        UsuarioNegocio contas = new UsuarioNegocio();

        /// <summary>
        /// Método para obter todos os Usuários existentes
        /// </summary>
        [HttpGet("/Api/ObterTodosUsuarios")]
        [ProducesResponseType(typeof(List<CadastroViewModel>), 200)]
        [ProducesResponseType(500)]
        public IActionResult ObterTodosUsuarios()
        {
            var usuarios = contas.ObterTodosUsuarios();
            return Ok(usuarios);
        }
        /// <summary>
        /// Método para Obter um usuário especifico no banco de dados
        /// </summary>

        [HttpGet("/Api/ObterUsuario")]
        public IActionResult ObterUsuario(string id)
        {
            var usuarios = contas.ObterUsuario(id);
            return Ok(usuarios);
        }

        /// <summary>
        /// Método para Autenticar um usuário
        /// </summary>
        [HttpPost("/Api/Login")]
        public IActionResult Login(LoginViewModel parametros)
        {
            var autenticar = contas.Logar(parametros.Email, parametros.Senha);
            return Ok(autenticar);
        }

        /// <summary>
        /// Método para Cadastrar um usuário no Banco de dados
        /// </summary>
        [HttpPost("/Api/CadastrarUsuario")]
        public IActionResult Post(CadastroViewModel usuario)
        {
            var usuarios = contas.CadastrarUsuario(usuario.Nome, usuario.Email, usuario.Senha);
            return Ok(usuarios);
        }
        /// <summary>
        /// Método para Excluir um usuário no Banco de dados
        /// </summary>
        [HttpDelete("/Api/DeletarUsuario")]
        public IActionResult DeleteUsuario(DeletarUsuarioViewModel deletarUsuário)
        {
            var usuarios = contas.DeletarUsuario(deletarUsuário.Id, deletarUsuário.Email, deletarUsuário.Senha);
            return Ok(usuarios);
        }
    }
}

