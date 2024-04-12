using LibraryApp1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserInfo> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<UserInfo> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string ErrorMessage { get; set; }

        public IList<UserInfo> AppUser { get; set; }
        public IList<Product> Products { get; set; }

        public IList<UserProduct> UserProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            UserProducts = await _context.UserProducts
                .Include(up => up.UserInfo)
                .Include(up => up.Product)
                .OrderBy(up => up.ProductPurchase)
                .ToListAsync();

            IQueryable<Product> products = _context.Product.Where(p => p.ProductQuantity > 0);

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search) || p.ProductAuthor.Contains(search));
            }

            Products = await products.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostPurchaseAsync(string productId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == productId);

            
                var totalPurchases = await _context.UserProducts.CountAsync(up => up.Id == currentUser.Id);
                if (totalPurchases >= 2)
                {
                    ErrorMessage = "En Fazla 2 ürün alabilirsiniz.";
                    return await OnGetAsync(null);
                }


                var existingPurchase = await _context.UserProducts
                    .FirstOrDefaultAsync(up => up.Id == currentUser.Id && up.ProductId == productId);

                if (existingPurchase == null )
                {
                    
                    var userProduct = new UserProduct
                    {
                        Id = currentUser.Id,
                        ProductId = product.ProductId,
                        ProductPurchase = DateTime.Now,
                        ProcessId = Guid.NewGuid().ToString()


                    };

                    _context.UserProducts.Add(userProduct);
                    await _context.SaveChangesAsync();

                    
                    product.ProductQuantity--;
                    await _context.SaveChangesAsync();

                    product.ProductPurchased++;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ErrorMessage = "Aynı üründen iki tane alamazsınız.";
                    
                    return await OnGetAsync(null);
                }
            


            return RedirectToPage("/MyBooks");
        }

    }
}
