using System;

namespace Jason.Jason
{
    internal class List : IPattern
    {
        readonly IPattern pattern;

        public List(IPattern pattern, IPattern separator)
        {
            this.pattern = new Optional(new Sequence(pattern, new Many(new Sequence(separator, pattern))));
        }

        public IMatch Match(StringView stringView)
        {
            return pattern.Match(stringView);
        }
    }
}
