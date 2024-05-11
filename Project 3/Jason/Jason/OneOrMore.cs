using System;

namespace Jason.Jason
{
    internal class OneOrMore : IPattern
    {
        readonly IPattern pattern;

        public OneOrMore(IPattern pattern)
        {
            this.pattern = new Sequence(pattern, new Many(pattern));
        }

        public IMatch Match(StringView stringView)
        {
            return pattern.Match(stringView);
        }
    }
}
