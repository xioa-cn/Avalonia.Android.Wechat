using System;
using AvaloniaApplication.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class UserInfomationViewModel : ViewModelBase, INavigationAware
{
    private readonly IRegionManager _regionManager;

    public UserInfomationViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
    }
    [ObservableProperty] private Contact _contact = new Contact();

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        var para = navigationContext.Parameters["info"];
        if (para is Contact contact)
        {
            Contact = contact;
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    [RelayCommand]
    private void SendMes()
    {
        MessageItem messageItem = new MessageItem()
        {
            Avatar = this.Contact.Avatar,
            Id = Guid.NewGuid().ToString(),
            Name = this.Contact.Name,
            Time = DateTime.Now.ToString("HH:mm:ss"),
        };
        this._regionManager.RequestNavigate(BaseViewModel.MainRegion, "dialogue", new NavigationParameters()
        {
            { "messageItem", messageItem },
            { "textInput", "" }
        });
    }
    

    public void GoBackMainView()
    {
        _regionManager.RequestNavigate(BaseViewModel.MainRegion, "MainView");
    }
}