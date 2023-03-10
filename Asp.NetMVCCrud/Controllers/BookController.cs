using Asp.NetMVCCrud.Data;
using Asp.NetMVCCrud.Models;
using Asp.NetMVCCrud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetMVCCrud.Controllers
{
    public class BookController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public BookController(AspMvcDbContext aspMvcDbContext)
        {
            _aspMvcDbContext = aspMvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _aspMvcDbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookVM addBookRequest)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = addBookRequest.Title,
                Category = addBookRequest.Category,
                Author = addBookRequest.Author,
                Description = addBookRequest.Description,
                DateBorrowed = addBookRequest.DateBorrowed,
                Status = addBookRequest.Status
            };

            await _aspMvcDbContext.Books.AddAsync(book);
            await _aspMvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var book = await _aspMvcDbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                var viewModel = new UpdateBookVM()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Category = book.Category,
                    Author = book.Author,
                    Description = book.Description,
                    DateBorrowed = book.DateBorrowed,
                    Status = book.Status
                };
                 return await Task.Run(() => View("View", viewModel));
                //return await View(viewModel);
            }   
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateBookVM model)
        {
            var book = await _aspMvcDbContext.Books.FindAsync(model.Id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Category = model.Category;
                book.Author = model.Author;
                book.Description = model.Description;
                book.DateBorrowed = model.DateBorrowed;
                book.Status = model.Status;

                await _aspMvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //N.b remember to properly handle user not found error
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult>Delete(UpdateBookVM model)
        {
            var book = await _aspMvcDbContext.Books.FindAsync(model.Id);
            if (book != null)
            {
                _aspMvcDbContext.Books.Remove(book);
                await _aspMvcDbContext.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
    }
}
