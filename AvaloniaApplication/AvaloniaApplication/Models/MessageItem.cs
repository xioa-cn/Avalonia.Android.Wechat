using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Common;

namespace AvaloniaApplication.Models;

public class MessageItem : ObservableObject
{
    private Bitmap? _avatarBitmap;

    private string _avatarUrl;

    public string AvatarUrl
    {
        get => _avatarUrl;
        set
        {
            _avatarUrl = value;
            LoadAvatar();
        }
    }

    public Bitmap? Avatar
    {
        get => _avatarBitmap;
        set => SetProperty(ref _avatarBitmap, value);
    }
    public string Id { get; set; }
    public string? Name { get; set; }
    public string Message { get; set; }
    public string Time { get; set; }

    public async Task LoadAvatar()
    {
        if (string.IsNullOrEmpty(AvatarUrl)) return;

        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(AvatarUrl);
            var stream = await response.Content.ReadAsStreamAsync();
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            Avatar = new Bitmap(memoryStream);
        }
        catch (Exception ex)
        {
            // 加载失败时不设置头像，将使用默认头像
        }
    }
}