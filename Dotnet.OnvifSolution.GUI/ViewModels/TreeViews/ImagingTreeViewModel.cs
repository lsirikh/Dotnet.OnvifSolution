using Dotnet.OnvifSolution.Base.Enums;
using Dotnet.OnvifSolution.Base.Models.Profiles.VideoSourceConfigs.VideoSource.Imaging;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 4:28:06 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class ImagingTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public ImagingTreeViewModel(IImagingOptionModel model)
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
        public float Brightness
        {
            get { return _model.Brightness; }
        }
        public float ColorSaturation
        {
            get { return _model.Brightness; }
        }

        public float Contrast
        {
            get { return _model.Brightness; }
        }

        public float Sharpness
        {
            get { return _model.Brightness; }
        }

        public ExposureModel Exposure
        {
            get { return _model.Exposure; }
        }

        public FocusModel Focus
        {
            get { return _model.Focus; }
        }

        public string IrCutFilter
        {
            get { return _model.IrCutFilter.ToString(); }
        }


        public BacklightCompensationModel BacklightCompensation
        {
            get { return _model.BacklightCompensation; }
        }

        public WhiteBalanceModel WhiteBalance
        {
            get { return _model.WhiteBalance; }
        }

        public WideDynamicRangeModel WideDynamicRange
        {
            get { return _model.WideDynamicRange; }
        }
        #endregion
        #region - Attributes -
        private IImagingOptionModel _model;
        #endregion
    }
}
