using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models;
using Dotnet.OnvifSolution.Base.Models.Profiles;
using Dotnet.OnvifSolution.GUI.ViewModels.TreeViews;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Dotnet.OnvifSolution.GUI.ViewModels.Panels
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/26/2023 4:26:37 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public class ProfileViewModel : ContentControlViewModel<CameraMediaModel>
    {

        #region - Ctors -
        public ProfileViewModel(CameraMediaModel model) : base(model)
        {
            CameraProfiles = new ObservableCollection<CameraProfile>();

            foreach (var item in model.Profiles)
            {
                CameraProfiles.Add(item);
            }

            
        }
        #endregion
        #region - Implementation of Interface -
        #endregion
        #region - Overrides -
        #endregion
        #region - Binding Methods -
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProfile = e.AddedItems?.OfType<CameraProfile>().FirstOrDefault();
            if(selectedProfile != null) 
            { 
                ProfileTreeViewModel = new ProfileTreeViewModel(selectedProfile);
            }
        }
        #endregion
        #region - Processes -
        #endregion
        #region - IHanldes -
        #endregion
        #region - Properties -
        public ProfileTreeViewModel ProfileTreeViewModel
        {
            get { return _profileTreeViewModel; }
            set 
            {
                _profileTreeViewModel = value;
                NotifyOfPropertyChange(() => ProfileTreeViewModel);
            }
        }
        public ObservableCollection<CameraProfile> CameraProfiles { get; }
        #endregion
        #region - Attributes -
        private ProfileTreeViewModel _profileTreeViewModel;
        #endregion
    }
}
