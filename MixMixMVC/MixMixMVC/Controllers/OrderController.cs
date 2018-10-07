using MixMixMVC.DrinkServiceReference;
using MixMixMVC.Helpers;
using MixMixMVC.Models;
using MixMixMVC.OrderServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixMixMVC.Controllers
{
    public class OrderController : Controller
    {
        /*
        OrderServiceClient orderProxy = new OrderServiceClient();
        DrinkServiceClient drinkProxy = new DrinkServiceClient();
        */

        [HttpPost]
        [AuthorizedLogin]
        public ActionResult AddToCart(int drinkid, int barid, string name, string ingredients)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                OrderServiceClient orderProxy = serviceHelper.GetOrderServiceClient();
                bool success = orderProxy.DecrementStock(drinkid, barid);
                string message;
                if (success)
                {
                    double price = drinkProxy.FindDrinkPriceById(drinkid, barid);
                    message = $"Drinken er købt til: {price} kr.";
                    if (Session["shoppingcart"] == null)
                    {
                        Session["shoppingcart"] = new List<OrderDrinkViewModel>();
                    }
                    List<OrderDrinkViewModel> list = (List<OrderDrinkViewModel>)Session["shoppingcart"];
                    List<OrderDrinkViewModel> drinkIdList = list.Where(x => x.DrinkId == drinkid).ToList();
                    if (!drinkIdList.Any())
                    {
                        OrderDrinkViewModel drink = new OrderDrinkViewModel();
                        drink.Names = new List<string>();
                        drink.BarId = barid;
                        drink.DrinkId = drinkid;
                        drink.Ingredients = ingredients;
                        drink.Names.Add(name);
                        drink.Subtotal = price;
                        drink.Quantity = 1;
                        ((List<OrderDrinkViewModel>)Session["shoppingcart"]).Add(drink);
                    }
                    else
                    {
                        if (!drinkIdList.First().Names.Contains(name))
                        {
                            drinkIdList.First().Names.Add(name);
                        }
                        drinkIdList.First().Subtotal += price;
                        drinkIdList.First().Quantity++;
                    }


                    return RedirectToAction("index", "drink", new { barId = barid, message = message });
                }
                else
                {
                    message = "drinken er desværre lige blevet udsolgt :(";
                    return RedirectToAction("index", "drink", new { barId = barid, message = message });
                }
            }
        }
        [AuthorizedLogin]
        public ActionResult ShoppingCart()
        {
            List<OrderDrinkViewModel> list = (List<OrderDrinkViewModel>)Session["shoppingcart"];
            return View(list);
        }
        [AuthorizedLogin]
        public ActionResult CreateOrder()
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                OrderServiceClient orderProxy = serviceHelper.GetOrderServiceClient();
                Order order = new Order();
                order.OrderLines = new List<OrderLine>();
                List<OrderDrinkViewModel> drinks = (List<OrderDrinkViewModel>)Session["shoppingcart"];
                foreach (OrderDrinkViewModel drink in drinks)
                {
                    OrderLine ol = new OrderLine
                    {
                        Drink = new OrderServiceReference.Drink { Id = drink.DrinkId },
                        Quantity = drink.Quantity,
                        Subtotal = drink.Subtotal,
                        Names = drink.Names,
                    };
                    order.OrderLines.Add(ol);
                    order.Status = Status.NotDone;
                    order.OrderTime = DateTime.Now;
                    order.Bar = new OrderServiceReference.Bar();
                    order.Bar.ID = drink.BarId;
                    order.Customer = new Customer();
                    order.Customer.Id = AuthHelper.CurrentUser.Id;
                }

                ViewBag.OrderNumber = orderProxy.CreateOrder(order);
                Session["shoppingcart"] = new List<OrderDrinkViewModel>();
                return View(ViewBag.OrderNumber);
            }
        }
    }
}
