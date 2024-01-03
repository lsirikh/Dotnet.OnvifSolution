using Dotnet.OnvifSolution.Base.Models.Profiles.VideoAnalyticConfigs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 1/3/2024 9:29:44 AM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class VideoAnalyticsConfigTreeViewModel : TreeBaseViewModel
    {

        #region - Ctors -
        public VideoAnalyticsConfigTreeViewModel(IVideoAnalyticsConfigModel model)
        {
            _model = model;
            if(model.Analytics.Count > 0) 
            {
                Analytics = new ObservableCollection<string>();
                foreach (var item in model.Analytics)
                {
                    Analytics.Add(item);
                }
                NotifyOfPropertyChange(() => Analytics);
            }
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
        public ObservableCollection<string> Analytics { get; set; }
        #endregion
        #region - Attributes -
        private IVideoAnalyticsConfigModel _model;
        #endregion
    }
}
