using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp1.Models
{

    public class UserProduct
    {
        [Key]
        public string ProcessId { get; set; }

        [ForeignKey("UserInfo")]
        public string Id { get; set; }

        public UserInfo UserInfo { get; set; }

        [ForeignKey("Product")]

        public string ProductId { get; set; }

         public Product Product { get; set; }
       
        public DateTime ProductPurchase { get; set; }

        
    }
}