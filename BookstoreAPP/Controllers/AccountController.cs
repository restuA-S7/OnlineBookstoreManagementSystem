using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookstoreAPP.Models;

namespace BookstoreAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookstoreDbContext _context;

        public AccountController(BookstoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Validasi login dengan memeriksa database
            var account = _context.Accounts.SingleOrDefault(a => a.Username == username && a.Password == password);

            if (account != null)
            {
                // Temukan pelanggan berdasarkan nama pengguna
                var customer = _context.Customers.SingleOrDefault(c => c.Name == username);
                if (customer == null)
                {
                    ModelState.AddModelError("", "Tidak dapat menemukan pelanggan yang sesuai.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("CustomerEmail", customer.Email) // Simpan CustomerEmail sebagai klaim
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            // Jika login gagal, kembalikan ke halaman login dengan pesan error
            ModelState.AddModelError("", "Username atau password salah");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingAccount = _context.Accounts.SingleOrDefault(a => a.Username == model.Username);
            if (existingAccount != null)
            {
                ModelState.AddModelError("", "Username sudah ada. Silakan gunakan username lain.");
                return View(model);
            }

            var newAccount = new Account
            {
                Username = model.Username,
                Password = model.Password
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            var newCustomer = new Customer
            {
                Name = model.Username,
                Email = model.Email
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim("CustomerEmail", newCustomer.Email) // Simpan CustomerEmail sebagai klaim
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }
    }
}
