using Book_Store_Database_Design.Data;
using Book_Store_Database_Design.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Database_Design.Controllers
{
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _dbContext;

        public BookController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var books = _dbContext.Books.ToList();
            return View(books);
        }

        public IActionResult Create()
        {   
            //creates view form for new book. User see the form/view.
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            //goes to database to find that data/record. id has to go to the database to retrieve the data.
            var book = _dbContext.Books.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            //dispays updated values.
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = _dbContext.Books.Find(id);
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index"); // Redirecting to the Index for simplicity; adjust as needed.
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
