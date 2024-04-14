using LetishteNet5.Data.Entities;
using LetishteNet5.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace LetishteNet5.Controllers
{
    public class UsersController : Controller
    {
        private readonly FlightManagerDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(FlightManagerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {

            return _context.Users != null ?
                View(await _context.Users.ToListAsync()) :
                Problem("Entity set 'FlightManagerDbContext.User' is null.");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        /*public IActionResult Create()
        [Authorize(Roles = "Admin")]
        {
            //?
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]*/
        //do kolkoto razbrah Bind raboti kato konstruktor na koito opisvash NavPropertitata koito da vzeme
       
        
        /*public async Task<IActionResult> Create([Bind("UserName,PasswordHash,Email,FirstName,LastName, EGN, Address,PhoneNumber")] User user)
        {
            user.Id= Guid.NewGuid().ToString();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
               *//*user.*//*
            }

            if (ModelState.IsValid)
            {
                
                IdentityUserRole<string> role1 = new IdentityUserRole<string>();
                role1.RoleId = _context.Roles.First(x => x.Name == "User").Id;
                role1.UserId = user.Id;
                _context.Add(user);
                _context.Add(role1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user =  _context.Users.First(x=>x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,PasswordHash,Email,FirstName,LastName, EGN, Address,PhoneNumber")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                  
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(string userId)
        {
            return (_context.Users?.Any(e => e.Id == userId)).GetValueOrDefault();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'FlightManagerDContext.Users' is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
