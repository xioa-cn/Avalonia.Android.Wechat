using System;
using System.Text.Json.Serialization;

using RestSharp;

namespace AvaloniaApplication.Utils;

public static class RobotMessage
{
    private static string url = "http://api.qingyunke.com/api.php?key=free&appid=0&msg=";

    public static string RequestRobot(string? message)
    {
        if (string.IsNullOrEmpty(message))
            return "";
        try
        {
            var u = url + message;
            var client = new RestClient(u);
            
            var request = new RestRequest();
            request.Timeout = TimeSpan.FromSeconds(5000);
            request.Method = Method.Get;
            //request.AddHeader("Authorization", "{{token}}");
          
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Host", "api.qingyunke.com");
            request.AddHeader("Connection", "keep-alive");
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            var r = System.Text.Json.JsonSerializer.Deserialize<MessageRobot>(response.Content);

            return r.Content;
        }
        catch (Exception ex)
        {
            return "我在忙 晚点回你！";
        }
    }

    public static void Test()
    {
        var resu = RequestRobot("你好");
    }
}

public class MessageRobot
{
    [JsonPropertyName("result")] public int Result { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}