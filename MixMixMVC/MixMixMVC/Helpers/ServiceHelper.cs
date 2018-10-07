using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MixMixMVC.AuthService;
using MixMixMVC.DrinkServiceReference;
using MixMixMVC.BarServiceRerefence;
using MixMixMVC.CustomerServiceReference;
using MixMixMVC.OrderServiceReference;
using MixMixMVC.CustomerCreateServiceReference;
using MixMixMVC.Exceptions;

namespace MixMixMVC.Helpers
{
    public class ServiceHelper : IDisposable
    {
        private DrinkServiceClient _drinkClient;
        private BarServiceClient _barClient;
        private CustomerServiceClient _customerClient;
        private OrderServiceClient _orderClient;

        public DrinkServiceClient GetDrinkServiceClient()
        {
            //In case session ran out
            if (AuthHelper.CurrentUser == null && _drinkClient != null)
            {
                (_drinkClient as IDisposable).Dispose();
                _drinkClient = null;
            }

            if (AuthHelper.CurrentUser != null && _drinkClient == null)
            {
                _drinkClient = new DrinkServiceClient("drinkServiceHttps");
                _drinkClient.ClientCredentials.UserName.UserName = AuthHelper.CurrentUser.Username;
                _drinkClient.ClientCredentials.UserName.Password = AuthHelper.CurrentUser.Password;
            }
            return _drinkClient;
        }

        public BarServiceClient GetBarServiceClient()
        {
            //In case session ran out
            if (AuthHelper.CurrentUser == null && _barClient != null)
            {
                (_barClient as IDisposable).Dispose();
                _barClient = null;
            }

            if (AuthHelper.CurrentUser != null && _barClient == null)
            {
                _barClient = new BarServiceClient("barServiceHttp");
                _barClient.ClientCredentials.UserName.UserName = AuthHelper.CurrentUser.Username;
                _barClient.ClientCredentials.UserName.Password = AuthHelper.CurrentUser.Password;
            }
            return _barClient;
        }

        public CustomerServiceClient GetCustomerServiceClient()
        {
            //In case session ran out
            if (AuthHelper.CurrentUser == null && _customerClient != null)
            {
                (_barClient as IDisposable).Dispose();
                _barClient = null;
            }

            if (AuthHelper.CurrentUser != null && _customerClient == null)
            {
                _customerClient = new CustomerServiceClient("customerServiceHttp");
                _customerClient.ClientCredentials.UserName.UserName = AuthHelper.CurrentUser.Username;
                _customerClient.ClientCredentials.UserName.Password = AuthHelper.CurrentUser.Password;
            }
            return _customerClient;

        }
        public OrderServiceClient GetOrderServiceClient()
        {
            //In case session ran out
            if (AuthHelper.CurrentUser == null && _orderClient != null)
            {
                (_barClient as IDisposable).Dispose();
                _barClient = null;
            }

            if (AuthHelper.CurrentUser != null && _orderClient == null)
            {
                _orderClient = new OrderServiceClient("orderServiceHttp");
                _orderClient.ClientCredentials.UserName.UserName = AuthHelper.CurrentUser.Username;
                _orderClient.ClientCredentials.UserName.Password = AuthHelper.CurrentUser.Password;
            }
            return _orderClient;
        }


        public CreateCustomerServiceClient GetCreateCustomerServiceClient()
        {
            return new CreateCustomerServiceClient("customerCreateService");
        }

        public AuthServiceClient GetAuthServiceClient()
        {
            return new AuthServiceClient("WSHttpBinding_IAuthService");
        }
        //Disposer alel clienter og null them
        //
        public void Dispose()
        {
            _barClient = null;
            _drinkClient = null;
            _orderClient = null;
            _customerClient = null;
        }
    }
}

