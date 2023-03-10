using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public FoodType FoodType { get; set; } //its a property
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
          
            if (ModelState.IsValid)  //server side validation
            {
                 _db.FoodType.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
               
            }
            return Page();
        }
    }
}
