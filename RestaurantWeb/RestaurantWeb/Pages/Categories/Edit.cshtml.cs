using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantWeb.Data;
using RestaurantWeb.Model;

namespace RestaurantWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public Category Category { get; set; } //its a property
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString()) //server side validation
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder can not exactly match the Name. ");
            }
            if (ModelState.IsValid)  //server side validation
            {
                 _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
               
            }
            return Page();
        }
    }
}
