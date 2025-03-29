using System;
namespace DfnvUtilities
{
    public class PrinterSeperator(char SeperatorChar, string Identifier, bool IsFullSeperator = false)
    {
        public char SeperatorChar { get; } = SeperatorChar;
        public string Identifier{ get; } = "$#" + Identifier;
        public bool IsFullSeperator { get; } = IsFullSeperator;

        public string Build(int chunkSize, int padding)
        {
            return IsFullSeperator ? " " + new String(SeperatorChar, chunkSize + 2 + 2 * padding) + " " : " +" + new String(SeperatorChar, chunkSize + 2 * padding) + "+ ";
        }
    }
}
