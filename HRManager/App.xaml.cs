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
        
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AppStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var appMainWindow = new MainWindow(ServiceProvider);
            appMainWindow.Show();
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IHumanCenter, HumanCenter>(sp =>
            {
                return new HumanCenter();
            });
            services.AddSingleton<IPersonFactory, PersonFactory>(sp =>
            {
                return new PersonFactory();
            });
            services.AddSingleton<IOrganizationFactory, OrganizationFactory>(sp =>
            {
                return new OrganizationFactory();
            });
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
