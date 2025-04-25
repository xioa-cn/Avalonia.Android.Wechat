using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Avalonia;
using Avalonia.Android;

namespace AvaloniaApplication.Android;

[Activity(
    Label = "AvaloniaApplication.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // 设置全屏显示
        Window?.AddFlags(WindowManagerFlags.LayoutNoLimits);
        Window?.AddFlags(WindowManagerFlags.LayoutInScreen);

        // 如果需要处理刘海屏
        if (Build.VERSION.SdkInt > BuildVersionCodes.P && Window?.Attributes != null)
#pragma warning disable CA1416
            Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
#pragma warning restore CA1416
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}