using Microsoft.AspNetCore.Html;

namespace MvcMusicStore.Helpers
{
    public static class CustomHelpers
    {
        public static IHtmlContent Truncate(string input, int length)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= length)
                return new HtmlString(System.Net.WebUtility.HtmlEncode(input ?? string.Empty));

            return new HtmlString($"<span title=\"{System.Net.WebUtility.HtmlEncode(input)}\">{System.Net.WebUtility.HtmlEncode(input.Substring(0, length))}...</span>");
        }
    }
}
