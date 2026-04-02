namespace MvcMusicStore.Helpers
{
    public static class StringHelper
    {
        public static string Truncate(string input, int length)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= length)
                return input ?? string.Empty;
            return input.Substring(0, length) + "...";
        }
    }
}
