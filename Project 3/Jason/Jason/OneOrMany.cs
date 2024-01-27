using System;

namespace Jason.Jason
{
    internal class OneOrMany
    {
        readonly IPattern pattern;

        public OneOrMany(IPattern pattern)
        {
            this.pattern = new Sequence(pattern, new Many(pattern));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
