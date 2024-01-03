using Caliburn.Micro;
using Dotnet.OnvifSolution.GUI.ViewModels;
using Dotnet.OnvifSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wpf.OnvifSolution.Gui.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>
    {
        #region - Ctors -
        public ShellViewModel(IEventAggregator eventAggregator
                            , DashBoardViewModel dashBoardViewModel
            )
        {
            _eventAggregator = eventAggregator;
            DashBoardViewModel = dashBoardViewModel;
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _eventAggregator.SubscribeOnUIThread(this);
            await DashBoardViewModel.ActivateAsync();
            await base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        #endregion
        #region - Attributes -
        private IEventAggregator _eventAggregator;

        public DashBoardViewModel DashBoardViewModel { get; }

        private DeviceService _deviceService;

        #endregion
    }
}
