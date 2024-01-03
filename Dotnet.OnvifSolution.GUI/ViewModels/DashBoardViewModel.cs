using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.GUI.ViewModels.Dialogs;
using Dotnet.OnvifSolution.Models;
using Dotnet.OnvifSolution.Services;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.GUI.ViewModels
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/15/2023 1:54:43 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/
    
    public class DashBoardViewModel : Screen
    {

        #region - Ctors -
        public DashBoardViewModel(IEventAggregator eventAggregator
                                , IWindowManager windowManager
                                , DeviceService deviceService)
        {
            _eventAggregator = eventAggregator;
            _deviceService = deviceService;
            _windowManager = windowManager;
            CameraProvider = new ObservableCollection<CameraViewModel>();
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        public async void AddButton()
        {
            _addDialog = new AddConnectionDialogViewModel(new ConnectionModel());
            bool ret = (bool)await _windowManager.ShowDialogAsync(_addDialog);

            if (!ret) return;

            var cameraDeivceModel = await _deviceService.InitializeOnvif(_addDialog.Model);
            if (cameraDeivceModel == null) return;
            
            CameraProvider.Add(new CameraViewModel(cameraDeivceModel));
        }

        public void RemoveButton()
        {
            if(SelectedItem== null) return;

            CameraProvider.Remove(SelectedItem);
            SelectedItem = null;
        }
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -

        public ObservableCollection<CameraViewModel> CameraProvider
        {
            get { return _cameraProvider; }
            set 
            {
                _cameraProvider = value;
            }
        }

        public CameraViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            { 
                _selectedItem = value; 
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        #endregion
        #region - Attributes -
        private IEventAggregator _eventAggregator;
        private DeviceService _deviceService;
        private IWindowManager _windowManager;
        private CameraViewModel _newInstance;
        private AddConnectionDialogViewModel _addDialog;
        private CameraViewModel _selectedItem;
        private ObservableCollection<CameraViewModel> _cameraProvider   ;
        #endregion
    }
}
