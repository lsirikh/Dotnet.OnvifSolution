using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OnvifSolution.Base.Models.Components
{
    public interface IMultiCastModel
    {
        string IpAddress { get; set; }
        int Port { get; set; }
        int Ttl { get; set; }
        bool AutoStart { get; set; }
    }
}
