using Natroral.Core.Contracts;
using Natroral.Core.Models;
using Natroral.DataAccess.SQL;
using Natroral.Services;
using System.Linq;
using System.Web.Mvc;

namespace Natroral.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }

        // GET: Basket2
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditBasketItem(string Id)
        {
            BasketItem item = new BasketItem();
            item = basketService.FindBaketItem(this.HttpContext, Id);

            string prodId = item.ProductId;

            DataContext dbContext = new DataContext();
            Product product = new Product();

            product = dbContext.Products.Where(x => x.Id == prodId).FirstOrDefault();

            TempData["ProdName"] = product.Name;

            return View(item);
        }

        [HttpPost]
        public ActionResult EditBasketItem(BasketItem item, string Id)
        {
            basketService.EditBaketItem(this.HttpContext, item, Id);

            if (item.Quantity == 0)
            {
                basketService.RemoveFromBasket(this.HttpContext, Id);
            }

            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ConfirmOrder()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    Street2 = customer.Street2,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Country = customer.Country,
                    ZipCode = customer.ZipCode
                };


                // calculate shipping cost
                var basketSummary = basketService.GetBasketSummary(this.HttpContext);
                int itemCount = basketSummary.BasketCount;
                string country = order.Country;

                if (country.ToLower() == "us" || country.ToLower().Contains("united states"))
                {
                    if (itemCount <= 2)
                    {
                        order.Shipping = 8;
                    }
                    else if (itemCount > 2 && itemCount <= 6)
                    {
                        order.Shipping = 10;
                    }
                    else
                    {
                        order.Shipping = 16;
                    }
                }
                else
                {
                    if (itemCount <= 3)
                    {
                        order.Shipping = 10;
                    }
                    else if (itemCount > 3 && itemCount <= 8)
                    {
                        order.Shipping = 16;
                    }
                    else
                    {
                        order.Shipping = 20;
                    }
                }

                order.OrderTotal = basketSummary.BasketTotal + order.Shipping;

                return View(order);
            }
            else
            {
                TempData[("CustomerError")] = "The logged in customer is not found, or the system has an error.";
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ConfirmOrder(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.Email = User.Identity.Name;

            order.OrderStatus = "Order Created";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Checkout", new { OrderId = order.Id });
        }

        [Authorize]
        public ActionResult Checkout(string OrderId)
        {
            Order order = new Order();
            order = orderService.GetOrder(OrderId);

            if (order != null)
            {
                return View(order);
            }
            else
            {
                TempData[("OrderError")] = "The requested order is not found, or Order Id is null.";
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            //payment processed   

            order.OrderStatus = "Payment Processed";

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}