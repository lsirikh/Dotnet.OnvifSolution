using Autofac;
using Dotnet.OnvifSolution.GUI.ViewModels;
using Dotnet.OnvifSolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.OnvifSolution.Gui.Base;
using Wpf.OnvifSolution.Gui.ViewModels;

namespace Wpf.OnvifSolution.Gui
{
    public class Bootstrapper : ParentBootstrapper<ShellViewModel>
    {
        #region - Ctors -
        public Bootstrapper()
        {
            Initialize();
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            await DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            try
            {
                base.ConfigureContainer(builder);

                builder.RegisterType<DashBoardViewModel>().SingleInstance();
                builder.RegisterType<DeviceService>().SingleInstance();
                //builder.RegisterType<DemoViewModel>().SingleInstance();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() }
            .Concat(base.SelectAssemblies())
            .Concat(new[]
            {
                Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), "Dotnet.OnvifSolution.GUI.dll")),
            });
        }
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        #endregion
        #region - Attributes -
        #endregion
    }
}
