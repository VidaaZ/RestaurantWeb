using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RestaurantWeb.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id) //based on this id we retrieve menuitem and pass that in view
        {   
            //retrive UserId which is logged in our application
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value, //we have userId which is populate in shoppingcart object
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType"),//populating MenuIyem
                 MenuItemId = id
               
            };
           
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) { 
            
            _unitOfWork.ShoppingCart.Add(ShoppingCart);
            _unitOfWork.Save();
            return RedirectToPage("Index");
            }
            return Page();

           



        }
    }
}
