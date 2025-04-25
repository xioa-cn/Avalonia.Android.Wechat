using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Platform;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication.Views;

public partial class DialogueView : UserControl
{
    private bool _isKeyboardVisible = false;

    public DialogueView()
    {
        InitializeComponent();
        this.Loaded += ViewLoaded;
        this.AttachedToVisualTree += OnAttachedToVisualTree;
        
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (App.PrismApplicationLifetime is not ISingleViewApplicationLifetime) return;
        try
        {
            _inputPane = TopLevel.GetTopLevel(this)?.InputPane;
            _inputPane.StateChanged += (sender, args) => { AdjustLayoutForKeyboard(); };
            // 监听文本框获得焦点事件
            TextInput.GotFocus += (s, e) =>
            {
                _isKeyboardVisible = true;
                AdjustLayoutForKeyboard();
            };

            // 监听文本框失去焦点事件
            TextInput.LostFocus += (s, e) =>
            {
                _isKeyboardVisible = false;
                AdjustLayoutForKeyboard();
            };
        }
        catch (Exception ex)
        {
        }
    }


    private IInputPane _inputPane;

    private void AdjustLayoutForKeyboard()
    {
        if (_inputPane?.State == InputPaneState.Open)
        {
            // 键盘显示时，调整布局
            DialogueGrid.Margin = new Thickness(0, 40, 0, _inputPane.OccludedRect.Height +20);
            MessageScroller.ScrollToEnd();
        }
        else
        {
            // 键盘隐藏时，恢复布局
            DialogueGrid.Margin = new Thickness(0, 40, 0, 10);
        }
    }


    private void ViewLoaded(object? sender, RoutedEventArgs e)
    {
        if (App.PrismApplicationLifetime is ISingleViewApplicationLifetime)
        {
            this.DialogueGrid.Margin = new Thickness(0, 40, 0, 10);
            this.TextInput.Margin = new Thickness(0, 10, 0, 10);
        }
    }

    private void BackListView(object? sender, TappedEventArgs e)
    {
        if (this.DataContext is DialogueViewModel vm)
        {
            vm.Back();
        }
    }
}