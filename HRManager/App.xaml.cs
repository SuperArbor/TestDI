using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HumanResource;
using HRManager.Views;

namespace HRManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ServiceProvider { get; set; }
        public MainWindow AppMainWindow { get; set; }
        
        public void AppStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            AppMainWindow = new MainWindow(ServiceProvider);
            AppMainWindow.Show();
        }
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IHumanCenter, HumanCenter>(sp =>
            {
                var zju = new School(Guid.NewGuid().ToString(), "ZJU");
                var pku = new School(Guid.NewGuid().ToString(), "PKU");
                var huawei = new Company(Guid.NewGuid().ToString(), "Huawei");
                var ali = new Company(Guid.NewGuid().ToString(), "Ali");

                var hc = new HumanCenter();
                hc.AddOrganization(zju);
                hc.AddOrganization(pku);
                hc.AddOrganization(huawei);
                hc.AddOrganization(ali);

                return hc;
            });
            services.AddSingleton<IPersonFactory, PersonFactory>(sp =>
            {
                return new PersonFactory();
            });
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
