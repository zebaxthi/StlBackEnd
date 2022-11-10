using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StlBackend.Data;
using StlBackend.Models;
using StlBackend.ViewModels;

namespace StlBackend.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventaryContext _context;

        public InventoryController(InventaryContext context, UserController userController)
        {
            _context = context;
        }

        // GET: Inventory
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var inventaryContext = _context.Inventories.Include(i => i.User);
            return View(await inventaryContext.ToListAsync());
        }

        // GET: Inventory/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.Inventories
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            return View(inventoryModel);
        }

        // GET: Inventory/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Quantity,UserId")] InventoryModel inventoryModel)
        {
            if (ModelState.IsValid)
            {
                inventoryModel.Id = Guid.NewGuid();
                inventoryModel.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                User user = new User()
                {
                    Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
                    LastName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
                    EmailAddress = "a", //User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Role = "a" //User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                };
                _context.Add(user);
                _context.Add(inventoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inventoryModel.UserId);
            return View(inventoryModel);
        }

        // GET: Inventory/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.Inventories.FindAsync(id);
            if (inventoryModel == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inventoryModel.UserId);
            return View(inventoryModel);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Status,Quantity,UserId")] InventoryModel inventoryModel)
        {
            if (id != inventoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryModelExists(inventoryModel.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inventoryModel.UserId);
            return View(inventoryModel);
        }

        // GET: Inventory/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.Inventories
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            return View(inventoryModel);
        }

        // POST: Inventory/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inventoryModel = await _context.Inventories.FindAsync(id);
            _context.Inventories.Remove(inventoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryModelExists(Guid id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
