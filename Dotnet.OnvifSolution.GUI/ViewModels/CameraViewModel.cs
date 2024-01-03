using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.GUI.ViewModels.Panels;
using Dotnet.OnvifSolution.Models;
using Dotnet.OnvifSolution.OnvifMedia;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Dotnet.OnvifSolution.GUI.ViewModels
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/15/2023 1:59:27 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class CameraViewModel : Screen
    {

        #region - Ctors -
        public CameraViewModel(ICameraDeviceModel model)
        {
            _model = model;
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -

        public void GetProfiles()
        {
            InnerViewModel = new ProfileViewModel(CameraMedia);
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


        public string Manufacturer
        {
            get { return _model.Manufacturer; }
            set
            {
                _model.Manufacturer = value;
                NotifyOfPropertyChange(nameof(Manufacturer));
            }
        }

        public string DeviceModel
        {
            get { return _model.DeviceModel; }
            set
            {
                _model.DeviceModel = value;
                NotifyOfPropertyChange(nameof(DeviceModel));
            }
        }

        public string FirmwareVersion
        {
            get { return _model.FirmwareVersion; }
            set
            {
                _model.FirmwareVersion = value;
                NotifyOfPropertyChange(nameof(FirmwareVersion));
            }
        }

        public string SerialNumber
        {
            get { return _model.SerialNumber; }
            set
            {
                _model.SerialNumber = value;
                NotifyOfPropertyChange(nameof(SerialNumber));
            }
        }

        public string HardwareId
        {
            get { return _model.HardwareId; }
            set
            {
                _model.HardwareId = value;
                NotifyOfPropertyChange(nameof(HardwareId));
            }
        }


        public CameraMediaModel CameraMedia
        {
            get { return _model.CameraMedia; }
            set
            {
                _model.CameraMedia = value;
                NotifyOfPropertyChange(nameof(Password));
            }
        }

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            set 
            {
                _profiles = value;
                NotifyOfPropertyChange(() => Profiles);
            }
        }

        public ObservableCollection<PTZConfiguration> PTZConfiguration
        {
            get => _ptzConfiguration;
            set
            {
                _ptzConfiguration = value;
                NotifyOfPropertyChange(() => PTZConfiguration);
            }
        }

        public ContentControlViewModel<CameraMediaModel> InnerViewModel
        {
            get { return _innerViewModel; }
            set 
            {
                _innerViewModel = value; 
                NotifyOfPropertyChange(() => InnerViewModel);
            }
        }

        public ICameraDeviceModel Model => _model;
        #endregion
        #region - Attributes -
        private string _deviceName;
        private ObservableCollection<Profile> _profiles;
        private ObservableCollection<PTZConfiguration> _ptzConfiguration;
        private ICameraDeviceModel _model;
        private ContentControlViewModel<CameraMediaModel> _innerViewModel;
        #endregion
    }
}
