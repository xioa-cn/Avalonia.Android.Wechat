using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AvaloniaApplication.Utils;

public static class ImageHelper
{
    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        return new Bitmap(AssetLoader.Open(resourceUri));
    }

    public static  Bitmap LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response =  httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var data =  response.Content.ReadAsByteArrayAsync().Result;
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}