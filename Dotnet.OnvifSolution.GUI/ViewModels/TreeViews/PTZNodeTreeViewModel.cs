using Dotnet.OnvifSolution.Base.Models.Profiles.PtzConfigs;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/29/2023 11:12:40 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class PTZNodeTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public PTZNodeTreeViewModel(IPTZNode model)
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
        public bool HomeSupported => _model.HomeSupported;
        public int MaximumPresets => _model.MaximumNumberOfPresets;
        public string AuxiliaryCommands => _model.AuxiliaryCommands != null ? string.Join(", ", _model.AuxiliaryCommands) : null;
        public string SupportedPTZSpaces => _model.SupportedPTZSpaces;
        #endregion
        #region - Attributes -
        private IPTZNode _model;
        #endregion
    }
}
