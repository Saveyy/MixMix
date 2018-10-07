using MixMixMVC.Models;
using MixMixMVC.CustomerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MixMixMVC.BarServiceRerefence;
using MixMixMVC.DrinkServiceReference;
using MixMixMVC.Helpers;

namespace MixMixMVC.Controllers
{
    public class DrinkController : Controller
    {
        // GET: Drink
        [AuthorizedLogin]
        public ActionResult Index(int barId, string message)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                BarServiceClient barProxy = serviceHelper.GetBarServiceClient();
                //Ambiguous reference her
                BarServiceRerefence.Bar foundBar = barProxy.Find(barId);
                DrinkServiceReference.Bar bar = new DrinkServiceReference.Bar();
                bar.ID = foundBar.ID;
                bar.Name = foundBar.Name;

                Dictionary<int, DrinkViewModel> drinks = drinkProxy.GetAvailableDrinks(bar);

                ViewBag.barId = barId;
                ViewBag.BarName = bar.Name;
                ViewBag.Message = message;


                return View(drinks);
            }
        }
        [AuthorizedLogin]
        public ActionResult DrinkName(int barId)
        {
            ViewBag.barId = barId;
            return View();
        }

        // POST: Drink/Create
        [HttpPost]
        [AuthorizedLogin]
        public ActionResult DrinkName(FormCollection collection, int barId)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                try
                {
                    string name = Convert.ToString(collection["DrinkName"]);
                    bool drinkNameExists = drinkProxy.CheckDrinkName(name);
                    if (drinkNameExists)
                    {
                        string message = "Drinken eksisterer allerede.";
                        return RedirectToAction("Index", new { barId = barId, message = message });
                    }

                    Session["createDrink"] = new DrinkIngredientViewModel();

                    ((DrinkIngredientViewModel)Session["createDrink"]).DrinkName = name;
                    ((DrinkIngredientViewModel)Session["createDrink"]).BarId = barId;

                    return RedirectToAction("DrinkIngredient");
                }
                catch
                {
                    string message = "Der skete desværre en fejl.";
                    return RedirectToAction("Index", new { barId = barId, message = message });
                }
            }
        }
        [AuthorizedLogin]
        public ActionResult DrinkIngredient()
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                DrinkIngredientViewModel drinkIngredientViewModel = new DrinkIngredientViewModel();
                drinkIngredientViewModel.Ingredients = drinkProxy.FindAllIngredients().ToList();
                drinkIngredientViewModel.Measurements = drinkProxy.FindAllMeasurements().ToList();
                return View(drinkIngredientViewModel);
            }
        }

        // POST: Drink/Create
        [HttpPost]
        [AuthorizedLogin]
        public ActionResult DrinkIngredient(FormCollection collection)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                try
                {
                    double quantity = Convert.ToDouble(collection["Quantity"]);

                    int ingredientId = Convert.ToInt32(collection["ddlIngredient"]);
                    int measurementId = Convert.ToInt32(collection["ddlMeasurement"]);

                    DrinkServiceReference.Ingredient ingredient = new DrinkServiceReference.Ingredient();
                    ingredient = drinkProxy.FindIngredient(ingredientId);

                    DrinkServiceReference.Measurement measurement = new DrinkServiceReference.Measurement();
                    measurement = drinkProxy.FindMeasurement(measurementId);

                    DrinkServiceReference.QuantifiedIngredient quantifiedIngredient = new DrinkServiceReference.QuantifiedIngredient();
                    quantifiedIngredient.Ingredient = ingredient;
                    quantifiedIngredient.Measurement = measurement;
                    quantifiedIngredient.Quantity = quantity;

                    if (((DrinkIngredientViewModel)Session["createDrink"]).QuantifiedIngredients == null)
                    {
                        ((DrinkIngredientViewModel)Session["createDrink"]).QuantifiedIngredients = new List<DrinkServiceReference.QuantifiedIngredient>();
                    }

                ((DrinkIngredientViewModel)Session["createDrink"]).QuantifiedIngredients.Add(quantifiedIngredient);

                    return RedirectToAction("DrinkIngredient");
                }
                catch
                {
                    return View();
                }
            }
        }

        [AuthorizedLogin]
        public ActionResult DrinkDecription()
        {
            return View();
        }

        // POST: Drink/Create
        [HttpPost]
        [AuthorizedLogin]
        public ActionResult DrinkDecription(FormCollection collection)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {
                DrinkServiceClient drinkProxy = serviceHelper.GetDrinkServiceClient();
                try
                {
                    DrinkServiceReference.Drink drink = new DrinkServiceReference.Drink();
                    drink.Ingredients = ((DrinkIngredientViewModel)Session["createDrink"]).QuantifiedIngredients.ToList();
                    drink.Names = new List<string> { ((DrinkIngredientViewModel)Session["createDrink"]).DrinkName };
                    drink.Recipe = Convert.ToString(collection["Recipe"]);
                    drinkProxy.CreateDrink(drink);

                    ViewBag.barId = ((DrinkIngredientViewModel)Session["createDrink"]).BarId;
                    string message = "Drinken er nu oprettet.";
                    return RedirectToAction("Index", new { barId = ViewBag.barId, message = message });
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
