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
            return new Match(text, start, end);
        }
    }
}
