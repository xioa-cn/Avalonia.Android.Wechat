using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using AvaloniaApplication.Models;
using AvaloniaApplication.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Prism.Regions;

namespace AvaloniaApplication.ViewModels;

public partial class CommunicationBookViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public ObservableCollection<ContactGroup> Contacts { get; set; }

    public ObservableCollection<string> Letters { get; set; } = new ObservableCollection<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "#"
    };

    public CommunicationBookViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
        Contacts = new ObservableCollection<ContactGroup>();
        Task.Run(() => { InitializeData(); });
    }

    private void InitializeData()
    {
        foreach (var letter in Letters)
        {
            Contacts.Add(new ContactGroup
            {
                Group = letter, Items = new ObservableCollection<Contact>()
            });
            foreach (var item in Enumerable.Range(0, 3))
            {
                Contacts[Contacts.Count - 1].Items.Add(
                    new Contact
                    {
                        Name = letter + "--" + item,
                        Avatar = ImageHelper.LoadFromWeb(new Uri(
                            "https://c-ssl.dtstatic.com/uploads/blog/202304/15/20230415081411_9a88c.thumb.400_0.jpg"))
                    }
                );
            }
        }
    }


    public void GoView(object? pa)
    {
        this._regionManager.RequestNavigate(BaseViewModel.MainRegion, "userInfomation", new NavigationParameters()
        {
            { "info", pa }
        });
    }


    public ContactGroup? Find(string letter)
    {
        // 找到对应的联系人分组
        var group = Contacts.FirstOrDefault(g => g.Group == letter);
        return group;
    }
}