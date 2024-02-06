using Book_Store_Database_Design.Data;
using Book_Store_Database_Design.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Database_Design.Controllers
{
    public class AuthorController: Controller
    {
        private readonly BookStoreDbContext _dbContext;

        //create dependency injection.
        public AuthorController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }  

        public IActionResult Index()
        {
            var authors = _dbContext.Authors.ToList();
            return View(authors);
        }

        public IActionResult Create()
        {
            //just create view();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var author = _dbContext.Authors.Find(id);
            return View(author);
        }

        public IActionResult Edit(Author author)
        {
            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var author = _dbContext.Authors.Find(id);
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return RedirectToAction("Index"); // Redirecting to the Index for simplicity; adjust as needed.
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
