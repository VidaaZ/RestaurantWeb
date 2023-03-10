using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data;
using Restaurant.Models;



namespace RestaurantWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public FoodType FoodType { get; set; } //its a property
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
          
            if (ModelState.IsValid)  //server side validation
            {
                await _db.FoodType.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
               
            }
            return Page();
        }
    }
}
