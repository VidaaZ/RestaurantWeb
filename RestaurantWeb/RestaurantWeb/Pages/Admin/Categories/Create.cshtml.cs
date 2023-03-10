using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;



namespace RestaurantWeb.Pages.Admin.Categories
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
            if (Category.Name == Category.DisplayOrder.ToString()) //server side validation
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder can not exactly match the Name. ");
            }
            if (ModelState.IsValid)  //server side validation
            {
                await _db.Category.AddAsync(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
               
            }
            return Page();
        }
    }
}
