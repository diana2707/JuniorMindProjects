using System;

namespace Jason.Jason
{
    internal class List : IPattern
    {
        readonly IPattern pattern;

        public List(IPattern pattern, IPattern separator)
        {
            this.pattern = new Optional(new Sequence(new Many(new Sequence(pattern, separator)), new Optional(pattern)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
