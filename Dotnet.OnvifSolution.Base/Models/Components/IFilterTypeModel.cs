using System.Collections.Generic;
using System.Xml;

namespace Dotnet.OnvifSolution.Base.Models.Components
{
    public interface IFilterTypeModel
    {
        List<XmlElement> Any { get; set; }
    }
}