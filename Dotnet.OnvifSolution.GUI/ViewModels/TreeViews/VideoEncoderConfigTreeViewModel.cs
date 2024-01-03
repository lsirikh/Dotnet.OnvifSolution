using Dotnet.OnvifSolution.Base.Models.Profiles.VideoEncoderConfigs;
using System.Runtime.InteropServices;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/29/2023 10:09:03 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class VideoEncoderConfigTreeViewModel : TreeBaseViewModel
    {
        #region - Ctors -
        public VideoEncoderConfigTreeViewModel(IVideoEncoderConfigModel model)
        {
            _model = model;
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        public string Name => _model.Name;
        public string Token => _model.Token;
        public int UseCount => _model.UseCount;
        public string Encoding => _model.Encoding.ToString();
        public string Resolution => $"{_model.Resolution.Width}X{_model.Resolution.Height}";
        public string SessionTimeout => _model.SessionTimeout.ToString();
        public float Quality => _model.Quality;
        public float FrameRate => _model.FrameRate;
        public float Bitrate => _model.Bitrate;
        public int EncodingInterval => _model.EncodingInterval;
        #endregion
        #region - Attributes -
        private IVideoEncoderConfigModel _model;
        #endregion
    }
}
