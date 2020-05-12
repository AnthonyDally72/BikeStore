using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProductTypesController( ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        // Get Create Action Method
        public IActionResult Create()
        {
            return View();
        }


        // Post Create action method
        [HttpPost]
        [ValidateAntiForgeryToken] // checks whether the token is valid or not. 
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid) //  checks the requirements of the model properties
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        // Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();

            }

            var productType = await _db.ProductTypes.FindAsync(id);
            if(productType == null)
            {
                return NotFound();
            }

            return View(productType);
            
        }


        // Post Edit action method
        [HttpPost]
        [ValidateAntiForgeryToken] // checks whether the token is valid or not. 
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) //  checks the requirements of the model properties
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        // GET Delete Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);

        }

        // Get Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);

        }


        // Post Delete action method
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken] // checks whether the token is valid or not. 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTypes = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }


    }
}