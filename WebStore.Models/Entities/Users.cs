using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Models.Entities
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Введіть ім'я користувача")]
        [MaxLength(20,ErrorMessage="Максимум 20 символів")]
        [Display(Name="Ім'я користувача")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [MaxLength(40),MinLength(6,ErrorMessage="Мінімум 6 символів")]
        [DataType(DataType.Password)]
        [Display(Name="Пароль")]
        public string Password { get; set; }

        public int Role { get; set; }

  
        [ForeignKey("Role")]
        public virtual UsersRole UsersRole { get; set; }
    }
}
