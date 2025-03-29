namespace DfnvUtilities
{
    internal class Helpers
    {
        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).Append(str.Substring(str.Length - str.Length % chunkSize, str.Length % chunkSize));
        }

        public static string Pad(string text, int padding, int chunkSize)
        {
            return " +" + new String(' ', padding) + text + new String(' ', padding + (chunkSize - text.Length)) + "+ ";
        }
    }
}
