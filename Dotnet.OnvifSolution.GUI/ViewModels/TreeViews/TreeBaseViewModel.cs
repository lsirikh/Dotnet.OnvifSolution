using Caliburn.Micro;
using System.Xml.Linq;

namespace Dotnet.OnvifSolution.GUI.ViewModels.TreeViews
{
    /****************************************************************************
        Purpose      :                                                           
        Created By   : GHLee                                                
        Created On   : 12/28/2023 1:52:00 PM                                                    
        Department   : SW Team                                                   
        Company      : Sensorway Co., Ltd.                                       
        Email        : lsirikh@naver.com                                         
     ****************************************************************************/

    public abstract class TreeBaseViewModel : PropertyChangedBase 
    {

        #region - Ctors -
        public TreeBaseViewModel()
        {
            TreeName = this.GetType().Name;
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
        public string TreeName
        {
            get => _treeName;
            set => Set(ref _treeName, value);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => Set(ref _isExpanded, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        #endregion
        #region - Attributes -
        private string _treeName;
        private bool _isExpanded;
        private bool _isSelected;
        #endregion
    }
}
