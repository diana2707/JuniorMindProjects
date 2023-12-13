using Jason.Jason;
using System;

namespace Jason.JasonFacts
{
    class Range : IPattern
    {
        readonly char start;
        readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public IMatch Match(string text)
        {
            bool success = true;

            if (string.IsNullOrEmpty(text))
            {
                success = false;
            }
            else if (text[0] < start || text[0] > end)
            {
                success = false;
            }

            return new Match(text, success);
        }
    }
}
