using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;

namespace AvaloniaApplication.Models;

public class ContactGroup
{
    public string Group { get; set; }
    public ObservableCollection<Contact> Items { get; set; }
}

public class Contact
{
    public string Name { get; set; }
    public Bitmap Avatar { get; set; }
}