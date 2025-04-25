using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views;

public partial class UserInfomationView : UserControl
{
    public UserInfomationView()
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

    private void BackListView(object? sender, TappedEventArgs e)
    {
        if (this.DataContext is UserInfomationViewModel vm)
        {
            vm.GoBackMainView();
        }
    }
}