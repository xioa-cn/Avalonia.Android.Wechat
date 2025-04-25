using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class FindViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public FindViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
    }


    [RelayCommand]
    private void GoView(string viewName)
    {
        this._regionManager.RequestNavigate(BaseViewModel.MainRegion, viewName);
    }
}