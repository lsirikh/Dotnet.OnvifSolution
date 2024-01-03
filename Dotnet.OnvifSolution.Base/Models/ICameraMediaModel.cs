using Dotnet.OnvifSolution.Base.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models
{
    public interface ICameraMediaModel
    {
        List<CameraProfile> Profiles { get; set; }
    }
}
