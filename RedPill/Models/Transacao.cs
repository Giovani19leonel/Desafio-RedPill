using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.Models
{
    public class Transacao
    {
        [Required]
        [Key]
        public Guid TransacaoId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime HoraLancamento { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid TransferenciaId { get; set; }
    }
}
