using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Extensions;
using BikeStore.Models;
using BikeStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
            };
        }

        //Get Index Shopping Cart
        public async Task<IActionResult> Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count > 0)
            {
                foreach (int cartItem in lstShoppingCart)
                {
                    Products prod = _db.Products.Include(p => p.Stores).Include(p => p.ProductTypes).Where(p => p.Id == cartItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(prod);
                }
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            ShoppingCartVM.Reservations.ReservationDate = ShoppingCartVM.Reservations.ReservationDate
                                                            .AddHours(ShoppingCartVM.Reservations.ReservationTime.Hour)
                                                            .AddHours(ShoppingCartVM.Reservations.ReservationTime.Minute);

            Reservations reservations = ShoppingCartVM.Reservations;
            _db.Reservations.Add(reservations);
            _db.SaveChanges();

            int reservationsId = reservations.Id;

            foreach (int productId in lstCartItems)
            {
                ProductsSelectForReservation productsSelectedForAppointment = new ProductsSelectForReservation()
                {
                    ReservationId = reservationsId,
                    ProductId = productId
                };
                _db.ProductsSelectForReservations.Add(productsSelectedForAppointment);

            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction("AppointmentConfirmation", "ShoppingCart", new { Id = reservationsId });
        }

        public IActionResult Remove(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if(lstCartItems.Count > 0)
            {
                if (lstCartItems.Remove(id))
                {
                    lstCartItems.Remove(id);
                }


            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }

        // Get
        public IActionResult ReservationConfirmation (int id)
        {
            ShoppingCartVM.Reservations = _db.Reservations.Where(a => a.Id == id).FirstOrDefault();
            List<ProductsSelectForReservation> objProdList = _db.ProductsSelectForReservations.Where(p => p.ReservationId == id).ToList();

            foreach(ProductsSelectForReservation prodResObj in objProdList)
            {
                ShoppingCartVM.Products.Add(_db.Products.Include(p => p.ProductTypes).Include(p => p.Stores).Where(p => p.Id == prodResObj.ProductId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }


    }
}