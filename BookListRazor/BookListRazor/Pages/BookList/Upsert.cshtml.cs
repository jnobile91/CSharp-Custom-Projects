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

        public UpsertModel(ApplicationDbContext db)
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
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book); // Adds new book
                }
                else
                {
                    _db.Update(Book); // Updates every property in the object
                }
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
