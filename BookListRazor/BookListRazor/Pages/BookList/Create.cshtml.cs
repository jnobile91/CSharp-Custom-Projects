using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        // No argument is needed here, as we set "Book Book" as a [BindProperty] in line 20
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await _db.Book.AddAsync(Book); // Adds info from the object to be added from the "Create" form into a queue
                await _db.SaveChangesAsync(); // Adds info to DB
                return RedirectToPage("Index"); // Returns user to the Index page after entering a new object into the DB
            }
            else
            {
                return Page();
            }
        }
    }
}
