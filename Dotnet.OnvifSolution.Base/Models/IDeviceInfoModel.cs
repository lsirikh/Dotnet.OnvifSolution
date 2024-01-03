using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models
{
    public interface IDeviceInfoModel
    {
        string Manufacturer { get; set; }
        string DeviceModel { get; set; }
        string FirmwareVersion { get; set; }
        string SerialNumber { get; set; }
        string HardwareId { get; set; }

        bool IsDevicePossible { get; set; }
        bool IsMediaPossible { get; set; }
        bool IsEventPossible { get; set; }
        bool IsImagingPossible { get; set; }
        bool IsPtzPossible { get; set; }
    }
}
