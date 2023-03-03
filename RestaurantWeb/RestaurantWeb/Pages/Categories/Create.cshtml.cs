using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantWeb.Data;
using RestaurantWeb.Model;

namespace RestaurantWeb.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public Category Category { get; set; } //its a property
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
          await _db.Category.AddAsync(Category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
