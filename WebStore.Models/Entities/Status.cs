using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.Entities
{
    public class Status
    {
        public int StatusID{get;set;}


        [Required(ErrorMessage = "Введіть статус")]
        [MaxLength(20, ErrorMessage = "Максимум 20 символів")]
        public string Name { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
