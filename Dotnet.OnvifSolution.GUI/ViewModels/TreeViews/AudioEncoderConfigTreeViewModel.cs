using Dotnet.OnvifSolution.Base.Models.Profiles.AudioEncoderConfigs;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/29/2023 10:47:56 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class AudioEncoderConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public AudioEncoderConfigTreeViewModel(IAudioEncoderConfigModel model)
        {
            _model = model;
            if(model.MultiCast != null)
                MulticastTreeViewModel = new MulticastTreeViewModel(model.MultiCast);
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
        public float Bitrate => _model.Bitrate;
        public float SampleRate => _model.SampleRate;
        public string SessionTimeout => _model.SessionTimeout.ToString();
        public MulticastTreeViewModel MulticastTreeViewModel { get; set; }
        #endregion
        #region - Attributes -
        private IAudioEncoderConfigModel _model;
        #endregion
    }
}
