using Book_Store_Database_Design.Data;
using Book_Store_Database_Design.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Database_Design.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookStoreDbContext _dbContext;

        public CategoryController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {   
            //creates view form for new book. User see the form/view.
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            //goes to database to find that data/record. id has to go to the database to retrieve the data.
            var category = _dbContext.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //dispays updated values.
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = _dbContext.Categories.Find(id);
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Index"); // Redirecting to the Index for simplicity; adjust as needed.
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
