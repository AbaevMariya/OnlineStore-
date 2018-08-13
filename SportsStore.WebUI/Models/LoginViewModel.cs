using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)] // мы используем атрибут DataType, чтобы сообщить MVC Framework, как мы хотим отображать редактор свойства Password.
        public string Password { get; set; }
    }
}