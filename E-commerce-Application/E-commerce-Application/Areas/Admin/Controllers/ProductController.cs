using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace E_commerce_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index() //goes to index.cshtml class in Views/Product folder.
        {
            List<Product> objProductList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.CategoryId.ToString()
               }),
                Product = new Product()
            };
            if (id == null || id == 0)
                //Insert-Create
            {
                return View(productVM);
            }
            else //Update
            {
                productVM.Product = _unitofwork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
           
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    String fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //Delete Old Image.
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if(productVM.Product.Id == 0)
                {
                    //Add Id, product.
                    _unitofwork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitofwork.Product.Update(productVM.Product);
                }
                
                _unitofwork.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitofwork.Category
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.CategoryId.ToString()
               });
                return View(productVM);
            }
        }
       
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitofwork.Product.Get(u => u.Id == id);

            if(productToBeDeleted == null)
            {
                return Json(new { success=false, message="Error while deleting"});
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitofwork.Product.Remove(productToBeDeleted);
            _unitofwork.Save();

            return Json(new { success = true, message = "Delete Successful." });
        }
        #endregion
    }
}
