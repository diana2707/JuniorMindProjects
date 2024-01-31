using System;

namespace Jason.Jason
{
    internal class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            pattern = new Character('0');
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
