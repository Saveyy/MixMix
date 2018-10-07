using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entities;

namespace WcfServiceLibrary.ServiceInterfaces
{
    [ServiceContract]
    public interface IBarService
    {
        [OperationContract]
        Bar Create(int managerId, string name, Address address, string phonenumber, string email, string username, string password);

        [OperationContract]
        Bar Find(int barId);

        [OperationContract]
        bool Update(int barId, string name, Address address, string phonenumber, string email, string username);

        [OperationContract]
        List<Bar> GetBarsByManagerId(int managerId);

        [OperationContract]
        List<Bar> FindAll(string zipcode);

        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        void CreateStock(int barId, double quantity, Ingredient ingredient, int measurementId);
        [OperationContract]
        void CreateNonMeasurableStock(int barId, double quantity, Ingredient ingredient);


        [OperationContract]
        List<Ingredient> GetAllIngredients();

        [OperationContract]
        List<Measurement> GetAllMeasurements();

        [OperationContract]
        List<Stock> GetAllStocks(int barId);

        [OperationContract]
        bool UpdateStock(List<Stock> stock, int barid);
        [OperationContract]
        Ingredient FindIngredient(int ingredientId);

        [OperationContract]
        Measurement FindMeasurement(int measurementId);
    }

}
