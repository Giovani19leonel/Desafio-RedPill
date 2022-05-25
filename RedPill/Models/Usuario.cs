using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedPill.Models
{
    public class Usuario
    {
        [Required]
        [Key]
        public Guid UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }       
        [Required]
        public string Senha { get; set; }
    }
}
