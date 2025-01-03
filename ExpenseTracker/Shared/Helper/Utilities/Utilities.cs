namespace CodeCommandos.Shared.Helper.Utilities;

public static class Utilities
{
    public static bool IsEmpty(this string text)
    {
        return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
    }
}