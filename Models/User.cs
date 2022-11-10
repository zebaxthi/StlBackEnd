using StlBackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StlBackend.ViewModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Role { get; set; }

        public ICollection<InventoryModel> Inventaries { get; set; }

    }
}
