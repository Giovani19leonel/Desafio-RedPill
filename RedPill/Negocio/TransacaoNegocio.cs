using RedPill.Data;
using RedPill.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedPill.Negocio;
using RedPill.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace RedPill.Negocio
{
    /// <summary>
    /// TODOS OS MÉTODOS DESSA CLASSE SÓ PODEM SER ACESSADOS COM AUTORIZAÇÃO DE UM JWT
    /// </summary>
    public class TransacaoNegocio
    {
        Transacao transacao = new Transacao();
        /// <summary>
        /// Metodo para inserir um valor no banco de dados na conta fornecida pelo Identificador[id]
        /// </summary>
        public string Depositar(string UsuarioId, decimal Valor)
        {
            bool contaExistente = false;
            // verifica se o identificador fornecido é de uma conta valida
            if (VerificacaoContaValida(UsuarioId))
            {
                transacao.UsuarioId = Guid.Parse(UsuarioId);
                transacao.Valor = transacao.Valor + Valor;
                transacao.HoraLancamento = DateTime.Now;
                transacao.TransacaoId = Guid.NewGuid();
                transacao.TransferenciaId = Guid.NewGuid();
                contaExistente = true;
            }
            using (RedpillDBContext db = new RedpillDBContext())
            {
                // caso a conta seja valida ele insere no Banco de dados
                if (contaExistente)
                {
                    db.Entry(transacao).State = EntityState.Added;
                    db.SaveChanges();
                    return "Deposito efetuado com sucesso!";
                }
                return "Falha na autenticação, essa conta não existe!";
            }
        }

        /// <summary>
        /// Método para consultar um saldo de uma conta com base em seu Identificador
        /// </summary>
        public decimal ConsultarSaldo(string UsuarioId)
        {
            using (RedpillDBContext db = new RedpillDBContext())
            {
                var lancamentos = db.Transacao.Where(x => x.UsuarioId == Guid.Parse(UsuarioId)).ToList();
                decimal saldo = lancamentos.Sum(x => x.Valor);
                return saldo;
            }
        }
        /// <summary>
        /// Metodo para transferir valores entre contas, só é possivel caso o valor da conta que fará a transferencia
        /// seja maior do que o valor que será enviado.
        /// 
        /// Recebe como parametro o identificador da conta que efetuara a transferencia, o valor a ser transferido
        /// e o indetificador da conta que receberá esse valor.
        /// </summary>
        public string Transferir(string UsuarioId, decimal Valor, string UsuarioTransferenciaId)
        {
            var transacaoID = Guid.NewGuid();

            using (RedpillDBContext db = new RedpillDBContext())
            {
                decimal SaldoConta = ConsultarSaldo(UsuarioId);

                if (SaldoConta > Valor)
                {
                    if (VerificacaoContaValida(UsuarioId) && VerificacaoContaValida(UsuarioTransferenciaId))
                    {
                        Transacao transacao1 = new Transacao();

                        transacao1.UsuarioId = Guid.Parse(UsuarioId);
                        transacao1.Valor = transacao1.Valor - Valor;
                        transacao1.HoraLancamento = DateTime.Now;
                        transacao1.TransacaoId = Guid.NewGuid();
                        transacao1.TransferenciaId = transacaoID;
                        db.Entry(transacao1).State = EntityState.Added;
                        db.SaveChanges();


                        Transacao transacao2 = new Transacao();

                        transacao2.UsuarioId = Guid.Parse(UsuarioTransferenciaId);
                        transacao2.Valor = transacao2.Valor + Valor;
                        transacao2.HoraLancamento = DateTime.Now;
                        transacao2.TransacaoId = Guid.NewGuid();
                        transacao2.TransferenciaId = transacaoID;
                        db.Entry(transacao2).State = EntityState.Added;
                        db.SaveChanges();

                        return "Transacao efetuada com sucesso!";
                    }
                }
                return "Informe uma conta válida!";
            }
        }

        /// <summary>
        /// Obtem todo o histórico de transações da conta fornecida (identificador da conta)
        /// </summary>
        public List<Transacao> ObterExtrato(string UsuarioId)
        {
            var listaUsuarios = new List<Transacao>();

            using (RedpillDBContext db = new RedpillDBContext())
            {
                var usuarios = db.Transacao.Where(x => x.UsuarioId == Guid.Parse(UsuarioId)).ToList();
                listaUsuarios.AddRange(usuarios.Select(x => new Transacao
                { UsuarioId = x.UsuarioId, Valor = x.Valor, HoraLancamento = x.HoraLancamento, TransacaoId = x.TransacaoId, TransferenciaId = x.TransferenciaId }).ToList());
            }
            return listaUsuarios;
        }

        /// <summary>
        /// Metodo para verificar se uma conta é valida, no caso se ela existe no Banco de dados.
        /// </summary>

        public bool VerificacaoContaValida(string UsuarioId)
        {
            var usuario = new UsuarioNegocio();
            var usuarioObtido = usuario.ObterUsuario(UsuarioId);
            return !string.IsNullOrEmpty(usuarioObtido.Nome) && !string.IsNullOrEmpty(usuarioObtido.Email) && !string.IsNullOrEmpty(usuarioObtido.Senha);
        }
    }
}
