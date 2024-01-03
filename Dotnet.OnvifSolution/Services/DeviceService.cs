using Dotnet.OnvifSolution.Base.Enums;
using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.Base.Models.Profiles;
using Dotnet.OnvifSolution.Factories;
using Dotnet.OnvifSolution.Models;
using Dotnet.OnvifSolution.OnvifDeviceIo;
using Dotnet.OnvifSolution.OnvifDeviceMgmt;
using Dotnet.OnvifSolution.OnvifImaging;
using Dotnet.OnvifSolution.OnvifMedia;
using Dotnet.OnvifSolution.OnvifPtz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Services
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/14/2023 10:58:17 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class DeviceService
    {

        #region - Ctors -
        public DeviceService()
        {
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        

        public async Task<ICameraDeviceModel> InitializeOnvif(IConnectionModel connection)
        {

            var host = connection.IpAddress;
            var username = connection.UserName;
            var password = connection.Password;

            var model =  new CameraDeviceModel();

            model.DeviceName = connection.DeviceName;
            model.IpAddress = connection.IpAddress;
            model.UserName = connection.UserName;
            model.Password = connection.Password;
            
            try
            {
                model.DeviceClient = await ServiceFactory.CreateDeviceClientAsync(host, username, password);
                
                if (model.DeviceClient == null) throw new Exception($"deviceClient was not created. Please check [Uri : {host}, Id : {username}, Pass: {password}]!");

                var info = model.DeviceClient.GetDeviceInformationAsync(new OnvifDeviceIo.GetDeviceInformationRequest());
                if (info == null) throw new Exception($"Camera Device was not fetched!");
                model.Manufacturer = info.Result.Manufacturer;
                model.DeviceModel = info.Result.Model;
                model.FirmwareVersion = info.Result.FirmwareVersion;
                model.SerialNumber = info.Result.SerialNumber;
                model.HardwareId = info.Result.HardwareId;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(InitializeOnvif)} of {nameof(DeviceService)} : {ex.Message}");
                return null;
            }

            try
            {
                model.MediaClient = await ServiceFactory.CreateMediaClientAsync(host, username, password);
                if (model.MediaClient == null) throw new Exception($"Media was not created. Please check [Uri : {host}, Id : {username}, Pass: {password}]!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(InitializeOnvif)} of {nameof(DeviceService)} : {ex.Message}");
                return null;
            }

            try
            {
                model.PtzClient = await ServiceFactory.CreatePTZClientAsync(host, username, password);
                if (model.PtzClient == null) throw new Exception($"Ptz was not created. Please check [Uri : {host}, Id : {username}, Pass: {password}]!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(InitializeOnvif)} of {nameof(DeviceService)} : {ex.Message}");
            }

            try
            {
                model.ImagingClient = await ServiceFactory.CreateImagingClientAsync(host, username, password);
                if (model.ImagingClient == null) throw new Exception($"Imaging was not created. Please check [Uri : {host}, Id : {username}, Pass: {password}]!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(InitializeOnvif)} of {nameof(DeviceService)} : {ex.Message}");
                return null;
            }
           

            model.Profiles = (await model.MediaClient.GetProfilesAsync()).Profiles.ToList();

            
            foreach (var profile in model.Profiles)
            {
                //Debug.WriteLine($"Profile: {profile.token}");
                var cameraProfile = new CameraProfile();
                //model.CameraMedia.Profiles.Add(cameraProfile);
                try
                {
                    cameraProfile.Name = profile.Name;
                    cameraProfile.Token = profile.token;
                    cameraProfile.Fixed = profile.@fixed;

                    //Profile Streaming URL
                    cameraProfile = await GetProfileStreamUrl(cameraProfile, profile.token, model);


                    //VideoSourceConfiguration
                    if(profile?.VideoSourceConfiguration != null)
                    {
                        cameraProfile.VideoSourceConfig = new Base.Models.Profiles.VideoSourceConfigs.VideoSourceConfigModel();

                        cameraProfile.VideoSourceConfig.Name = profile?.VideoSourceConfiguration?.Name;
                        cameraProfile.VideoSourceConfig.Token = profile?.VideoSourceConfiguration?.SourceToken;
                        cameraProfile.VideoSourceConfig.UseCount = (int)profile?.VideoSourceConfiguration?.UseCount;
                        cameraProfile.VideoSourceConfig.Bounds = new List<int>
                        {
                            (int)profile?.VideoSourceConfiguration?.Bounds.x,
                            (int)profile?.VideoSourceConfiguration?.Bounds.y,
                            (int)profile?.VideoSourceConfiguration?.Bounds.width,
                            (int)profile?.VideoSourceConfiguration?.Bounds.height,
                        };
                        cameraProfile.VideoSourceConfig.Token = profile?.VideoSourceConfiguration?.SourceToken;

                        cameraProfile = await GetVideoSource(cameraProfile, model);
                    }
                    
                    //AudioSourceConfiguration
                    if(profile.AudioSourceConfiguration != null)
                    {
                        cameraProfile.AudioSourceConfig = new Base.Models.Profiles.AudioSourceConfigs.AudioSourceConfigModel();

                        cameraProfile.AudioSourceConfig.Name = profile.AudioSourceConfiguration.Name;
                        cameraProfile.AudioSourceConfig.Token = profile.AudioSourceConfiguration.token;
                        cameraProfile.AudioSourceConfig.UseCount = profile.AudioSourceConfiguration.UseCount;

                        cameraProfile = await GetAudioSource(cameraProfile, model);
                    }
                    

                    //VideoEncoderConfiguration
                    cameraProfile = GetVideoEncoderConfig(cameraProfile, profile.VideoEncoderConfiguration);
                    //AudioEncoderConfiguration
                    cameraProfile = GetAudioEncoderConfig(cameraProfile, profile.AudioEncoderConfiguration);
                    //VideoAnalyticsConfiguration
                    cameraProfile = GetVideoAnalyticsConfig(cameraProfile, profile.VideoAnalyticsConfiguration);
                    //MetadataConfiguration
                    cameraProfile = GetMetadataConfig(cameraProfile, profile.MetadataConfiguration);
                    //PTZConfiguration
                    cameraProfile = await GetPTZConfig(cameraProfile, model);
                    model.CameraMedia.Profiles.Add(cameraProfile);
                }
                catch 
                {
                }
            }
            return model;
        }

        private async Task<CameraProfile> GetProfileStreamUrl(CameraProfile cameraProfile, string token, CameraDeviceModel model)
        {
            try
            {
                var streamSetup = new StreamSetup
                {
                    Stream = StreamType.RTPMulticast,  // 또는 RTPMulticast
                    Transport = new Transport
                    {
                        Protocol = TransportProtocol.RTSP
                    }
                };

                MediaUri stream = await model.MediaClient.GetStreamUriAsync(streamSetup, token);
                if (stream != null)
                {

                    cameraProfile.MediaUri = new Base.Models.Components.MediaUriModel();
                    cameraProfile.MediaUri.Uri = stream.Uri;
                    cameraProfile.MediaUri.InvalidAfterConnect = stream.InvalidAfterConnect;
                    cameraProfile.MediaUri.InvalidAfterReboot = stream.InvalidAfterReboot;
                    cameraProfile.MediaUri.Timeout = stream.Timeout;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetProfileStreamUrl)} of {nameof(DeviceService)} : {ex.Message}");
            }

            return cameraProfile;
        }

        private CameraProfile GetVideoAnalyticsConfig(CameraProfile cameraProfile, OnvifMedia.VideoAnalyticsConfiguration videoAnalyticsConfiguration)
        {
            try
            {
                cameraProfile.VideoAnalyticsConfig = new Base.Models.Profiles.VideoAnalyticConfigs.VideoAnalyticsConfigModel();
                cameraProfile.VideoAnalyticsConfig.Name = videoAnalyticsConfiguration.Name;
                cameraProfile.VideoAnalyticsConfig.Token = videoAnalyticsConfiguration.token;
                cameraProfile.VideoAnalyticsConfig.UseCount = videoAnalyticsConfiguration.UseCount;
                foreach (var item in videoAnalyticsConfiguration.AnalyticsEngineConfiguration.AnalyticsModule)
                {
                    cameraProfile.VideoAnalyticsConfig.Analytics.Add(item.Name);
                }

                foreach (var item in videoAnalyticsConfiguration.RuleEngineConfiguration.Rule)
                {
                    cameraProfile.VideoAnalyticsConfig.Analytics.Add(item.Name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetVideoAnalyticsConfig)} of {nameof(DeviceService)} : {ex.Message}");
            }

            return cameraProfile;
        }

        private CameraProfile GetAudioEncoderConfig(CameraProfile cameraProfile, OnvifMedia.AudioEncoderConfiguration audioEncoderConfiguration)
        {
            try
            {
                if (audioEncoderConfiguration == null) return cameraProfile;

                cameraProfile.AudioEncoderConfig = new Base.Models.Profiles.AudioEncoderConfigs.AudioEncoderConfigModel();
                cameraProfile.AudioEncoderConfig.Name = audioEncoderConfiguration.Name;
                cameraProfile.AudioEncoderConfig.Token = audioEncoderConfiguration.token;
                cameraProfile.AudioEncoderConfig.UseCount = audioEncoderConfiguration.UseCount;
                cameraProfile.AudioEncoderConfig.Encoding =
                    (EnumAudioEncoding)audioEncoderConfiguration.Encoding;
                cameraProfile.AudioEncoderConfig.Bitrate = audioEncoderConfiguration.Bitrate;
                cameraProfile.AudioEncoderConfig.SampleRate = audioEncoderConfiguration.SampleRate;
                cameraProfile.AudioEncoderConfig.SessionTimeout = audioEncoderConfiguration.SessionTimeout;

                if (audioEncoderConfiguration.Multicast == null) return cameraProfile;
                cameraProfile.AudioEncoderConfig.MultiCast = new Base.Models.Components.MultiCastModel();
                cameraProfile.AudioEncoderConfig.MultiCast.IpAddress = audioEncoderConfiguration.Multicast.Address.IPv4Address;
                cameraProfile.AudioEncoderConfig.MultiCast.Port = audioEncoderConfiguration.Multicast.Port;
                cameraProfile.AudioEncoderConfig.MultiCast.Ttl = audioEncoderConfiguration.Multicast.TTL;
                cameraProfile.AudioEncoderConfig.MultiCast.AutoStart = audioEncoderConfiguration.Multicast.AutoStart;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetAudioEncoderConfig)} of {nameof(DeviceService)} : {ex.Message}");
            }
            return cameraProfile;
        }

        private CameraProfile GetVideoEncoderConfig(CameraProfile cameraProfile, OnvifMedia.VideoEncoderConfiguration videoEncoderConfiguration)
        {
            try
            {
                cameraProfile.VideoEncoderConfig = new Base.Models.Profiles.VideoEncoderConfigs.VideoEncoderConfigModel();
                cameraProfile.VideoEncoderConfig.Name = videoEncoderConfiguration.Name;
                cameraProfile.VideoEncoderConfig.Token = videoEncoderConfiguration.token;
                cameraProfile.VideoEncoderConfig.UseCount = videoEncoderConfiguration.UseCount;
                cameraProfile.VideoEncoderConfig.Encoding =
                    (EnumVideoEncoding)videoEncoderConfiguration.Encoding;
                cameraProfile.VideoEncoderConfig.Resolution.Width = videoEncoderConfiguration.Resolution.Width;
                cameraProfile.VideoEncoderConfig.Resolution.Height = videoEncoderConfiguration.Resolution.Height;
                cameraProfile.VideoEncoderConfig.SessionTimeout = videoEncoderConfiguration.SessionTimeout;
                cameraProfile.VideoEncoderConfig.Quality = videoEncoderConfiguration.Quality;
                cameraProfile.VideoEncoderConfig.FrameRate = videoEncoderConfiguration.RateControl.FrameRateLimit;
                cameraProfile.VideoEncoderConfig.Bitrate = videoEncoderConfiguration.RateControl.BitrateLimit;
                cameraProfile.VideoEncoderConfig.EncodingInterval = videoEncoderConfiguration.RateControl.EncodingInterval;
                cameraProfile.VideoEncoderConfig.MultiCast.IpAddress = videoEncoderConfiguration.Multicast.Address.IPv4Address;
                cameraProfile.VideoEncoderConfig.MultiCast.Port = videoEncoderConfiguration.Multicast.Port;
                cameraProfile.VideoEncoderConfig.MultiCast.Ttl = videoEncoderConfiguration.Multicast.TTL;
                cameraProfile.VideoEncoderConfig.MultiCast.AutoStart = videoEncoderConfiguration.Multicast.AutoStart;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetVideoEncoderConfig)} of {nameof(DeviceService)} : {ex.Message}");
            }
            return cameraProfile;
        }

        private async Task<CameraProfile> GetAudioSource(CameraProfile cameraProfile, CameraDeviceModel model)
        {
            try
            {
                var audioSources = await model.MediaClient.GetAudioSourcesAsync();

                if (audioSources != null)
                {
                    cameraProfile.AudioSourceConfig.AudioSource = new Base.Models.Profiles.AudioSourceConfigs.AudioSource.AudioSourceModel();
                    foreach (var item in audioSources.AudioSources)
                    {
                        cameraProfile.AudioSourceConfig.AudioSource.Token = item.token;
                        cameraProfile.AudioSourceConfig.AudioSource.Channels = item.Channels;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetAudioSource)} of {nameof(DeviceService)} : {ex.Message}");
            }

            return cameraProfile;
        }

        private async Task<CameraProfile> GetVideoSource(CameraProfile cameraProfile, CameraDeviceModel model)
        {
            try
            {
                if (model.MediaClient == null) return cameraProfile;

                var mediaClient = model.MediaClient;
                var video_sources = await mediaClient.GetVideoSourcesAsync();
                //string vsource_token = null;

                foreach (var source in video_sources.VideoSources)
                {

                    if (model.CameraMedia.ProfileTitle == null)
                        model.CameraMedia.ProfileTitle = $"{source.token}:{cameraProfile.Name}";

                    //OnvifImaging.ImagingSettings20 imaging = null;
                    //if (source.Imaging == null)
                    //    imaging = await model.ImagingClient?.GetImagingSettingsAsync(source.token);


                    cameraProfile.VideoSourceConfig.VideoSource.Token = source.token;
                    cameraProfile.VideoSourceConfig.VideoSource.FrameRate = source.Framerate;
                    cameraProfile.VideoSourceConfig.VideoSource.Resolution.Width = source.Resolution.Width;
                    cameraProfile.VideoSourceConfig.VideoSource.Resolution.Height = source.Resolution.Height;

                    if (source.Imaging == null) return cameraProfile;

                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption = new Base.Models.Profiles.VideoSourceConfigs.VideoSource.Imaging.ImagingOptionModel();
                    /////////////////////Focus/////////////////////////
                    if (source.Imaging?.Focus != null)
                    {
                        // Focus
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Focus.AutoFocusMode =
                            (EnumAutoFocusMode)source.Imaging.Focus.AutoFocusMode;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Focus.NearLimit =
                            source.Imaging.Focus.NearLimit;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Focus.FarLimit =
                            source.Imaging.Focus.FarLimit;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Focus.DefaultSpeed =
                            source.Imaging.Focus.DefaultSpeed;
                    }


                    /////////////////////Exposure/////////////////////////
                    if (source.Imaging?.Exposure != null)
                    {

                        //Exposure
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.Mode =
                            (EnumExposureMode)source.Imaging.Exposure.Mode;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.IrisRange.Min =
                            source.Imaging.Exposure.MinIris;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.IrisRange.Max =
                            source.Imaging.Exposure.MaxIris;

                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.ExposureTime = source.Imaging.Exposure.ExposureTime;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.ExposureTimeRange.Min = source.Imaging.Exposure.MinExposureTime;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.ExposureTimeRange.Max = source.Imaging.Exposure.MaxExposureTime;

                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.Gain = source.Imaging.Exposure.Gain;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.GainRange.Min = source.Imaging.Exposure.MinGain;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Exposure.GainRange.Max = source.Imaging.Exposure.MaxGain;

                    }


                    //Imaging Other Options
                    if (model.ImagingClient == null) return cameraProfile;

                    var imaging_opts = await model.ImagingClient?.GetOptionsAsync(source.token);
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Brightness =
                        source.Imaging.Brightness;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.ColorSaturation =
                        source.Imaging.ColorSaturation;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Sharpness =
                        source.Imaging.Sharpness;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.Contrast =
                        source.Imaging.Contrast;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.IrCutFilter =
                        (EnumIrCutFilterMode)source.Imaging.IrCutFilter;
                    
                    
                    

                    if (imaging_opts != null)
                    {
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BrightnessRange.Min =
                            imaging_opts.Brightness.Min;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BrightnessRange.Max =
                            imaging_opts.Brightness.Max;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.ColorSaturationRange.Max =
                            imaging_opts.ColorSaturation.Max;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.ColorSaturationRange.Min =
                            imaging_opts.ColorSaturation.Min;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.SharpnessRange.Min =
                            imaging_opts.Sharpness.Min;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.SharpnessRange.Max =
                            imaging_opts.Sharpness.Max;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.ContrastRange.Min =
                            imaging_opts.Contrast.Min;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.ContrastRange.Max =
                            imaging_opts.Sharpness.Max;
                    }


                    //BacklightCompensation
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BacklightCompensation.Level =
                            source.Imaging.BacklightCompensation.Level;

                    if (imaging_opts.BacklightCompensation.Level != null)
                    {
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BacklightCompensation.LevelRange.Max =
                            imaging_opts.BacklightCompensation.Level.Max;
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BacklightCompensation.LevelRange.Min =
                            imaging_opts.BacklightCompensation.Level.Min;
                    }

                    foreach (var item in imaging_opts.BacklightCompensation.Mode.ToList())
                    {
                        cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.BacklightCompensation.Mode =
                            (EnumBacklightCompensationMode)item;
                    }

                    //WhiteBalance
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.WhiteBalance.YbGain =
                        source.Imaging.WhiteBalance.CbGain;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.WhiteBalance.YrGain =
                        source.Imaging.WhiteBalance.CrGain;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.WhiteBalance.Mode =
                        (EnumWhiteBalanceMode)source.Imaging.WhiteBalance.Mode;

                    //Wide Dynamic Range
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.WideDynamicRange.Level =
                        source.Imaging.WideDynamicRange.Level;
                    cameraProfile.VideoSourceConfig.VideoSource.ImagingOption.WideDynamicRange.Mode = (EnumWideDynamicMode)source.Imaging.WideDynamicRange.Mode;
                }
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetVideoSource)} of {nameof(DeviceService)} : {ex.Message}");
            }
            return cameraProfile;
        }

        private CameraProfile GetMetadataConfig(CameraProfile cameraProfile, OnvifMedia.MetadataConfiguration metadataConfiguration)
        {
            try
            {
                if (metadataConfiguration == null) return cameraProfile;

                cameraProfile.MetadataConfig = new Base.Models.Profiles.MetadataConfigs.MetadataConfigModel();
                cameraProfile.MetadataConfig.Name = metadataConfiguration.Name;
                cameraProfile.MetadataConfig.Token = metadataConfiguration.token;
                cameraProfile.MetadataConfig.UseCount = metadataConfiguration.UseCount;
                cameraProfile.MetadataConfig.SessionTimeout = metadataConfiguration.SessionTimeout;
                cameraProfile.MetadataConfig.Analytics = metadataConfiguration.Analytics;
                cameraProfile.MetadataConfig.CompressionType = metadataConfiguration.CompressionType;
                cameraProfile.MetadataConfig.GeoLocation = metadataConfiguration.GeoLocation;
                cameraProfile.MetadataConfig.ShapePolygon = metadataConfiguration.ShapePolygon;

                if(metadataConfiguration.PTZStatus != null)
                {
                    cameraProfile.MetadataConfig.PTZStatus = new Base.Models.Components.PTZFilterModel();
                    cameraProfile.MetadataConfig.PTZStatus.Status = metadataConfiguration.PTZStatus.Status;
                    cameraProfile.MetadataConfig.PTZStatus.Position = metadataConfiguration.PTZStatus.Position;
                }

                if(metadataConfiguration.Events != null)
                {
                    cameraProfile.MetadataConfig.EventSubscription = new Base.Models.Components.EventSubscriptionModel();
                    if(metadataConfiguration.Events.Filter != null) 
                    {
                        cameraProfile.MetadataConfig.EventSubscription.Filter = new Base.Models.Components.FilterTypeModel();
                        foreach (var item in metadataConfiguration.Events.Filter.Any)
                        {
                            cameraProfile.MetadataConfig.EventSubscription.Filter.Any.Add(item);
                        }
                    }

                    if (metadataConfiguration.Events.SubscriptionPolicy != null)
                    {
                        cameraProfile.MetadataConfig.EventSubscription.SubscriptionPolicy = new Base.Models.Components.FilterTypeModel();
                        foreach (var item in metadataConfiguration.Events.SubscriptionPolicy?.Any)
                        {
                            cameraProfile.MetadataConfig.EventSubscription.SubscriptionPolicy.Any.Add(item);
                        }
                    }
                }

                if(metadataConfiguration.Multicast != null)
                {
                    cameraProfile.MetadataConfig.MultiCast = new Base.Models.Components.MultiCastModel();
                    cameraProfile.MetadataConfig.MultiCast.IpAddress = metadataConfiguration.Multicast.Address.IPv4Address;

                    cameraProfile.MetadataConfig.MultiCast.Port = metadataConfiguration.Multicast.Port;
                    cameraProfile.MetadataConfig.MultiCast.Ttl = metadataConfiguration.Multicast.TTL;
                    cameraProfile.MetadataConfig.MultiCast.AutoStart = metadataConfiguration.Multicast.AutoStart;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetMetadataConfig)} of {nameof(DeviceService)} : {ex.Message}");
            }
            return cameraProfile;
        }

        private async Task<CameraProfile> GetPTZConfig(CameraProfile cameraProfile, CameraDeviceModel model)
        {
            try
            {
                //PTZConfiguration
                if (model.PtzClient == null) return cameraProfile;

                var ptzClient = model.PtzClient;

                var configs = await ptzClient?.GetConfigurationsAsync();

                if (configs != null)
                {
                    cameraProfile.PTZConfig = new Base.Models.Profiles.PtzConfigs.PTZConfigModel();
                    foreach (var item in configs.PTZConfiguration)
                    {
                        cameraProfile.PTZConfig.Name = item.Name;
                        cameraProfile.PTZConfig.Token = item.token;
                        cameraProfile.PTZConfig.UseCount = item.UseCount;
                        cameraProfile.PTZConfig.PanTiltRange.XRange.Min = item.PanTiltLimits.Range.XRange.Min;
                        cameraProfile.PTZConfig.PanTiltRange.XRange.Max = item.PanTiltLimits.Range.XRange.Max;
                        cameraProfile.PTZConfig.PanTiltRange.YRange.Min = item.PanTiltLimits.Range.YRange.Min;
                        cameraProfile.PTZConfig.PanTiltRange.YRange.Max = item.PanTiltLimits.Range.YRange.Max;
                        cameraProfile.PTZConfig.PanTiltRange.Uri = item.PanTiltLimits.Range.URI;
                        cameraProfile.PTZConfig.ZoomRange.XRange.Min = item.ZoomLimits.Range.XRange.Min;
                        cameraProfile.PTZConfig.ZoomRange.XRange.Max = item.ZoomLimits.Range.XRange.Max;
                        cameraProfile.PTZConfig.ZoomRange.Uri = item.ZoomLimits.Range.URI;

                        cameraProfile.PTZConfig.DefaultPTZSpeed.PanTilt.X = item.DefaultPTZSpeed.PanTilt.x;
                        cameraProfile.PTZConfig.DefaultPTZSpeed.PanTilt.Y = item.DefaultPTZSpeed.PanTilt.y;
                        cameraProfile.PTZConfig.DefaultPTZSpeed.PanTilt.Space = item.DefaultPTZSpeed.PanTilt.space;

                        cameraProfile.PTZConfig.DefaultPTZSpeed.Zoom.X = item.DefaultPTZSpeed.Zoom.x;
                        cameraProfile.PTZConfig.DefaultPTZSpeed.Zoom.Space = item.DefaultPTZSpeed.Zoom.space;
                        cameraProfile.PTZConfig.Timeout = item.DefaultPTZTimeout;

                        cameraProfile.PTZConfig.DefaultAbsolutePantTiltPositionSpace = 
                            item.DefaultAbsolutePantTiltPositionSpace;
                        cameraProfile.PTZConfig.DefaultAbsoluteZoomPositionSpace =                        
                            item.DefaultAbsoluteZoomPositionSpace;

                        cameraProfile.PTZConfig.DefaultContinuousPanTiltVelocitySpace = 
                            item.DefaultContinuousPanTiltVelocitySpace;
                        cameraProfile.PTZConfig.DefaultContinuousZoomVelocitySpace = 
                            item.DefaultContinuousZoomVelocitySpace;

                        cameraProfile.PTZConfig.DefaultRelativePanTiltTranslationSpace = 
                            item.DefaultRelativePanTiltTranslationSpace;
                        cameraProfile.PTZConfig.DefaultRelativeZoomTranslationSpace = 
                            item.DefaultRelativeZoomTranslationSpace;

                        var node = await ptzClient.GetNodeAsync(item.NodeToken);

                        cameraProfile.PTZConfig.PTZNode.Name = node.Name;
                        cameraProfile.PTZConfig.PTZNode.Token = node.token;
                        cameraProfile.PTZConfig.PTZNode.HomeSupported = node.HomeSupported;
                        cameraProfile.PTZConfig.PTZNode.MaximumNumberOfPresets = node.MaximumNumberOfPresets;

                        if (node.AuxiliaryCommands == null) continue;

                        foreach (var innerItem in node.AuxiliaryCommands)
                        {
                            cameraProfile.PTZConfig.PTZNode.AuxiliaryCommands.Add(innerItem);
                        }
                        //cameraProfile.PTZConfig.PTZNode.SupportedPTZSpaces = node.SupportedPTZSpaces;
                    }

                    //profile_token = configs.PTZConfiguration.;
                    //absolute_move = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultAbsolutePantTiltPositionSpace);
                    //absolute_zoom = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultAbsoluteZoomPositionSpace);
                    //relative_move = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultRelativePanTiltTranslationSpace);
                    //relative_zoom = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultRelativeZoomTranslationSpace);
                    //continuous_move = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultContinuousPanTiltVelocitySpace);
                    //continuous_zoom = !string.IsNullOrWhiteSpace(profile?.PTZConfiguration?.DefaultContinuousPanTiltVelocitySpace);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Raised Exception in {nameof(GetPTZConfig)} of {nameof(DeviceService)} : {ex.Message}");

            }
            return cameraProfile;
        }
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        #endregion
        #region - Attributes -
        //private OnvifDeviceIo.deviceClient deviceClient;
        //private mediaClient media;
        //private PTZClient ptz;
        //private ImagingPort imaging;
        //private GetProfilesResponse profiles;
        //private OnvifDeviceIo.GetCapabilitiesResponse capabilities;
        //private string host;
        //private string username;
        //private string password;
        #endregion
    }
}
