using DataBase;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class DrinkController
    {
        DrinkDB drinkDB = new DrinkDB();
        QuantifiedIngredientDB quantifiedIngredientDB = new QuantifiedIngredientDB();
        IngredientDB ingredientDB = new IngredientDB();
        IngredientController ingredientController = new IngredientController();
        MeasurementDB measurementDB = new MeasurementDB();

        public Drink Create(Drink drink)
        {
            Drink drinkWithId = drinkDB.Insert(drink);
            drinkDB.InsertDrinkNames(drinkWithId.Names.First(), drinkWithId.Id);
            quantifiedIngredientDB.Insert(drinkWithId.Ingredients, drinkWithId.Id);
            return drinkWithId;
        }

        public List<Ingredient> FindAllIngredients()
        {
            return ingredientDB.FindAll();
        }

        public Ingredient FindIngredient(int id)
        {
            return ingredientController.Find(id);
        }

        public List<Measurement> FindAllMeasurements()
        {
            return measurementDB.FindAll();
        }

        public Measurement FindMeasurement(int id)
        {
            return measurementDB.Find(id);
        }

        public bool CheckDrinkName(string name)
        {
            return drinkDB.CheckDrinkName(name);
        }

        public double FindDrinkPriceById(int drinkId, int barId)
        {
            return drinkDB.FindDrinkPriceById(drinkId, barId);
        }

        public void InsertDrinkPrice(Bar bar, Price price)
        {
            drinkDB.InsertPrice(bar, price);
        }

        public Dictionary<int, DrinkViewModel> FindAllAvailableDrinks(Bar bar)
        {
            Dictionary<int, DrinkViewModel> availableDrinks = drinkDB.FindAllAvailableDrinks(bar);
            foreach (var drink in availableDrinks)
            {
                drink.Value.Price = drinkDB.FindDrinkPriceById(drink.Key, bar.ID);
            }
            return availableDrinks;
        } 
        
    }
}
