using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp1.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductAuthor { get; set; }
        public int ProductQuantity { get; set; }

        public string ProductPhoto { get; set; }
        public int ProductPurchased { get; set; } =  0;
        

        public ICollection<UserProduct> UserProducts{ get; set; }

        void Method1()
        {


        }
       
        public Product() { }
    }
}

