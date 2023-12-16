using System;

namespace Jason.Jason
{
    internal class Match : IMatch
    {
        readonly string text;
        readonly char start;
        readonly char end;

        public Match(string text, char start, char end)
        {
            this.text = text;
            this.start = start;
            this.end = end;
        }

        public bool Success()
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            else if (text[0] < start || text[0] > end)
            {
                return false;
            }

            return true;
        }

        public string RemainingText()
        {
            if (!Success())
            {
                return text;
            }

            return text.Substring(1);
        }
    }
}
