using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    public class DepositarViewModel
    {
        [Required(ErrorMessage = "Porfavor Digite um valor")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Porfavor Informe o seu identificador")]
        public Guid UsuarioId { get; set; }
    }
}
