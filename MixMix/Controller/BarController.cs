using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataBase;

namespace Controller
{
    public class BarController
    {
        private readonly LocationController locationController = new LocationController();
        private BarDB barDB = BarDB.Instance;
        private IngredientController ingredientController = new IngredientController();

        public Bar Create(int managerId, string name, Address address, string phonenumber, string email, string username, string password)
        {
            Bar bar = new Bar(name, address, phonenumber, email, username);

            return barDB.Create(bar, managerId, password);
        }

        public bool DecrementStock(int drinkId, int barId)
        {

            return barDB.DecrementStock(drinkId, barId);
        }

        public Bar Create(Bar bar, int managerId, string password)
        {
            return barDB.Create(bar, managerId, password);
        }

        public bool CreateStock(int barId, double quantity, Ingredient ingredient, int? measurementId)
        {
            Measurement measurement = null;
            if (measurementId != null && ingredient.Measurable)
            {
                measurement = ingredientController.FindMeasurement(measurementId.Value);
                quantity = quantity * measurement.Proportion;
            }

            Stock stock = new Stock
            {
                Ingredient = ingredient,
                Quantity = quantity
            };
            return barDB.CreateStock(barId, stock);
        }

        public bool Update(int barId, string name, Address address, string phonenumber, string email, string username)
        {
            Bar barToUpdate = new Bar(name, address, phonenumber, email, username, barId);
            return barDB.Update(barToUpdate);
        }

        public List<Stock> GetAllStocks(int barId)
        {
            return barDB.GetAllStocks(barId);
        }



        public Bar Find(int barId)
        {
            return barDB.Find(barId);
        }

        public bool Update(Bar bar)
        {
            return barDB.Update(bar);
        }


        public List<Bar> GetBarsByManagerId(int managerId)
        {
            return barDB.GetBarsByManagerId(managerId);
        }

        public bool Delete(int id)
        {
            return barDB.Delete(id);
        }

        public Bar Login(string username, string password)
        {
            return barDB.Login(username, password);
        }

        public List<Bar> FindAll(string zipcode)
        {
            return barDB.FindAll(zipcode);
        }

        public bool AddStockToBar(Bar bar, Stock stock)
        {
            return barDB.CreateStock(bar.ID, stock);
        }

        public double FindIngredientPrice(int ingredientID, int barID)
        {
            return barDB.FindIngredientPrice(ingredientID, barID);
        }

        public bool UpdateStock(List<Stock> stocks, int barid)
        {
            return barDB.UpdateStock(stocks, barid);
        }
    }
}
