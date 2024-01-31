using System;

namespace Jason.Jason
{
    internal class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            pattern = new OneOrMore(new Range('0', '9'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
