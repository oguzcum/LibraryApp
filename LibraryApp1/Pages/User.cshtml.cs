using LibraryApp1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp1.Pages
{
    public class User : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserInfo> userManager;

        public UserInfo appUser;
        public IList<Product> Products { get; set; }

        public User(ApplicationDbContext context, UserManager<UserInfo> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public void OnGet(string search)
        {
            IQueryable<Product> products = _context.Product;


            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search) || p.ProductAuthor.Contains(search));
            }

            Products = products.ToList();
        }
        




    }
}