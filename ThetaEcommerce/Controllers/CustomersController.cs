using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ThetaEcommerce.Models;

namespace ThetaEcommerce.Controllers
{
    public class CustomersController : Controller
    {
        private readonly theta_ecommerceContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomersController(theta_ecommerceContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return _dbcontext.Customers != null ? 
                          View(await _dbcontext.Customers.ToListAsync()) :
                          Problem("Entity set 'theta_ecommerceContext.Customers'  is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbcontext.Customers == null)
            {
                return NotFound();
            }

            var customer = await _dbcontext.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,PhoneNo,Email,Address,SystemUserId,CreatedBy,CreatedDate,ModifyDate,ModifiedBy,Status")] Customer customer, IList<IFormFile>? Fileimages)
        {
            if (Fileimages != null)
            {
                foreach (IFormFile item in Fileimages)
                {
                    var ImagePath = "/images/" + Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                    using (FileStream cc = new FileStream(_webHostEnvironment.WebRootPath + ImagePath, FileMode.Create))
                    {
                        item.CopyTo(cc);
                    }
                    customer.Image += "," + ImagePath;
                }
            }
            if (ModelState.IsValid)
            {
                _dbcontext.Add(customer);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbcontext.Customers == null)
            {
                return NotFound();
            }

            var customer = await _dbcontext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,PhoneNo,Email,Address,SystemUserId,CreatedBy,CreatedDate,ModifyDate,ModifiedBy,Status")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(customer);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbcontext.Customers == null)
            {
                return NotFound();
            }

            var customer = await _dbcontext.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbcontext.Customers == null)
            {
                return Problem("Entity set 'theta_ecommerceContext.Customers'  is null.");
            }
            var customer = await _dbcontext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbcontext.Customers.Remove(customer);
            }
            
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_dbcontext.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
