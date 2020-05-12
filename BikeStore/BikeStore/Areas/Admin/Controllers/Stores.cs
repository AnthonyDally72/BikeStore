using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models;
using BikeStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.Admin.Controllers
{
   // [Authorize(Roles = StaticDetails.SuperAdminEndUser)]
    [Area("Admin")]
    public class StoresController : Controller
    {

        private readonly ApplicationDbContext _db;

        public StoresController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Stores.ToList());
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stores stores)
        {
            if (ModelState.IsValid)
            {
                _db.Add(stores);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stores);
        }


        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _db.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stores stores)
        {
            if (id != stores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(stores);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stores);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _db.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }


        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _db.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stores = await _db.Stores.FindAsync(id);
            _db.Stores.Remove(stores);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}