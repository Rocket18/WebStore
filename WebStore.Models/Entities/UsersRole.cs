using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.Entities
{
    public class UsersRole
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
