using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaApplication.Models;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views.Components;

public partial class MessageList : UserControl
{
    public MessageList()
    {
        InitializeComponent();
        this.Loaded += MesssageListViewLoaded;
    }

    private void MesssageListViewLoaded(object? sender, RoutedEventArgs e)
    {
        if (App.PrismApplicationLifetime is ISingleViewApplicationLifetime lifetime)
        {
            this.topShow.Margin = new Thickness(12, 40, 12, 8);
        }
    }

    private Point _startPoint;
    private bool _isPressed;
    private const double SwipeThreshold = 80;

    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            _isPressed = true;
            _startPoint = e.GetPosition(border);
            e.Pointer.Capture(sender as IInputElement);
        }
    }

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (!_isPressed || sender is not Border border) return;

        var currentPoint = e.GetPosition(border);
        var delta = _startPoint.X - currentPoint.X;

        // 添加阻尼效果
        if (delta > 0)
        {
            // 当滑动超过阈值时，增加阻力
            if (delta > SwipeThreshold)
            {
                delta = SwipeThreshold + (delta - SwipeThreshold) * 0.2;
            }
        }
        else
        {
            // 向右滑动时添加阻力
            delta = delta * 0.2;
        }

        // 限制最大滑动距离
        delta = Math.Max(0, Math.Min(delta, SwipeThreshold * 1.2));

        border.RenderTransform = new TranslateTransform(-delta, 0);
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        if (sender is not Border border) return;

        _isPressed = false;
        e.Pointer.Capture(null);

        var currentPoint = e.GetPosition(border);
        var delta = _startPoint.X - currentPoint.X;

        // 如果滑动距离超过阈值，保持打开状态
        if (delta > SwipeThreshold / 2)
        {
            border.RenderTransform = new TranslateTransform(-SwipeThreshold, 0);
        }
        else
        {
            border.RenderTransform = new TranslateTransform(0, 0);
        }
    }

    private void DeleteItemClick(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is MessageListViewModel vm)
        {
            var btn = sender as Button;
            if (btn is null) return;
            var messageItem = btn.Tag as MessageItem;
            vm.Remove(messageItem);
        }
    }

    private void OnMessageTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not Border border) return;
        if (this.DataContext is MessageListViewModel vm)
        {
            vm.OpenDialogue(border.Tag as MessageItem);
        }
    }
}