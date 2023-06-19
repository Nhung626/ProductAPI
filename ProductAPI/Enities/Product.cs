using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Enities{
    [Table("Product")]
    public class Product{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         [Required] 
        [MaxLength(100)]
        public string ProductName{get; set;}
        [Required] 
        [MaxLength(100)]
        public string Description{get; set;}
        public Double Price{get; set;}
    }
}