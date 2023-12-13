using System;

namespace Jason.Jason
{
    internal class Match : IMatch
    {
        readonly bool success;
        readonly string text;

        public Match(string text, bool success)
        {
            this.text = text;
            this.success = success;
        }

        public bool Success()
        {
            return success;
        }

        public string RemainingText()
        {
            return text.Substring(1);
        }

    }
}
