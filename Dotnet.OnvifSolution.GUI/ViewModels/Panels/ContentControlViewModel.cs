using Caliburn.Micro;
using Dotnet.OnvifSolution.Base.Models;

namespace Dotnet.OnvifSolution.GUI.ViewModels.Panels
{
    public abstract class ContentControlViewModel<T> : PropertyChangedBase 
        where T : CameraMediaModel
    {

        public ContentControlViewModel(T model)
        {
            _model = model;
        }


        public CameraMediaModel Model => _model;

        private T _model;
    }
}