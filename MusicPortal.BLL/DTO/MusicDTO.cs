using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicPortal.BLL.DTO
{
    public class MusicDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public byte[] AudioData { get; set; }
    }
}
