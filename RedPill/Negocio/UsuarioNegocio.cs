using Microsoft.EntityFrameworkCore;
using RedPill.Models;
using RedPill.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedPill.Data;
using RedPill.Services;
using Microsoft.AspNetCore.Mvc;

namespace RedPill.Negocio
{
    public class UsuarioNegocio
    {
        // Metodo para pegar todos os Usúarios cadastrados no Banco de dados.
        // retorna uma lista dos usuários
        public List<ObterUsuárioViewModel> ObterTodosUsuarios()
        {
            var listaUsuarios = new List<ObterUsuárioViewModel>();

            using (RedpillDBContext db = new RedpillDBContext())
            {
                var usuarios = db.Usuario;
                listaUsuarios.AddRange(usuarios.Select(x => new ObterUsuárioViewModel
                { Id = x.UsuarioId, Email = x.Email, Nome = x.Nome, Senha = x.Senha }).ToList());
            }

            return listaUsuarios;
        }
        /// <summary>
        /// Método para obter um usuário especifico cadastrado no Banco de dados, 
        /// ele recebe como parametro o Identificador da conta que será exibida.
        /// Retorna uma lista com os campos dessa conta
        /// </summary>
        public Usuario ObterUsuario(string id)
        {
            var usuario = new Usuario();

            using (RedpillDBContext db = new RedpillDBContext())
            {
                var registro = db.Usuario.FirstOrDefault(x => x.UsuarioId.ToString() == id);
                usuario.Email = registro.Email;
                usuario.Senha = registro.Senha;
                usuario.Nome = registro.Nome;
                usuario.UsuarioId = registro.UsuarioId;
            }
            return usuario;
        }
        /// <summary>
        /// Método para obter o Token de acesso as transações da API, é necessário fornecer uma conta válida no Banco de dados
        /// Faz a verificação de acesso e retorna o Token com validação de 1 hora!
        /// </summary>
        public string Logar(string email, string senha)
        {
            Usuario user = new Usuario();
            var usuarios = ObterTodosUsuarios();
            var autenticar = usuarios.Exists(usuario => usuario.Email == email && usuario.Senha == senha);
            if (autenticar)
            {
                user.Email = email;
                user.Senha = senha;
                var token = TokenService.GenerateToken(user);
                var resultado = "O seu token de acesso as transações é: "  + token;
                return resultado;
            }
            return "Conta invalida";
        }

        /// <summary>
        /// Metódo para cadastrar um usuário no Banco de dados, informando o nome email e senha desse usuário!
        /// </summary>
        public string CadastrarUsuario(string nome, string email, string senha)
        {
            var usuario = new Usuario();

            using (RedpillDBContext db = new RedpillDBContext())
            {
                var usuarios = ObterTodosUsuarios();
                var consultar = usuarios.Exists(usuario => usuario.Email == email);
                if (!consultar)
                {
                    usuario.Email = email;
                    usuario.UsuarioId = Guid.NewGuid();
                    usuario.Nome = nome;
                    usuario.Senha = senha;
                    db.Entry(usuario).State = EntityState.Added;
                    db.SaveChanges();
                    var result = "Conta criada com sucesso, seu ID de identificação é: " + usuario.UsuarioId + "\n Para receber seu token de acesso faça o login";
                    return result;
                }
                else
                {
                    return "Já existe uma conta cadastrada com esse email.";
                }
            }
        }
        /// <summary>
        /// Método para deletar um usuário do banco de dados, recebe como parametro o identificador dessa conta, o email
        /// e a senha, faz a autenticação da conta para depois excluir!
        /// </summary>
        public string DeletarUsuario(string id, string email, string senha)
        {
            var usuario = new Usuario();
            using (RedpillDBContext db = new RedpillDBContext())
            {
                if (Autenticar(id, email, senha))
                {
                    usuario.Email = email;
                    usuario.UsuarioId = Guid.Parse(id);
                    usuario.Senha = senha;
                    db.Entry(usuario).State = EntityState.Deleted;
                    db.SaveChanges();
                    return "Conta excluida com sucesso!";
                }
            }
            return "Falha na autenticação, por favor digite uma conta válida";
        }

        /// <summary>
        /// Método para VALIDAR a conta fornecida
        /// </summary>
        private bool Autenticar(string id, string email, string senha)
        {
            var usuario = ObterUsuario(id);
            return usuario.Email == email && usuario.Senha == senha;
        }
    }
}
