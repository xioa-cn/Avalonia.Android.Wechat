using System;
using AvaloniaApplication.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaApplication.Models;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class MessageListViewModel : ViewModelBase, IInitializedable
{
    [ObservableProperty] private string? _title;
    [ObservableProperty] private ObservableCollection<MessageItem> _messages;

    private IRegionManager _regionManager;

    public MessageListViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
        _messages = new ObservableCollection<MessageItem>();
        Initialize();
    }

    [RelayCommand]
    public void OpenDialogue(MessageItem? messageItem)
    {
        if (messageItem != null)
            this._regionManager.RequestNavigate(BaseViewModel.MainRegion, "dialogue", new NavigationParameters()
            {
                { "messageItem", messageItem },
                { "textInput", "" }
            });
    }

    private string[] avaurls =
    [
        "https://c-ssl.dtstatic.com/uploads/blog/202304/29/20230429194620_cd903.thumb.400_0.jpg",
        "https://c-ssl.dtstatic.com/uploads/blog/202304/15/20230415081411_9a88c.thumb.400_0.jpg",
        "https://img.keaitupian.cn/newupload/11/1667371769659239.png",
        "https://ss2.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=1139261305,2277749928&fm=253&gp=0.jpg",
    ];

    public async void Initialize()
    {
        foreach (var a in avaurls)
        {
            var messageItem = new MessageItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "XIOA\ud83d\udcab",
                Message = "有一条信息",
                AvatarUrl = a,
                Time = DateTime.Now.ToString("HH:mm"),
            };
            Messages.Add(messageItem);
        }
    }


    public void Remove(MessageItem? messageItem)
    {
        if (messageItem != null)
            Messages.Remove(messageItem);
    }

    [RelayCommand]
    private void DeleteMessage(MessageItem messageItem)
    {
    }
}