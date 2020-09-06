using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankLibrary;

namespace BankLibrary.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly BankLibraryContext _context;

        public DepartmentsController(BankLibraryContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == null) return RedirectToAction("Bank", "Index");
            ViewBag.BankName = name;
            ViewBag.BankId = id;
            var departmentsByBank = _context.Department.Where(department => department.BankId == id).Include(department => department.City);
            return View(await departmentsByBank.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Bank)
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create(int bankId)
        {
            ViewBag.BankId = bankId;
            ViewBag.BankName = _context.Bank.Where(bank => bank.Id == bankId).FirstOrDefault().Name;

            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bankId, [Bind("Id,BankId,Number,CityId,Info,NumberOfEmployers,Photo")] Department department)
        {
            department.BankId = bankId;
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Departments", new { id = bankId, name = _context.Bank.Where(bank => bank.Id == bankId).FirstOrDefault().Name });
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", department.CityId);
            return RedirectToAction("Index", "Departments", new { id = bankId, name = _context.Bank.Where(bank => bank.Id == bankId).FirstOrDefault().Name });
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Bank, "Id", "Name", department.BankId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", department.CityId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BankId,Number,CityId,Info,NumberOfEmployers,Photo")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Departments", new { id = department.BankId, name = _context.Bank.Where(bank => bank.Id == department.BankId).FirstOrDefault().Name });
            }
            ViewData["BankId"] = new SelectList(_context.Bank, "Id", "Name", department.BankId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", department.CityId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Bank)
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Departments", new { id = department.BankId, name = _context.Bank.Where(bank => bank.Id == department.BankId).FirstOrDefault().Name });
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
