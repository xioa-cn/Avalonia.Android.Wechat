using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication.Views;

public partial class FindView : UserControl
{
    public FindView()
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
}