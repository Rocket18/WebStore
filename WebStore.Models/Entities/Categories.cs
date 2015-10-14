using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.Entities
{
    public class Categories
    {
       
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
