using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.GUI.ViewModels.Dialogs
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/15/2023 3:14:14 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class AddConnectionDialogViewModel : Screen
    {

        #region - Ctors -
        public AddConnectionDialogViewModel(IConnectionModel connectionModel)
        {
            _model = connectionModel;
            _eventAggregator = IoC.Get<IEventAggregator>();
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _eventAggregator?.SubscribeOnUIThread(this);
            DeviceName = "cam1";
            IpAddress = "192.168.1.119";
            UserName = "admin";
            Password = "sensorway1";
            await base.OnActivateAsync(cancellationToken);
        }
        protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator?.Unsubscribe(this);
            await base.OnDeactivateAsync(close, cancellationToken);
        }

        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        public async void ClickOk()
        {
            await TryCloseAsync(true);
        }

        public async void ClickCancel()
        {
            await TryCloseAsync(false);
        }
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        public string DeviceName
        {
            get { return _model.DeviceName; }
            set
            {
                _model.DeviceName = value;
                NotifyOfPropertyChange(nameof(DeviceName));
            }
        }
        public string IpAddress
        {
            get { return _model.IpAddress; }
            set 
            {
                _model.IpAddress = value; 
                NotifyOfPropertyChange(nameof(IpAddress));
            }
        }

        public string UserName
        {
            get { return _model.UserName; }
            set
            { 
                _model.UserName = value;
                NotifyOfPropertyChange(nameof(UserName));
            }
        }

        public string Password
        {
            get { return _model.Password; }
            set 
            { 
                _model.Password = value; 
                NotifyOfPropertyChange(nameof(Password));
            }
        }
        public IConnectionModel Model => _model;
        #endregion
        #region - Attributes -
        private IConnectionModel _model;
        private IEventAggregator _eventAggregator;
        #endregion
    }
}
