using RedPill.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.ViewModel
{
    public class TransferenciaViewModel : DepositarViewModel
    {
        /// <summary>
        ///  herda UsuarioId e Valor da class DepositarViewModel
        /// </summary>
        [Required]
        public Guid TransferirId { get; set; }
    }
}
