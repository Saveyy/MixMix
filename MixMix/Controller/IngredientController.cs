using DataBase;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class IngredientController
    {
        private IngredientDB ingredientDB = new IngredientDB();
        private MeasurementDB measurementDB = new MeasurementDB();

        public Ingredient Create(Ingredient ingredient)
        {
            return ingredientDB.Insert(ingredient);
        }

        public Ingredient Find(int id)
        {
            return ingredientDB.Find(id);
        }

        public List<Ingredient> Find(string name)
        {
            return ingredientDB.Find(name);
        }

        public List<Ingredient> FindAllIngredients()
        {
            return ingredientDB.FindAll();
        }

        public bool Update(Ingredient ing)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Measurement FindMeasurement(int id)
        {
            return measurementDB.Find(id);
        }
        public Measurement FindMeasurement(string type)
        {
            return measurementDB.Find(type);
        }

        public List<Measurement> FindAllMeasurements()
        {
            return measurementDB.FindAll();
        }
    }
}
