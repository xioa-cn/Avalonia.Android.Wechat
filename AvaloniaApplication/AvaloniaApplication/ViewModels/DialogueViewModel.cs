using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using AvaloniaApplication.Models;
using AvaloniaApplication.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public static class DialogueUtil
{
    public static Dictionary<string, List<DialogueMessage>> AllMessages { get; set; } = new();
}

public partial class DialogueViewModel : ViewModelBase, INavigationAware
{
    private readonly IRegionManager _regionManager;

    public DialogueViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
    }

    [ObservableProperty] private string? _textInput;
    [ObservableProperty] private string? _dialogueUser;
    private string _userId = "";
    private MessageItem Other;

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        var para = navigationContext.Parameters["messageItem"] as MessageItem;
        this.DialogueUser = para?.Name;
        Other = para;
        _userId = para?.Id;
        var textInput = navigationContext.Parameters["textInput"] as string;
        this.TextInput = textInput;
        DialogueMessages.Clear();
        if (_userId != null && DialogueUtil.AllMessages.ContainsKey(_userId))
        {
            foreach (var item in DialogueUtil.AllMessages[_userId])
            {
                DialogueMessages.Add(item);
            }
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public void Back()
    {
        _regionManager.RequestNavigate(BaseViewModel.MainRegion, "MainView", new NavigationParameters());
    }

    private Random random = new Random();

    [RelayCommand]
    public void SendMessage(ScrollViewer scrollViewer)
    {
        if (!DialogueUtil.AllMessages.TryGetValue(_userId, out _))
        {
            DialogueUtil.AllMessages[_userId] = new();
        }

        var mes = new DialogueMessage
        {
            Content = this.TextInput,
            Person = People.Self,
            Avatar = LoadSelfAvatar.SelfAvatarBitmap
        };
        DialogueMessages.Add(mes);

        DialogueUtil.AllMessages[_userId].Add(mes);
        scrollViewer.ScrollToEnd();
        var text = this.TextInput;
        Task.Run(() =>
        {
            var robotMes = RobotMessage.RequestRobot(text);
            if (string.IsNullOrEmpty(robotMes))
            {
                return;
            }

            var r = random.Next(0, 100);


            var mes1 = new DialogueMessage
            {
                Content = robotMes,
                Person = People.Other,
                Avatar = Other.Avatar,
                IsSelf = false
            };
            DialogueMessages.Add(mes1);

            DialogueUtil.AllMessages[_userId].Add(mes1);
            Dispatcher.UIThread.Invoke(() => { scrollViewer.ScrollToEnd(); });
        });


        this.TextInput = "";
    }

    public ObservableCollection<DialogueMessage> DialogueMessages { get; set; } =
        new ObservableCollection<DialogueMessage>();
}