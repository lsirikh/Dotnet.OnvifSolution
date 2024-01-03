using Dotnet.OnvifSolution.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource.Imaging
{
    public interface IFocusModel
    {
        EnumAutoFocusMode AutoFocusMode { get; set; }
        float DefaultSpeed { get; set; }
        float NearLimit { get; set; }
        float FarLimit { get; set; }
    }
}
