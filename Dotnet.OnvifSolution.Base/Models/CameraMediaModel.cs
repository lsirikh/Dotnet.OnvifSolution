using Dotnet.OnvifSolution.Base.Models.Profiles;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dotnet.OnvifSolution.Base.Models
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/21/2023 10:34:22 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class CameraMediaModel : ICameraMediaModel
    {

        public CameraMediaModel()
        {
            Profiles = new List<CameraProfile>();
        }

        [JsonProperty("profiles", Order = 1)]
        public List<CameraProfile> Profiles { get; set; }

        [JsonIgnore]
        public string ProfileTitle { get; set; }
    }
}
