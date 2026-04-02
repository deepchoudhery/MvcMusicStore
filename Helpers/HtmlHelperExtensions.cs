using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace MvcMusicStore.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent Truncate(this IHtmlHelper htmlHelper, string? input, int length)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= length)
                return new HtmlString(HtmlEncoder.Default.Encode(input ?? string.Empty));

            var encodedFull = HtmlEncoder.Default.Encode(input);
            var encodedTruncated = HtmlEncoder.Default.Encode(input.Substring(0, length));
            return new HtmlString($"<span title=\"{encodedFull}\">{encodedTruncated}...</span>");
        }
    }
}
