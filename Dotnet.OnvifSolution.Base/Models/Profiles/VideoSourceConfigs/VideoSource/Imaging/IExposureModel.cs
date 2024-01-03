using Dotnet.OnvifSolution.Base.Enums;
using Dotnet.OnvifSolution.Base.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource.Imaging
{
    public interface IExposureModel
    {
        EnumExposureMode Mode { get; set; }
        EnumExposurePriority Priority { get; set; }

        float ExposureTime { get; set; }
        LevelModel ExposureTimeRange { get; set; }

        float Gain { get; set; }
        LevelModel GainRange { get; set; }

        float Iris { get; set; }
        LevelModel IrisRange { get; set; }
    }
}
