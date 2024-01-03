using Dotnet.OnvifSolution.Base.Models.Components;
using Dotnet.OnvifSolution.Base.Models.Profiles.MetadataConfigs;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 1/3/2024 10:39:00 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class MetadataConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public MetadataConfigTreeViewModel(IMetadataConfigModel model)
        {
            _model = model;
            if (_model.MultiCast != null)
                MulticastTreeViewModel = new MulticastTreeViewModel(_model.MultiCast);
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
        public string SessionTimeout => _model.SessionTimeout;
        public bool Analytics => _model.Analytics;
        public bool PTZStatus => _model.PTZStatus?.Status != null ? _model.PTZStatus.Status : false;
        public bool PTZPosition => _model.PTZStatus?.Position != null ? _model.PTZStatus.Position : false;
        public bool EventFilter => _model.EventSubscription?.Filter != null ? true : false;
        public bool EventSubscriptionPolicy => _model.EventSubscription?.SubscriptionPolicy != null ? true : false;
        public MulticastTreeViewModel MulticastTreeViewModel { get; }
        #endregion
        #region - Attributes -
        private IMetadataConfigModel _model;
        #endregion
    }
}
