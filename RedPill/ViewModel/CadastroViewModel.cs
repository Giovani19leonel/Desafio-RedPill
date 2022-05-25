using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    /// <summary>
    /// Propriedades exigidas para cadastro do usuário no BD
    /// </summary>
    public class CadastroViewModel
    { 
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "A senha deve conter de 4-20 caracteres")]
        public string Senha { get; set; }
    }
}
