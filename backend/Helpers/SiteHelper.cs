using System.Drawing;
using System.Runtime.CompilerServices;
using backend.Constants;

namespace backend.Helpers;

public static class SiteHelper
{
    public static int ValidatePageSize(int pageSize)
    {
        List<int> validPageSizes = new List<int>() { 10, 25, 50 };
        return validPageSizes.Contains(pageSize) ? pageSize : SiteConstant.DefaultPageSize;
    }

    public static string RandomString(int length)
    {
        Random random = new();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string RandomNumber(int length)
    {
        Random random = new();

        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string RandomColorFromUuid(Guid id)
    {
        Color color = Color.FromArgb(id.GetHashCode());
        return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
    }

    public static string GetCurrentMethodName([CallerMemberName] string name = "")
    {
        return name;
    }
}