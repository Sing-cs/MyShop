using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService OrderService)
        {
            this.orderService = OrderService;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }

        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StateList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "OrderShipped",
                "Order Completed"
            };

            Order order = orderService.GetOrder(Id);

            if (order != null)
            {
                return View(order);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string Id)
        {
            Order order = orderService.GetOrder(Id);
            order.OrderState = updatedOrder.OrderState;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}