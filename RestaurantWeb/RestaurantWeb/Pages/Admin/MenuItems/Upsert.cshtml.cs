using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;



namespace RestaurantWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItem MenuItem { get; set; } //its a property
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {    
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            MenuItem = new();
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u=>u.Id==id); //populate menuitem and load all data from datababse
            }
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem() //populate CategoryList to retrieve all Categories
            {
                Text = i.Name,
                Value=i.Id.ToString()
            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }
        public async Task<IActionResult> OnPost()
        {

            //get the root path of wwwroot folder:
            string webRootPath = _hostEnvironment.WebRootPath;
            //capture the files which was uploaded:
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //give a name to our new file and make sure each file has unique name
                string fileName_new = Guid.NewGuid().ToString();
                //finding out the folder of uploads:
                var uploads = Path.Combine(webRootPath, @"images\menuItems"); //final place where we have to uploads our files
                //make sure files have the same extension:
                var extension = Path.GetExtension(files[0].FileName);
                //finally we should copy our file inside the folder that we determined:

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension),FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
                //create a new menuItem
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();


            }
            else
            {
                //update
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                if (files.Count > 0)
                {  //give a name to our new file and make sure each file has unique name
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuItems"); //final place where we have to uploads our files
                                                                                  //make sure files have the same extension:
                    var extension = Path.GetExtension(files[0].FileName);

                    //give  the old image path:
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    //if you want to update the image you should delete the old image so you should check if there is exist or not:
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //upload new image into the folder

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
