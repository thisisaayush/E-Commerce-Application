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
            List<Product> objProductList = _unitofwork.Product.GetAll().ToList();
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

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                _unitofwork.Product.Add(productVM.Product);
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
       
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product productFromDb = _unitofwork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitofwork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();
            TempData["success"] = "Product deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
