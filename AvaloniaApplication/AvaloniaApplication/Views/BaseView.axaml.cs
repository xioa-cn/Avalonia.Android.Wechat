using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaApplication.Services;
using AvaloniaApplication.ViewModels;


namespace AvaloniaApplication.Views;

public partial class BaseView : UserControl
{
    public BaseView()
    {
        InitializeComponent();
        this.Loaded += OnInitializedVm;
        return;

        void OnInitializedVm(object? sender, EventArgs e)
        {
            if (App.PrismApplicationLifetime is ISingleViewApplicationLifetime)
            {
                if (this.DataContext is IInitializedable bvm)
                {
                    bvm.Initialize();
                }
            }

            if (this.DataContext is not null) return;

            var vm = App.ContainerHelper?.Resolve(typeof(BaseViewModel));
            this.DataContext = vm;
        }
    }
}