using Dotnet.OnvifSolution.Base.Enums;
using Dotnet.OnvifSolution.Base.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models.Profiles.AudioEncoderConfigs
{
    public interface IAudioEncoderConfigModel
    {
        string Name { get; set; }
        string Token { get; set; }
        int UseCount { get; set; }
        EnumAudioEncoding Encoding { get; set; }
        int Bitrate { get; set; }
        int SampleRate { get; set; }
        string SessionTimeout { get; set; }
        MultiCastModel MultiCast { get; set; }
    }
}
