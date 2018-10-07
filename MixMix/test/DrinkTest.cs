using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase;
using System.Data.SqlClient;

namespace test
{
    [TestClass]
    public class DrinkTest
    {
        DrinkController controller = new DrinkController();
        Drink TestVodka = null;
        Drink TestJuice = null;
        Drink TestVodkaJuice = null;
        Ingredient TestVodkaIngre = null;
        Ingredient TestJuiceIngre = null;
        Bar bar = null;
        QuantifiedIngredient quantifiedIngredientVodka = null;
        QuantifiedIngredient quantifiedIngredientJuice = null;


        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }

        [TestMethod]
        public void DrinkCreate()
        {
            //setup
            int preCount = TestHelper.CountTable("Drinks");
            preCount = preCount + 3;    // +3 as there are 3 Drink objects that
                                        // get added to the DB in the method 
                                        // FillDataToTheDataBase(); 

            //act
            FillDataToTheDataBase();
            int postCount = TestHelper.CountTable("Drinks");

            //assert
            Assert.AreEqual(preCount, postCount, "Drink was not Created Successfully");
        }

        [TestMethod]
        public void CheckIfDrinkNameExists()
        {
            //setup
            DrinkDB drinkDB = new DrinkDB();
            List<String> names = new List<string>();
            names.Add("testJuiceVodka");

            Ingredient testVodka = new Ingredient("testVodka", true, 37);
            Ingredient testJuice = new Ingredient("testJuice", true, 0);

            IngredientDB ingredientDB = new IngredientDB();
            ingredientDB.Insert(testVodka);
            ingredientDB.Insert(testJuice);

            MeasurementDB measurementDB = new MeasurementDB();

            Measurement measurement = measurementDB.Find("cl");

            QuantifiedIngredient quantifiedIngredientVodka = new QuantifiedIngredient(8, testVodka, measurement);
            QuantifiedIngredient quantifiedIngredientJuice = new QuantifiedIngredient(16, testJuice, measurement);

            List<QuantifiedIngredient> quantifiedIngredientsList = new List<QuantifiedIngredient>();
            quantifiedIngredientsList.Add(quantifiedIngredientVodka);
            quantifiedIngredientsList.Add(quantifiedIngredientJuice);

            Drink drink = new Drink(names, quantifiedIngredientsList, "Bland");

            Drink drinkWithId = drinkDB.Insert(drink);

            drinkDB.InsertDrinkNames(names.First(), drinkWithId.Id);

            QuantifiedIngredientDB quantifiedIngredientDB = new QuantifiedIngredientDB();
            quantifiedIngredientDB.Insert(quantifiedIngredientsList, drinkWithId.Id);

            //act
            bool drinkNameExists = drinkDB.CheckDrinkName(drinkWithId.Names.First());

            //assert
            Assert.AreEqual(drinkNameExists, true);
        }

        [TestMethod]
        public void FindByName()
        {
            //setup
            FillDataToTheDataBase();
            //act
            bool success = controller.CheckDrinkName("TestNameVodka");
            //assert

            Assert.AreEqual(success, true, "Drink wasn't found");
        }
        [TestMethod]
        public void TestFindDrinkPrice()
        {
            FillDataToTheDataBase();
            //setup

            DrinkController dc = new DrinkController();
            BarController bc = new BarController();
            

            bc.AddStockToBar(bar, new Stock() { Ingredient = TestVodkaIngre, Quantity = 100 });
            bc.AddStockToBar(bar, new Stock() { Ingredient = TestJuiceIngre, Quantity = 100 });
            int vodkaSellingPrice = 50;
            int juiceSellingPrice = 10;
            controller.InsertDrinkPrice(bar, new Price() { BuyingPrice = 10, SellingPrice = vodkaSellingPrice, Ingredient = TestVodkaIngre });
            controller.InsertDrinkPrice(bar, new Price() { BuyingPrice = 2, SellingPrice = juiceSellingPrice, Ingredient = TestJuiceIngre });

            //act
            double priceForVodkaJuice = controller.FindDrinkPriceById(TestVodkaJuice.Id, bar.ID);
            double ActualPriceVodkaJuice = 0;
            QuantifiedIngredient QIngreVodka = TestVodkaJuice.Ingredients.Where(e => e.Ingredient.Id == TestVodkaIngre.Id).First();
            QuantifiedIngredient QIngreJuice = TestVodkaJuice.Ingredients.Where(e => e.Ingredient.Id == TestJuiceIngre.Id).First();
            ActualPriceVodkaJuice += vodkaSellingPrice * QIngreVodka.Quantity;
            ActualPriceVodkaJuice += juiceSellingPrice * QIngreJuice.Quantity;

            //Assert
            Assert.AreEqual(ActualPriceVodkaJuice, priceForVodkaJuice);
        }

        [TestMethod]
        public void FindAllAvailableDrinks()
        {
            //setup
            FillDataToTheDataBase();

            DrinkController dc = new DrinkController();
            BarController bc = new BarController();
            bc.AddStockToBar(bar, new Stock() { Ingredient = TestVodkaIngre, Quantity = 100 });


            //Act
            List<DrinkViewModel> found = controller.FindAllAvailableDrinks(bar).Values.ToList();

            //assert
            Assert.AreEqual(found.Count, 1);

            //Setup 2
            bc.AddStockToBar(bar, new Stock() { Ingredient = TestJuiceIngre, Quantity = 100 });

            //Act 2
            List<DrinkViewModel> found2 = controller.FindAllAvailableDrinks(bar).Values.ToList();

            //assert 2
            Assert.AreEqual(found2.Count, 3);
        }

        [TestMethod]
        public void FindallIngredients()
        {
            //setup
            FillDataToTheDataBase();
            //act
            var found = controller.FindAllIngredients();

            //assert
            Assert.AreEqual(found.Count, TestHelper.CountAllIngredients(), 0, "FindAllIngredients Failed");
        }

        [TestMethod]
        public void FindAllMeasurements()
        {
            //setup
            // - Measurements are created by system administrator when system is initialised

            //act
            var found = controller.FindAllMeasurements();

            //assert
            Assert.AreEqual(found.Count, TestHelper.FindAllMeasurements(), "Not All Measurements were found");
        }


        [TestCleanup]
        public void CleanUp()
        {
            controller = new DrinkController();
            TestVodka = null;
            TestJuice = null;
            TestVodkaJuice = null;
            TestVodkaIngre = null;
            TestJuiceIngre = null;
            bar = null;
            quantifiedIngredientVodka = null;
            quantifiedIngredientJuice = null;
            TestHelper.DeleteAllInDB();

        }



        private void FillDataToTheDataBase()
        {
            #region Make bar
            BarController bc = new BarController();
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            this.bar = bc.Create(bar, m.Id, "TestPassword");
            #endregion

            IngredientController ingredientController = new IngredientController();

            List<Drink> drinks = new List<Drink>();


            Ingredient vodka = new Ingredient
            {
                Name = "TestVodka",
                Measurable = true,
                Alch = 37.5
            };

            Ingredient juice = new Ingredient
            {
                Name = "TestJuice",
                Measurable = true,
                Alch = 0
            };

            TestVodkaIngre = ingredientController.Create(vodka);
            TestJuiceIngre = ingredientController.Create(juice);


            quantifiedIngredientVodka = new QuantifiedIngredient
            {
                Quantity = 6,
                Ingredient = ingredientController.Find("TestVodka").First(), //We would like it to throw an exception here, as it's a test
                Measurement = ingredientController.FindMeasurement("cl")
            };

            quantifiedIngredientJuice = new QuantifiedIngredient
            {
                Quantity = 2,
                Ingredient = ingredientController.Find("TestJuice").First(), //We would like it to throw an exception here, as it's a test
                Measurement = ingredientController.FindMeasurement("cl")
            };

            Drink drinkVodkaJuice = new Drink
            {
                Recipe = "TestVodkaJuiceRecipe",
                Names = new List<string>() { "TestName1", "TestName2" },
                Ingredients = new List<QuantifiedIngredient>() { quantifiedIngredientJuice, quantifiedIngredientVodka }
            };

            Drink drinkVodka = new Drink
            {
                Recipe = "TestVodkaRecipe",
                Names = new List<string>() { "TestNameVodka" },
                Ingredients = new List<QuantifiedIngredient>() { quantifiedIngredientVodka }
            };

            Drink drinkJuice = new Drink
            {
                Recipe = "TestJuiceRecipe",
                Id = 3,
                Names = new List<string>() { "TestJuice", "TestAppelsinjuice" },
                Ingredients = new List<QuantifiedIngredient>() { quantifiedIngredientJuice }
            };

            TestVodka = controller.Create(drinkVodka);
            TestJuice = controller.Create(drinkJuice);
            TestVodkaJuice = controller.Create(drinkVodkaJuice);
        }
    }
}
