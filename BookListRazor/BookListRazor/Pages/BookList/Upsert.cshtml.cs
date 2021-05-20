using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id) // The "?" allows the id to be null, allowing us to use this function for create + edit
        {
            Book = new Book();

            // Create new entry
            if (id == null)
            {
                return Page();
            }
            else
            {
                Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            }

            // Update existing entry
            if (Book == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Adds assigned book (using "Id") to queue,
                // then reassigns properties from the "Edit" window
                var BookFromDb = await _db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync(); // Changes info in DB
                return RedirectToPage("Index"); // Returns user to the Index page after editing an object in the DB
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
