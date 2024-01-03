using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Text;
using System;
using System.Threading.Tasks;
using Dotnet.OnvifSolution.OnvifDeviceIo;
using Dotnet.OnvifSolution.Security;
using Dotnet.OnvifSolution.OnvifMedia;
using Dotnet.OnvifSolution.OnvifPtz;
using Dotnet.OnvifSolution.OnvifImaging;

namespace Dotnet.OnvifSolution.Factories
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/14/2023 11:01:50 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public static class ServiceFactory
    {

        #region - Ctors -
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        static Binding CreateBinding()
        {
            var binding = new CustomBinding();
            var textBindingElement = new TextMessageEncodingBindingElement
            {
                MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None),
                WriteEncoding = Encoding.UTF8
            };
            var httpBindingElement = new HttpTransportBindingElement
            {
                AllowCookies = true,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            binding.Elements.Add(textBindingElement);
            binding.Elements.Add(httpBindingElement);

            return binding;
        }
        #endregion
        #region - Processes -
        public static async Task<DeviceClient> CreateDeviceClientAsync(string host, string username, string password)
        {
            return await CreateDeviceClientAsync(new Uri($"http://{host}/onvif/device_service"), username, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<DeviceClient> CreateDeviceClientAsync(Uri uri, string username, string password)
        {
            Binding binding = CreateBinding();
            var endpoint = new EndpointAddress(uri);
            var device = new DeviceClient(binding, endpoint);
            var time_shift = await GetDeviceTimeShift(device);

            device = new DeviceClient(binding, endpoint);
            device.ChannelFactory.Endpoint.EndpointBehaviors.Clear();
            device.ChannelFactory.Endpoint.EndpointBehaviors.Add(new SoapSecurityHeaderBehavior(username, password, time_shift));

            // Connectivity Test

            return device;
        }

        public static async Task<MediaClient> CreateMediaClientAsync(string host, string username, string password)
        {
            var binding = CreateBinding();
            var device = await CreateDeviceClientAsync(host, username, password);
            var caps = await device.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.Media });
            var media = new MediaClient(binding, new EndpointAddress(new Uri(caps.Capabilities.Media.XAddr)));

            var time_shift = await GetDeviceTimeShift(device);
            media.ChannelFactory.Endpoint.EndpointBehaviors.Clear();
            media.ChannelFactory.Endpoint.EndpointBehaviors.Add(new SoapSecurityHeaderBehavior(username, password, time_shift));

            return media;
        }

        public static async Task<PTZClient> CreatePTZClientAsync(string host, string username, string password)
        {
            var binding = CreateBinding();
            var device = await CreateDeviceClientAsync(host, username, password);
            var caps = await device.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.PTZ });
            var ptz = new PTZClient(binding, new EndpointAddress(new Uri(caps.Capabilities.PTZ.XAddr)));

            var time_shift = await GetDeviceTimeShift(device);
            ptz.ChannelFactory.Endpoint.EndpointBehaviors.Clear();
            ptz.ChannelFactory.Endpoint.EndpointBehaviors.Add(new SoapSecurityHeaderBehavior(username, password, time_shift));

            return ptz;
        }

        public static async Task<ImagingPortClient> CreateImagingClientAsync(string host, string username, string password)
        {
            var binding = CreateBinding();
            var device = await CreateDeviceClientAsync(host, username, password);
            var caps = await device.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.Imaging });
            var imaging = new ImagingPortClient(binding, new EndpointAddress(new Uri(caps.Capabilities.Imaging.XAddr)));

            var time_shift = await GetDeviceTimeShift(device);
            imaging.ChannelFactory.Endpoint.EndpointBehaviors.Clear();
            imaging.ChannelFactory.Endpoint.EndpointBehaviors.Add(new SoapSecurityHeaderBehavior(username, password, time_shift));


            return imaging;
        }

        static async Task<TimeSpan> GetDeviceTimeShift(DeviceClient device)
        {
            try
            {
                var utc = (await device.GetSystemDateAndTimeAsync()).UTCDateTime;
                var dt = new System.DateTime(utc.Date.Year, utc.Date.Month, utc.Date.Day,
                                  utc.Time.Hour, utc.Time.Minute, utc.Time.Second);
                return dt - System.DateTime.UtcNow;
            }
            catch (Exception)
            {

                throw;
            }
            
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
