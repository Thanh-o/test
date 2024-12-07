using ComicSys.Data;
using ComicSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ComicSys.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
                .ToListAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            // Fetch all customers and comic books from the database for the dropdown lists
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View();
        }


        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalDate,ReturnDate,CustomerId")] Rental rental, List<RentalDetailModel> rentalDetails)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Set the rental date to the current date
                    rental.RentalDate = DateTime.Now;

                    // Add the rental to the Rentals table
                    _context.Add(rental);
                    await _context.SaveChangesAsync();  // Save to get the rental ID (auto-generated)

                    // For each rental detail (for each comic book rented)
                    foreach (var detail in rentalDetails)
                    {
                        var rentalDetail = new RentalDetail
                        {
                            RentalID = rental.RentalID,  // Link the rental detail to the rental
                            ComicBookID = detail.ComicBookID,  // The comic book being rented
                            Quantity = detail.Quantity,  // The quantity of comic books being rented
                            PricePerDay = detail.PricePerDay  // The rental price per day for this comic book
                        };

                        // Add the rental detail to the RentalDetails table
                        _context.RentalDetails.Add(rentalDetail);
                    }

                    // Save all changes
                    await _context.SaveChangesAsync();

                    // Commit the transaction if everything is successful
                    await transaction.CommitAsync();

                    // Redirect back to the index page
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // If something goes wrong, rollback the transaction
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "An error occurred while creating the rental.");
                }
            }

            // If there was an error, load the dropdown lists again for the view
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalID == id);
        }
    }

    public class RentalDetailModel
    {
        public int ComicBookID { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
