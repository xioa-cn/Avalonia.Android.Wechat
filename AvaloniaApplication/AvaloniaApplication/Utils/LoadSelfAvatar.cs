using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace AvaloniaApplication.Utils;

public class LoadSelfAvatar
{
    public static string SelfAvatarPath { get; set; } =
        "https://c-ssl.dtstatic.com/uploads/blog/202304/15/20230415081411_9a88c.thumb.400_0.jpg";

    private static Bitmap? _avatarBitmap;

    public static Bitmap? SelfAvatarBitmap
    {
        get
        {
            _avatarBitmap ??= LoadAvatar(SelfAvatarPath);
            return _avatarBitmap;
        }
    }

    public static  Bitmap? LoadAvatar(string url)
    {
        if (string.IsNullOrEmpty(url)) return null;

        try
        {
            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var stream = response.Content.ReadAsStreamAsync();
            var memoryStream = new MemoryStream();
            stream.Result.CopyTo(memoryStream);

            memoryStream.Position = 0;

            return new Bitmap(memoryStream);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}