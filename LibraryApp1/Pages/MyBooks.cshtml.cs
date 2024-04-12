using LibraryApp1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp1.Pages
{
    public class MyBooksModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserInfo> _userManager;

        public MyBooksModel(ApplicationDbContext context, UserManager<UserInfo> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public UserInfo AppUser { get; set; }
        public IList<UserProduct> UserProducts { get; set; }

        public IList<Product> Products { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                UserProducts = await _context.UserProducts
                    .Include(up => up.Product)
                    .Where(up => up.Id == currentUser.Id)
                    .ToListAsync();

                foreach (var userProduct in UserProducts)
                {
                    var product = userProduct.Product;
                }


                return Page();

            }
            else
            {

                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult> OnPostReturnAsync(string processId)
        {
            var userProduct = await _context.UserProducts.FirstOrDefaultAsync(up => up.ProcessId == processId);

            var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == userProduct.ProductId);

            if (product != null)
            {
                _context.UserProducts.Remove(userProduct);
                await _context.SaveChangesAsync();

                product.ProductQuantity++;
                await _context.SaveChangesAsync();
            }


            return RedirectToPage();
        }
    }
}

