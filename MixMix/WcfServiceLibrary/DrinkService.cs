using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Entities;
using WcfServiceLibrary.ServiceInterfaces;

namespace WcfServiceLibrary
{
    public class DrinkService : IDrinkService
    {
        DrinkController drinkController = new DrinkController();

        public bool CheckDrinkName(string name)
        {
            return drinkController.CheckDrinkName(name);
        }

        public Drink CreateDrink(Drink drink)
        {
            return drinkController.Create(drink);
        }

        public List<Ingredient> FindAllIngredients()
        {
            return drinkController.FindAllIngredients();
        }

        public List<Measurement> FindAllMeasurements()
        {
            return drinkController.FindAllMeasurements();
        }

        public double FindDrinkPriceById(int drinkId, int barId)
        {
            return drinkController.FindDrinkPriceById(drinkId, barId);
        }

        public Ingredient FindIngredient(int id)
        {
            return drinkController.FindIngredient(id);
        }

        public Measurement FindMeasurement(int id)
        {
            return drinkController.FindMeasurement(id);
        }

        public Dictionary<int, DrinkViewModel> GetAvailableDrinks(Bar bar)
        {
            return drinkController.FindAllAvailableDrinks(bar);
        }


    }
}
