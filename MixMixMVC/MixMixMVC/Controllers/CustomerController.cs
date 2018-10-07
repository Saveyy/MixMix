using MixMixMVC.AuthService;
using MixMixMVC.BarServiceRerefence;
using MixMixMVC.CustomerServiceReference;
using MixMixMVC.Exceptions;
using MixMixMVC.Helpers;
using MixMixMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixMixMVC.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer/Details/5
        [AuthorizedLogin]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {

                try
                {
                    string name = Convert.ToString(collection["Name"]);
                    string email = Convert.ToString(collection["Email"]);
                    string password = Convert.ToString(collection["Password"]);
                    string username = Convert.ToString(collection["Username"]);
                    string phonenumber = Convert.ToString(collection["Phonenumber"]);

                    CustomerCreateServiceReference.Customer customer = new CustomerCreateServiceReference.Customer
                    {
                        Name = name,
                        Email = email,
                        Username = username,
                        PhoneNumber = phonenumber
                    };
                    serviceHelper.GetCreateCustomerServiceClient().CreateCustomer(customer, password);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Customer/Edit/5
        [AuthorizedLogin]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (customer.Username.Equals(Helpers.AuthHelper.CurrentUser.Username))
            {
                return View(customer);
            }
            else
            {
                return View();
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [AuthorizedLogin]
        public ActionResult Edit(CustomerViewModel customer, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Customer/Delete/5
        [AuthorizedLogin]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [AuthorizedLogin]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {

                try
                {
                    LoginViewModel lvm = new LoginViewModel
                    {
                        Username = Convert.ToString(collection["Username"]),
                        Password = Convert.ToString(collection["Password"])
                    };

                    AuthServiceClient authClient = serviceHelper.GetAuthServiceClient();

                    if (authClient.Login(lvm.Username, lvm.Password))
                    {
                        AuthHelper.Login(lvm);
                        lvm.Id = serviceHelper.GetCustomerServiceClient().FindCustomerByUsername(lvm.Username).Id;
                    }
                    else
                    {
                        throw new InvalidLoginException();
                    }

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }
        }


        [AuthorizedLogin]
        public ActionResult BarList()
        {
            List<Bar> bars = new List<Bar>();
            return View(bars);
        }

        [HttpPost]
        [AuthorizedLogin]
        public ActionResult BarList(FormCollection collection, string zipcode)
        {
            using (ServiceHelper serviceHelper = new ServiceHelper())
            {

                BarServiceClient barProxy = serviceHelper.GetBarServiceClient();
                LoginViewModel lvm = AuthHelper.CurrentUser;
                return View(barProxy.FindAll(zipcode));
            }
        }

    }
}
