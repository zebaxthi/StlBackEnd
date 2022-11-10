using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StlBackend.Models
{
    [Table("Product")]
    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        public string Description { get; set; }

        [Required]
        public double Quantity { get; set; }

        public Guid InventoryId { get; set; }

        [ForeignKey("InventoryId")]
        public InventoryModel Inventory { get; set; }

    }
}
