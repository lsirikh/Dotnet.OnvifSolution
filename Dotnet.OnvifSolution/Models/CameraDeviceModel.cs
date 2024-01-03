using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.OnvifDeviceIo;
using Dotnet.OnvifSolution.OnvifImaging;
using Dotnet.OnvifSolution.OnvifMedia;
using Dotnet.OnvifSolution.OnvifPtz;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dotnet.OnvifSolution.Models
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/15/2023 5:31:34 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class CameraDeviceModel : ICameraDeviceModel
    {
        #region - Ctors -
        public CameraDeviceModel()
        {
            CameraMedia = new CameraMediaModel();
        }

        public CameraDeviceModel(string ipAddress, string userName, string password
            , DeviceClient device, MediaClient media, PTZClient ptz, ImagingPortClient imaging, List<Profile> profiles) : this()
        {
            IpAddress = ipAddress;
            UserName = userName;
            Password = password;

            DeviceClient = device;
            MediaClient = media;
            PtzClient = ptz;
            ImagingClient = imaging;
            Profiles = profiles;
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        /// <summary>
        /// Device Name Property
        /// </summary>
        [JsonProperty("device_name", Order = 1)]
        public string DeviceName { get; set; }
        /// <summary>
        /// Ip Address for Ip Camera with Port if needed.
        /// </summary>
        [JsonProperty("ip_address", Order = 2)]
        public string IpAddress { get; set; }
        /// <summary>
        /// Username for Authentication
        /// </summary>
        [JsonProperty("user_name", Order = 3)]
        public string UserName { get; set; }
        /// <summary>
        /// Password for Authentication
        /// </summary>
        [JsonProperty("password", Order = 4)]
        public string Password { get; set; }
        /// <summary>
        /// Class한 Profile 정보
        /// </summary>
        [JsonProperty("camera_media", Order = 5)]
        public CameraMediaModel CameraMedia { get; set; }


        public string Manufacturer { get ; set ; }
        public string DeviceModel { get ; set ; }
        public string FirmwareVersion { get ; set ; }
        public string SerialNumber { get ; set ; }
        public string HardwareId { get ; set ; }

        public bool IsDevicePossible { get ; set ; }
        public bool IsMediaPossible { get ; set ; }
        public bool IsEventPossible { get ; set ; }
        public bool IsImagingPossible { get ; set ; }
        public bool IsPtzPossible { get ; set ; }










        /// <summary>
        /// Onvif Class deviceClient
        /// From Dotnet.OnvifSolution.OnvifDeviceIo.Device
        /// </summary>
        [JsonIgnore]
        public DeviceClient DeviceClient { get ; set; }

        /// <summary>
        /// Onvif Class mediaClient
        /// From Dotnet.OnvifSolution.OnvifMedia.Media
        /// </summary>
        [JsonIgnore]
        public MediaClient MediaClient { get ; set; }

        /// <summary>
        /// Onvif Class ptzClient
        /// From Dotnet.OnvifSolution.OnvifPtz.PTZ
        /// </summary>
        [JsonIgnore]
        public PTZClient PtzClient { get ;  set; }

        /// <summary>
        /// Onvif Class imagingClient
        /// From Dotnet.OnvifSolution.OnvifImaging.ImagingPort
        /// </summary>
        [JsonIgnore]
        public ImagingPortClient ImagingClient { get ;  set; }

        /// <summary>
        /// Onvif Class profiles
        /// From http://www.onvif.org/ver10/schema
        /// </summary>
        [JsonIgnore]
        public List<Profile> Profiles { get ;  set; }
        
        #endregion
        #region - Attributes -
        #endregion
    }
}
