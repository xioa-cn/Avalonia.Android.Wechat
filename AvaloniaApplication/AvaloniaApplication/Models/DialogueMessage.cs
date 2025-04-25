using Avalonia.Media.Imaging;

namespace AvaloniaApplication.Models;

public enum People
{
    None,
    Self,
    Other,
}

public class DialogueMessage
{
    public bool IsSelf
    {
        get;
        set;
    } = true;

    public People Person { get; set; }
    public string? Content { get; set; }
    public Bitmap? Avatar { get; set; }
}