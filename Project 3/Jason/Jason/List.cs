using System;

namespace Jason.Jason
{
    internal class List : IPattern
    {
        readonly IPattern pattern;

        public List(IPattern pattern, IPattern separator)
        {
            this.pattern = new Sequence(new Many(new Sequence(pattern, separator)), pattern);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
