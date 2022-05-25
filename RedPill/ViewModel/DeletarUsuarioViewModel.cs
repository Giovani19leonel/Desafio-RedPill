using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    public class DeletarUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Senha { get; set; }
    }
}
