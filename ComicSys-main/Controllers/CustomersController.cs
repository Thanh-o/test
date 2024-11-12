using ComicSys.Data;
using ComicSys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComicSys.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicSystemContext _context;

        public CustomersController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegistrationDate = DateTime.Now; // Set ngày đăng ký hiện tại
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Điều hướng đến trang chủ hoặc trang khác sau khi đăng ký
            }
            return View(customer);
        }
    }
}
