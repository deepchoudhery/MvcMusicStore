using Microsoft.AspNetCore.Html;

namespace MvcMusicStore
{
    public static class CustomHelpers
    {
        public static IHtmlContent Truncate(string input, int length)
        {
            if (input == null) return new HtmlString(string.Empty);
            if (input.Length <= length) return new HtmlString(System.Net.WebUtility.HtmlEncode(input));
            return new HtmlString($"<span title=\"{System.Net.WebUtility.HtmlEncode(input)}\">{System.Net.WebUtility.HtmlEncode(input.Substring(0, length))}...</span>");
        }
    }
}