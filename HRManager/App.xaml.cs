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
using System.Collections.ObjectModel;

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
                //定义person加入organization后的处理事项
                var onEntered = new Action<IOrganization, IPerson>((org, p) =>
                {
                    p.Organization = org.Name;
                    (org as Organization).PeopleToShow = new ObservableCollection<IPerson>(org.GetMembers());
                });
                //定义person离开organization后的处理事项
                var onLeft = new Action<IOrganization, IPerson>((org, p) =>
                {
                    p.Organization = string.Empty;
                    (org as Organization).PeopleToShow = new ObservableCollection<IPerson>(org.GetMembers());
                });
                var zju = new School(Guid.NewGuid().ToString(), "ZJU", onEntered, onLeft);
                var pku = new School(Guid.NewGuid().ToString(), "PKU", onEntered, onLeft);
                var huawei = new Company(Guid.NewGuid().ToString(), "Huawei", onEntered, onLeft);
                var ali = new Company(Guid.NewGuid().ToString(), "Ali", onEntered, onLeft);

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
