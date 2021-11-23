using System;
using System.ComponentModel.DataAnnotations;

namespace API_csharp.Models.Usuarios
{
    public class RegistrarViewModelInput
    {
        [Required(ErrorMessage = "O login é obrigatorio")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O Email é obrigatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatoria")]
        public string Senha { get; set; }
    }
}

