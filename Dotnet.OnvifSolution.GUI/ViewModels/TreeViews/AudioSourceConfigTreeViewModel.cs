using Dotnet.OnvifSolution.Base.Models.Profiles.AudioSourceConfigs;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 5:17:29 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class AudioSourceConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public AudioSourceConfigTreeViewModel(IAudioSourceConfigModel model)
        {
            _model = model;
            Debug.WriteLine($"Name : {model.Name}, Token : {model.Token}, UseCount : {model.UseCount}");
            if(model.AudioSource != null)
                AudioSourceTreeViewModel = new AudioSourceTreeViewModel(model.AudioSource);

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
        public string Name
        {
            get { return _model.Name; }
        }

        public string Token
        {
            get { return _model.Token; }
        }

        public int UseCount
        {
            get { return _model.UseCount; }
        }

        public AudioSourceTreeViewModel AudioSourceTreeViewModel { get; }
        #endregion
        #region - Attributes -
        private IAudioSourceConfigModel _model;
        #endregion
    }
}
