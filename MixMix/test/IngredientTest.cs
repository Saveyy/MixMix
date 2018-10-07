using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class IngredientTest
    {

        IngredientController controller = new IngredientController();

        [TestInitialize]
        public void TestInitialize()
        {
           CleanUp();
        }
        
        [TestMethod]
        public void IngredientCreate()
        {
            //setup
            int preCount = TestHelper.CountTable("Ingredients");
            preCount = preCount + 1;
            Ingredient ingredient = new Ingredient() { Name = "TestCreate", Measurable = true, Alch = 20 };

            //act
            controller.Create(ingredient);
            int postCount = TestHelper.CountTable("Ingredients");
            //assert
            Assert.AreEqual(preCount, postCount, "Ingredient was not created successfully");
        }

        [TestMethod]
        public void IngredientFindById()
        {
            //setup
            Ingredient ingre = new Ingredient() { Name = "TestFind", Measurable = true, Alch = 20 };
            Ingredient expected = controller.Create(ingre);
            //act
            Ingredient actual = controller.Find(expected.Id);
            //assert
            Assert.AreEqual(expected, actual, "Ingredient wasn't found");
        }

        [TestCleanup]
        public void CleanUp() => TestHelper.DeleteAllInDB();


    }
}
