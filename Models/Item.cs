using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace barter_razor.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Category { get; set; }

    }

    public class ItemUploader : Item
    {
        [ForeignKey("UserID")]
        [Required]
        public IdentityUser IdentityUser {get; set;}

        public void copyItem(Item item)
        {  
            this.Id = item.Id;
            this.Name = item.Name;
            this.Price = item.Price;
            this.Category = item.Category;         
        }
    }
}