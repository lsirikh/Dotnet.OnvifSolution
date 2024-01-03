using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models.Profiles;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 2:20:54 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class ProfileTreeViewModel : PropertyChangedBase
    {

        #region - Ctors -
        public ProfileTreeViewModel(ICameraProfile model)
        {
            _model = model;
            //Children = new BindableCollection<TreeBaseViewModel>() { new VideoSourceConfigTreeViewModel(model.VideoSourceConfig) };
            if(model.VideoSourceConfig != null)
                VideoSourceConfigTreeViewModel = new VideoSourceConfigTreeViewModel(model.VideoSourceConfig);
            if(model.AudioSourceConfig != null)
                AudioSourceConfigTreeViewModel = new AudioSourceConfigTreeViewModel(model.AudioSourceConfig);
            if(model.VideoEncoderConfig != null)
                VideoEncoderConfigTreeViewModel = new VideoEncoderConfigTreeViewModel(model.VideoEncoderConfig);
            if (model.AudioEncoderConfig != null)
                AudioEncoderConfigTreeViewModel = new AudioEncoderConfigTreeViewModel(model.AudioEncoderConfig);
            if(model.PTZConfig != null)
                PTZConfigTreeViewModel = new PTZConfigTreeViewModel(model.PTZConfig);
            if(model.VideoAnalyticsConfig != null)
                VideoAnalyticsConfigTreeViewModel = new VideoAnalyticsConfigTreeViewModel(model.VideoAnalyticsConfig);
            if(model.MetadataConfig != null)
                MetadataConfigTreeViewModel = new MetadataConfigTreeViewModel(model.MetadataConfig);
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

        public bool Fixed
        {
            get { return _model.Fixed; }
        }
        public VideoSourceConfigTreeViewModel VideoSourceConfigTreeViewModel { get; }
        public AudioSourceConfigTreeViewModel AudioSourceConfigTreeViewModel { get; }
        public VideoEncoderConfigTreeViewModel VideoEncoderConfigTreeViewModel { get; }
        public AudioEncoderConfigTreeViewModel AudioEncoderConfigTreeViewModel { get; }
        public PTZConfigTreeViewModel PTZConfigTreeViewModel { get; }
        public VideoAnalyticsConfigTreeViewModel VideoAnalyticsConfigTreeViewModel { get; }
        public MetadataConfigTreeViewModel MetadataConfigTreeViewModel { get; }

        //public BindableCollection<TreeBaseViewModel> Children { get; }
        #endregion
        #region - Attributes -
        private ICameraProfile _model;
        #endregion
    }
}
