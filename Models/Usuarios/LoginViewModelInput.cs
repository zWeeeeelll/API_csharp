using System;
using System.ComponentModel.DataAnnotations;

namespace API_csharp.Models.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O senha é obrigatório")]
        public string Senha { get; set; }
    }
}

