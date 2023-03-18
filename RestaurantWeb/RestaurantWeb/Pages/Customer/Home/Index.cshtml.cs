using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        //we want list of all menuItem in Index page so we create a property with type of MenuItem and populate it in the Get method with using of unitofwork
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public void OnGet()
        {
            MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties:"Category,FoodType");
            CategoryList = _unitOfWork.Category.GetAll(orderby:u=>u.OrderBy(c=>c.DisplayOrder));
        }
    }
}
