using Dotnet.OnvifSolution.Base.Models;

namespace Dotnet.OnvifSolution.Models
{
    public interface ICameraDeviceModel : IOnvifClientModel, IConnectionModel, IDeviceInfoModel
    {
        CameraMediaModel CameraMedia { get; set; }
    }
}