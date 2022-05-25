using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    /// <summary>
    /// campo ID do usuário
    /// </summary>
    public class ObterUsuárioViewModel : CadastroViewModel
    {
        /// <summary>
        /// herda nome,email,senha da class CadastroViewModel
        /// Feito separado para gerar ID!
        /// </summary>
        public Guid Id { get; set; }
    }
}
