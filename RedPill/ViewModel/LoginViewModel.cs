using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    /// <summary>
    /// Campos exigidos para autenticar o usuário
    /// </summary>
    public class LoginViewModel
    {
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Senha { get; set; }
    }
}
