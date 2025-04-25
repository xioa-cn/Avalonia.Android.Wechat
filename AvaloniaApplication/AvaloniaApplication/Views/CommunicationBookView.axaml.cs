using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using AvaloniaApplication.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaApplication.Views;

public partial class CommunicationBookView : UserControl
{
    public CommunicationBookView()
    {
        InitializeComponent();
        this.Loaded += (s, e) =>
        {
            if (App.PrismApplicationLifetime is ISingleViewApplicationLifetime)
            {
                this.Header.Margin = new Thickness(0, 40, 0, 10);

            }
        };
    }

    private void ScrHelperClick(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is not CommunicationBookViewModel vm) return;
        if ((sender as Button)?.Tag is string tag)
        {
            var re = vm.Find(tag);
            var group = this.GetVisualDescendants()
                .OfType<StackPanel>()
                .FirstOrDefault(sp => sp.Name == re.Group);
            if (group != null)
            {
                // 滚动到指定分组
                group.BringIntoView();
            }
        }
    }

    private void GoViewClick(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is CommunicationBookViewModel vm)
        {
            var pa = sender as Button;
            if (pa != null)
                vm.GoView(pa.Tag);
        }
    }
}