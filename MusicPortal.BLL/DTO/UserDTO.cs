using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicPortal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 20 символов")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Логин может содержать только латинские буквы и цифры")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Пароль должен быть от 3 до 20 символов")]
        public string Password { get; set; }
    }
}
