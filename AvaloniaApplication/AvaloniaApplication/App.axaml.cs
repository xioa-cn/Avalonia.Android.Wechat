using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;
using AvaloniaApplication.Views.Components;
using Prism.DryIoc;
using Prism.Ioc;

namespace AvaloniaApplication;

public partial class App : PrismApplication
{
    public static IApplicationLifetime? PrismApplicationLifetime { get; private set; }
    public static IContainerProvider? ContainerHelper { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        DisableAvaloniaDataAnnotationValidation();
        base.OnFrameworkInitializationCompleted();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            containerRegistry.Register<MainWindow>();
        }

        containerRegistry.RegisterForNavigation<BaseView, BaseViewModel>();
        // 注册视图
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>("MainView");
        containerRegistry.RegisterForNavigation<MessageList, MessageListViewModel>("message");
        containerRegistry.RegisterForNavigation<UserMainView>("me");
        containerRegistry.RegisterForNavigation<DialogueView, DialogueViewModel>("dialogue");
        containerRegistry.RegisterForNavigation<FindView,FindViewModel>("find");
        containerRegistry.RegisterForNavigation<CommunicationBookView,CommunicationBookViewModel>("communication");
        containerRegistry.RegisterForNavigation<UserInfomationView,UserInfomationViewModel>("userInfomation");
    }

    protected override AvaloniaObject CreateShell()
    {
        PrismApplicationLifetime = ApplicationLifetime;
        ContainerHelper = Container;
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            var mainWindow = Container.Resolve<MainWindow>();

            return mainWindow;
        }

        if (ApplicationLifetime is ISingleViewApplicationLifetime)
        {
            return Container.Resolve<BaseView>();
        }

        return null;
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // 获取所有类型为 DataAnnotationsValidationPlugin 的验证器
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // 遍历并移除每个找到的验证器
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}