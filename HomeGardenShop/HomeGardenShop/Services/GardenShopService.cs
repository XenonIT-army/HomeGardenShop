using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using HomeGardenShop.Helps;
using HomeGardenShop.Models;
using HomeGardenShopServer;
using Xamarin.Forms;

namespace HomeGardenShop.Services
{
    public class GardenShopService
    {
        private HomeGardenShopServer.Greeter.GreeterClient _greeterClient;
        private GrpcWebHandler _httpHandler;
        private GrpcChannel _channel;
        public GardenShopService()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12;

                //AppContext.SetSwitch("Microsoft.AspNetCore.Server.Kestrel.EnableWindows81Http2", true);
                //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport",true);


                //debug on  emulator
                //_httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
                // _httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });

                //var serverAddress = (Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:5000" : "https://192.168.0.102");

                //var serverAddress = (Device.RuntimePlatform == Device.Android ? "https://192.168.0.102" : "https://192.168.0.102");

                var serverAddress = (Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:5000" : "https://185.250.23.158:5000");

                var path = Xamarin.Forms.DependencyService.Get<IPath>().GetPath("cert.pfx");

                var sert = new X509Certificate2(path, "2904");
                var httpHandler = new HttpClientHandler();
                //httpHandler.ClientCertificates.Add(sert);
                _httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, httpHandler);
                //httpHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                // Return true to allow certificates that are untrusted/invalid
                httpHandler.ServerCertificateCustomValidationCallback =
                 HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                // httpHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls12;

                var opt = new GrpcChannelOptions();
                opt.HttpHandler = _httpHandler;
                _channel = GrpcChannel.ForAddress($"{serverAddress}", opt);

                //var channel = GrpcChannel.ForAddress($"{serverAddress}", opt);



                //var channel = new Channel($"{serverAddress}",ChannelCredentials.Insecure);
                //var credentials = CallCredentials.FromInterceptor((context, metadata) => Task.CompletedTask);
                //var channel = new Channel($"{serverAddress}", ChannelCredentials.Create(new SslCredentials(), credentials));
                var channel = GrpcChannel.ForAddress($"{serverAddress}", new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(httpHandler)
                });

                _greeterClient = new HomeGardenShopServer.Greeter.GreeterClient(channel);


                   // httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb,
                //new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; } });

               
                //var req = new HttpRequestMessage(HttpMethod.Get, _myEndpoint) { Version = new Version(2, 0) };
                //var response = await _httpClient.SendAsync(req).ConfigureAwait(false);
                //_channel = GrpcChannel.ForAddress($"{serverAddress}", new GrpcChannelOptions
                //{
                //    HttpHandler = new GrpcWebHandler(_httpHandler)
                //});


                //_greeterClient = new HomeGardenShopServer.Greeter.GreeterClient(_channel);
            }
            catch (Exception ex)
            {

            }
        }

       

        public bool IsStart()
        {
            if (_greeterClient != null)
            {
                return true;
            }
            else
                return false;
        }
        public async Task<List<Product>> GetProductsAsync(string request)
        {
            List<Product> products = new List<Product>();
            try
            {
                CancellationToken token = new CancellationToken();
                using (var call = _greeterClient.ListProducts(new HomeGardenShopServer.ProductsRequest { Language = request }))
                {
                    while (await call.ResponseStream.MoveNext(token))
                    {
                        var item = call.ResponseStream.Current;

                        products.Add(new Product { Id = item.Id, Name = item.Name, AllCount = item.Count, Price = item.Price, CategoryId = item.CategoryId, DiscountPrice = item.DiscountPrice, Description = item.Description, Image = item.Image });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            //using (var call = _greeterClient.GetProductsAsync(new ProductsRequest { Name = hello }))
            //{
            //    var res = call.ResponseAsync.Result;
            //}
            //var result =   _greeterClient.ListProducts(new HomeGardenShopServer.ProductsRequest { Name = hello });

            return products;
        }
        public async Task<List<Category>> GetCategorysAsync(string request)
        {
            List<Category> category = new List<Category>();
            try
            {
                CancellationToken token = new CancellationToken();
                using (var call = _greeterClient.ListCategorys(new HomeGardenShopServer.CategorysRequest { Language = request }))
                {
                    while (await call.ResponseStream.MoveNext(token))
                    {
                        var item = call.ResponseStream.Current;

                        category.Add(new Category { Id = item.Id, Name = item.Name });
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return category;
        }

        public async Task<bool> MakeAnOrderAsync(Order order)
        {
            bool result = false;
            RepeatedField<ProductGrpc> collection = new RepeatedField<ProductGrpc>();
            try
            {
                CancellationToken token = new CancellationToken();
                foreach (var item in order.Products)
                {
                    double price;
                    if (item.DiscountPrice > 0)
                    {
                        price = item.DiscountPrice;
                    }
                    else
                        price = item.Price;
                    collection.Add(new ProductGrpc { Count = item.Count, Id = item.Id, Price = price });
                }



                using (var call = _greeterClient.MakeAnOrder(new HomeGardenShopServer.MakeAnOrderRequest
                {
                    Products = { collection },
                    OrderId = order.Id,
                    StatusId = order.StatusId,
                    Sum = order.Sum,
                    UserId = order.UserId
                }))
                {
                    while (await call.ResponseStream.MoveNext(token))
                    {
                        var res = call.ResponseStream.Current;
                        if (res.StatusId == (int)OrderStatus.InProcess)
                        {
                            result = true;
                        }
                        else
                            result = false;

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public Task<List<Order>> GetListOrdersAsync(string userId)
        {
            bool result = false;
            List<Order> orders = new List<Order>();
            CancellationToken token = new CancellationToken();
            try
            {
                using (var call = _greeterClient.ListOrdersAsync(new HomeGardenShopServer.ListOrdersRequest { UserId = userId }))
                {
                    var responce = call.ResponseAsync.Result;
                    
                    foreach (var item in responce.Orders)
                    {
                        List<Product> products = new List<Product>();
                        DateTime date = DateTime.MinValue;
                        foreach (var prod in item.Products)
                        {
                            products.Add(new Product { Id = prod.Id, Name = prod.Name, Count = prod.Count, Price = prod.Price, CategoryId = prod.CategoryId, DiscountPrice = prod.DiscountPrice, Description = prod.Description, Image = prod.Image });
                        }

                        try
                        {
                            date = DateTime.ParseExact(item.DateTime, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch 
                        {
                        }
                        orders.Add(new Order { Id = item.OrderId, StatusId = item.StatusId, Sum = item.Sum, UserId = item.UserId, DataTime = date, Products = products });
                    }
                }

                //using (var call = _greeterClient.ListOrders(new HomeGardenShopServer.ListOrdersRequest { UserId = userId }))
                //{

                //    while (await call.ResponseStream.MoveNext(token))
                //    {
                //        var res = call.ResponseStream.Current;
                //        List<Product> products = new List<Product>();
                //        foreach (var item in res.Products)
                //        {
                //            products.Add(new Product { Id = item.Id, Name = item.Name, Count = item.Count, Price = item.Price, CategoryId = item.CategoryId, DiscountPrice = item.DiscountPrice, Description = item.Description, Image = item.Image });
                //        }

                //        orders.Add(new Order { Id = res.OrderId, StatusId = res.StatusId, Sum = res.Sum, UserId = res.UserId, DataTime = DateTime.Parse(res.DateTime), Products = products });

                //    }
                //}
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }



            return Task.FromResult(orders);
        }

        public Task<bool> CancelOrderAsync(Order order)
        {
            bool result = false;
            try
            {
                CancellationToken token = new CancellationToken();

                using (var call = _greeterClient.CancelOrderAsync(new HomeGardenShopServer.CancelOrderRequest
                {
                    OrderId = order.Id,
                    StatusId = order.StatusId,
                    UserId = order.UserId
                }))
                {
                    var responce = call.ResponseAsync.Result;
                    result = responce.IsCancel;
                }

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(result);
        }

        public async Task<List<News>> GetListNews(string name)
        {
            bool result = false;
            List<News> news = new List<News>();
            CancellationToken token = new CancellationToken();
            try
            {
                using (var call = _greeterClient.ListNews(new HomeGardenShopServer.NewsRequest { Language = name }))
                {

                    while (await call.ResponseStream.MoveNext(token))
                    {
                        var item = call.ResponseStream.Current;

                        news.Add(new News { Id = item.Id, Name = item.Name, Description = item.Description, DataTime = DateTime.Parse(item.DateTime), Image = item.Image });
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }



            return news;
        }

        public Task<bool> RegistrUser(User user)
        {
            bool result = false;
            try
            {
                CancellationToken token = new CancellationToken();

                using (var call = _greeterClient.RegistrUserAsync(new HomeGardenShopServer.RegistrUserRequest
                {
                    UserId = user.Id,
                    Address = user.Address,
                    Mail = user.Mail,
                    Name = user.Name,
                    Phone = $"{user.CodePhone} {user.Phone.Value}"
                }))
                {
                    var responce = call.ResponseAsync.Result;
                    result = responce.IsRegistr;
                }

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(result);
        }

        public Task<bool> IsRegistrUser(User user)
        {
            bool result = false;
            try
            {
                CancellationToken token = new CancellationToken();

                using (var call = _greeterClient.IsRegistrUserAsync(new HomeGardenShopServer.IsRegistrUserRequest
                {
                    UserId = user.Id
                }))
                {
                    var responce = call.ResponseAsync.Result;
                    result = responce.IsRegistr;
                }

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(result);
        }

        public Task<User> GetUser(User user)
        {
            bool result = false;
            try
            {
                CancellationToken token = new CancellationToken();

                using (var call = _greeterClient.GetUserAsync(new HomeGardenShopServer.GetUserRequest
                {
                    UserId = user.Id
                }))
                {
                    var responce = call.ResponseAsync.Result;
                    user.Name = responce.Name;
                    user.Mail = responce.Mail;
                    user.Address = responce.Address;

                    user.CodePhone = responce.Phone.Split()[0];
                    user.Phone.Value = responce.Phone.Replace(user.CodePhone, "");
                }

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(user);
        }

        public Task<string> GetAboutUsText(string language)
        {
            string text = "";
            try
            {
                CancellationToken token = new CancellationToken();

                using (var call = _greeterClient.GetAboutUsAsync(new HomeGardenShopServer.GetAboutUsRequest
                {
                    Language = language
                }))
                {
                    var responce = call.ResponseAsync.Result;
                    text = responce.Text;
                }

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(text);
        }
    }
}

