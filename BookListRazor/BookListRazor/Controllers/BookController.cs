using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")] // Defines the route for API
    [ApiController] // Assigns route as APIController
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Adds API call to display table in the DB
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDB == null)
            {
                return Json(new { success = false, message = "Error deleting selection." });
            }
            else
            {
                _db.Book.Remove(bookFromDB);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Deleted selection successfully!" });
            }
        }
    }
}
