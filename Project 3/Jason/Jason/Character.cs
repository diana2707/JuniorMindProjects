using Jason.Jason;
using System;

namespace Jason
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(text, pattern, pattern);
        }
    }
}
