using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;



namespace RestaurantWeb.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; } //its a property
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                 _unitOfWork.Category.Add(Category);
               _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
               
            }
            return Page();
        }
    }
}
