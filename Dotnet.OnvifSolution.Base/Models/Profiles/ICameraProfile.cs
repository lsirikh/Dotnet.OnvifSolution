using Dotnet.OnvifSolution.Base.Models.Profiles.AudioEncoderConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.AudioSourceConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.MetadataConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.PtzConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoAnalyticConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoEncoderConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models.Profiles
{
    public interface ICameraProfile
    {
        string Name { get; set; }
        string Token { get; set; }
        bool Fixed { get; set; }
        VideoSourceConfigModel VideoSourceConfig { get; set; }
        VideoEncoderConfigModel VideoEncoderConfig { get; set; }
        AudioSourceConfigModel AudioSourceConfig { get; set; }
        AudioEncoderConfigModel AudioEncoderConfig { get; set; }
        PTZConfigModel PTZConfig { get; set; }
        VideoAnalyticsConfigModel VideoAnalyticsConfig { get; set; }
        MetadataConfigModel MetadataConfig { get; set; }



    }
}
