using Dotnet.OnvifSolution.Base.Models.Components;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/29/2023 10:50:29 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class MulticastTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public MulticastTreeViewModel(IMultiCastModel model)
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
        public string IpAddress => _model.IpAddress;
        public int Port => _model.Port;
        public int TTL => _model.Ttl;
        public string AutoStart => _model.AutoStart.ToString();
        #endregion
        #region - Attributes -
        private IMultiCastModel _model;
        #endregion
    }
}
