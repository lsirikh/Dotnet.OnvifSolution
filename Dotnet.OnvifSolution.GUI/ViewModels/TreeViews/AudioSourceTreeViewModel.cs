using Dotnet.OnvifSolution.Base.Models.Profiles.AudioSourceConfigs.AudioSource;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 5:35:24 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class AudioSourceTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public AudioSourceTreeViewModel(AudioSourceModel model)
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
        public string Token
        {
            get { return _model.Token; }
        }

        public int Channels
        {
            get { return _model.Channels; }
        }
        #endregion
        #region - Attributes -
        private AudioSourceModel _model;
        #endregion
    }
}
