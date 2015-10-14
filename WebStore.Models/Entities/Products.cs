using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace WebStore.Models.Entities
{
    [MetadataType(typeof(ProductsMetadataSource))]
    public partial class Products
    {

        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(100)]   
        public string Image { get; set; }

        [Required] 
        [MaxLength(50)]
        public string Name { get; set; }

        [Required] 
        public decimal Price { get; set; }

        public Guid Code { get; set; }

        [Required] 
        [MaxLength(50)]
        public string Company { get; set; }

        [Required] 
        public int Avilability{ get; set; }

        [ForeignKey("Avilability")] 
        public virtual Status Status{get;set;}

        public virtual ICollection<Categories> Categories { get; set; }

    }

    public class ProductsMetadataSource
    {
       
        [ScaffoldColumn(true)]
        [Display(Name="ID")]
        public int ProductID { get; set; }

      
     
        [Display(Name = "Зображення")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Введіть назву продукту")]
        [Display(Name = "Назва")]
        [MaxLength(50,ErrorMessage="Максимум 50 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть ціну продукту")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "Код")]
        public Guid Code { get; set; }

        [Required(ErrorMessage = "Введіть виробника продукту")]
        [Display(Name = "Виробник")]
      
        public string Company { get; set; }

        [Required(ErrorMessage = "Оберіть статус")]
        [Display(Name = "Статус")]

        public int Avilability { get; set; }

     
        [Display(Name = "Статус")]
        public virtual Status Status { get; set; }

        [Display(Name = "Категорія")]
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
