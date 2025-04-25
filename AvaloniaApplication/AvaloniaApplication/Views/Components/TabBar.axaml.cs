using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Services;

namespace AvaloniaApplication.Views.Components;

public partial class TabBar : UserControl
{
    public TabBar()
    {
        InitializeComponent();
        this.Loaded += VmLoaded;
    }

    private void VmLoaded(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is IInitializedable vm)
        {
            vm.Initialize();
            this.Loaded -= VmLoaded;
        }
    }
}