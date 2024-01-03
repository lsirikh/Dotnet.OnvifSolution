using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 1:54:41 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class VideoSourceConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public VideoSourceConfigTreeViewModel(IVideoSourceConfigModel model)
        {
            _model = model;
            //if(model.VideoSource != null)
            //    Children = new BindableCollection<VideoSourceTreeViewModel>();
            VideoSourceTreeViewModel = new VideoSourceTreeViewModel(model.VideoSource);
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

        public string Bounds
        {
            get { return $"({_model.Bounds[0]}, {_model.Bounds[1]}, {_model.Bounds[2]}, {_model.Bounds[3]})"; }
        }

        //public BindableCollection<VideoSourceTreeViewModel> Children { get; }
        public VideoSourceTreeViewModel VideoSourceTreeViewModel { get; }
        #endregion
        #region - Attributes -
        private IVideoSourceConfigModel _model;

        #endregion
    }
}
