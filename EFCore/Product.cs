using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        //reference: Navigation Property
        public Category Category { get; set; }
    }
}
