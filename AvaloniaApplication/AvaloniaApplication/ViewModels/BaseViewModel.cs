using AvaloniaApplication.Services;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class BaseViewModel : ViewModelBase,IInitializedable
{
    public const string MainRegion = nameof(MainRegion);
    public const string HomeRegion = nameof(HomeRegion);

    private readonly IRegionManager _regionManager;

    public BaseViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        Initialize();
    }

    public void Initialize()
    {
        _regionManager.RequestNavigate(MainRegion, "MainView");
    }

    
}