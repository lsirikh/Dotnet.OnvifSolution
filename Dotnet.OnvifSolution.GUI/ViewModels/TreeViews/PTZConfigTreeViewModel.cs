using Dotnet.OnvifSolution.Base.Models.Profiles.PtzConfigs;
using System.Runtime.InteropServices;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/29/2023 11:29:20 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class PTZConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public PTZConfigTreeViewModel(IPTZConfigModel model)
        {
            _model = model;
            if (_model.PTZNode != null)
                PTZNodeTreeViewModel = new PTZNodeTreeViewModel(_model.PTZNode);

        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        #endregion
        #region - Processes -
        public string Name => _model.Name;
        public string Token => _model.Token;
        public int UseCount => _model.UseCount;
        public string PanTiltLimits => $"X({_model.PanTiltRange.XRange.Min}, {_model.PanTiltRange.XRange.Max})" +
            $", Y({_model.PanTiltRange.YRange.Min}, {_model.PanTiltRange.YRange.Max})";
        public string ZoomLimits => $"X({_model.ZoomRange.XRange.Min}, {_model.ZoomRange.XRange.Max})";
        public string DefaultPTZSpeed => $"(pan={_model.DefaultPTZSpeed.PanTilt.X}, tilt={_model.DefaultPTZSpeed.PanTilt.Y}, zoom={_model.DefaultPTZSpeed.Zoom.X})";
        public string DefaultPTZTimeout => _model.Timeout;

        public string DefaultAbsolutePantTiltPositionSpace => _model.DefaultAbsolutePantTiltPositionSpace;
        public string DefaultAbsoluteZoomPositionSpace => _model.DefaultAbsoluteZoomPositionSpace;

        public string DefaultContinuousPantTiltelocitySpace => _model.DefaultContinuousPanTiltVelocitySpace;
        public string DefaultContinuousZoomVelocitySpace => _model.DefaultContinuousZoomVelocitySpace;

        public string DefaultRelativePanTiltTranslationSpace => _model.DefaultRelativePanTiltTranslationSpace;
        public string DefaultRelativeZoomTranslationSpace => _model.DefaultRelativeZoomTranslationSpace;
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        public PTZNodeTreeViewModel PTZNodeTreeViewModel { get; }
        #endregion
        #region - Attributes -
        private IPTZConfigModel _model;
        #endregion
    }
}
