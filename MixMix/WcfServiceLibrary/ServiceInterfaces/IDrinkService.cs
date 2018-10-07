using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.ServiceInterfaces
{
    [ServiceContract]
    public interface IDrinkService
    {
        [OperationContract]
        Dictionary<int, DrinkViewModel> GetAvailableDrinks(Bar bar);
        [OperationContract]
        double FindDrinkPriceById(int drinkId, int barId);
        [OperationContract]
        bool CheckDrinkName(string name);
        [OperationContract]
        Drink CreateDrink(Drink drink);
        [OperationContract]
        List<Ingredient> FindAllIngredients();
        [OperationContract]
        List<Measurement> FindAllMeasurements();
        [OperationContract]
        Ingredient FindIngredient(int id);
        [OperationContract]
        Measurement FindMeasurement(int id);
    }
}
