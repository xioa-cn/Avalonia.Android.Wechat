using AvaloniaApplication.Services;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class MainViewModel : ViewModelBase, IInitializedable
{
    private readonly IRegionManager _regionManager;

    public MainViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void Initialize()
    {
        _regionManager.RequestNavigate(BaseViewModel.HomeRegion, "message");
    }


    #region region views

    [RelayCommand]
    private void MainNavigateToView(string viewName)
    {
        _regionManager.RequestNavigate(BaseViewModel.HomeRegion, viewName);
    }

    #endregion
}