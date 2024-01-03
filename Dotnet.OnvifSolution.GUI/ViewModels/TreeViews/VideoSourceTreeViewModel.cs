using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource.Imaging;
using System.Runtime.InteropServices;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 2:11:07 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class VideoSourceTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public VideoSourceTreeViewModel(IVideoSourceModel model)
        {
            _model = model;
            if(model.ImagingOption != null)
                ImagingTreeViewModel = new ImagingTreeViewModel(model.ImagingOption);
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

        public float FrameRate
        {
            get { return _model.FrameRate; }
        }

        public string Resolution
        {
            get { return $"{_model.Resolution.Width}X{_model.Resolution.Height}"; }
        }

        public ImagingTreeViewModel ImagingTreeViewModel { get; }
        #endregion
        #region - Attributes -
        private IVideoSourceModel _model;
        #endregion
    }
}
