using Microsoft.Extensions.DependencyInjection;
using System;
using AwesomeApp.Services;
using AwesomeApp.ViewModels;
using System.Net.Http;

namespace XamWebApiClient
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            //add services
            services.AddHttpClient<IUserService, UserService>(c =>
            {
                c.BaseAddress = new Uri("http://9041-143-0-57-126.ngrok.io/api/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = 
                    (sender, cert, chain, sslPolicyErrors) => { return true; }
            }); 

            //add viewmodels
            services.AddTransient<UserRegisterViewModel>();
            services.AddTransient<UserSaveViewModel>();
            services.AddTransient<UserListViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => ServiceProvider.GetService<T>();
    }
}