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
    public class BarService : IBarService
    {
        BarController barController = new BarController();
        IngredientController ingredientController = new IngredientController();

        public Bar Create(int managerId, string name, Address address, string phonenumber, string email, string username, string password)
        {
            return barController.Create(managerId, name, address, phonenumber, email, username, password);
        }

        public Bar Find(int barId)
        {
            return barController.Find(barId);
        }

        public List<Bar> GetBarsByManagerId(int managerId)
        {
            return barController.GetBarsByManagerId(managerId);
        }

        public bool Update(int barId, string name, Address address, string phonenumber, string email, string username)
        {
            return barController.Update(barId, name, address, phonenumber, email, username);
        }

        public List<Bar> FindAll(string zipcode)
        {
            return barController.FindAll(zipcode);
        }

        public bool Delete(int id)
        {
            return barController.Delete(id);
        }

        public void CreateStock(int barId, double quantity, Ingredient ingredient, int measurementId)
        {
            barController.CreateStock(barId, quantity, ingredient, measurementId);
        }

        public void CreateNonMeasurableStock(int barId, double quantity, Ingredient ingredient)
        {
            barController.CreateStock(barId, quantity, ingredient, null);
        }

        public List<Ingredient> GetAllIngredients()
        {
            return ingredientController.FindAllIngredients();
        }

        public List<Measurement> GetAllMeasurements()
        {
            return ingredientController.FindAllMeasurements();
        }

        public List<Stock> GetAllStocks(int barId)
        {
            return barController.GetAllStocks(barId);
        }

        public bool UpdateStock(List<Stock> stocks, int barid)
        {
            return barController.UpdateStock(stocks, barid);
        }

        public Ingredient FindIngredient(int ingredientId)
        {
            return ingredientController.Find(ingredientId);
        }

        public Measurement FindMeasurement(int measurementId)
        {
            return ingredientController.FindMeasurement(measurementId);
        }
    }
}