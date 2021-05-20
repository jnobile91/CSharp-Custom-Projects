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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);

            // Checks if book is null, returns a "Not Found" error
            if(book == null)
            {
                return NotFound();
            }
            // Otherwise, deletes the entry
            else
            {
                _db.Book.Remove(book); // Queues the book to be deleted
                await _db.SaveChangesAsync(); // Submits delete to the DB, removing the tuple

                return RedirectToPage("Index"); // Returns the user to the Index page
            }
        }
    }
}
