using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPill.Negocio;
using RedPill.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.Controllers
{
    [ApiController]
    public class TransacaoController : Controller
    {
        /// <summary>
        /// Todas as requisições fornecidas por essa controller só são acessiveis com um JWT que é fornecida por meio
        /// de um Login!
        /// Para autorizar é preciso fornecer na API -> Bearer + TOKEN
        /// Exemplo: bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IkZlcm5hbmRhQGhvdG1haWwuY29tIiwibmJmIjoxNjUzNTE4MDcxLCJleHAiOjE2NTM1MjE2NzEsImlhdCI6MTY1MzUxODA3MX0.P2MbcoQ35Ams2LhGSK0ZU6NJQmbdgTNtokebl8DBRcc
        /// </summary>
        TransacaoNegocio transacao = new TransacaoNegocio();

        /// <summary>
        /// Metodo Requisição para consultar o Saldo de uma conta no Banco de dados
        /// </summary>
        [Authorize]
        [HttpGet("/Api/Transacao/ConsultarSaldo")]
        public IActionResult ConsultarSaldo(string id)
        {
            var usuarios = transacao.ConsultarSaldo(id);
            return Ok(usuarios);
        }
        /// <summary>
        /// Metodo Requisição para consultar o Extrato de uma conta no Banco de dados
        /// </summary>
        [Authorize]
        [HttpGet("/Api/Transacao/ConsultarExtrato")]
        public IActionResult ConsultarExtrato(string id)
        {
            var usuarios = transacao.ObterExtrato(id);
            return Ok(usuarios);
        }

        /// <summary>
        /// Método Requisição para efetuar um deposito(Simulado) no banco de dados.
        /// </summary>
        [Authorize]
        [HttpPost("/Api/Transacao/Depositar")]
        public IActionResult Depositar(DepositarViewModel depositar)
        {
            var usuarios = transacao.Depositar(depositar.UsuarioId.ToString(), depositar.Valor);
            return Ok(usuarios);
        }
        /// <summary>
        /// Método Requisição para efetuar uma transferencia entre contas no Banco de dados
        /// </summary>
        [Authorize]
        [HttpPost("/Api/Transacao/Transferir")]
        public IActionResult Transferir(TransferenciaViewModel transferencia)
        {
            var usuarios = transacao.Transferir(transferencia.UsuarioId.ToString(), transferencia.Valor, transferencia.TransferirId.ToString());
            return Ok(usuarios);
        }
    }
}
