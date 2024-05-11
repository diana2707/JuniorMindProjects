using System;

namespace Jason.Jason
{
    internal class Optional : IPattern
    {
        readonly IPattern pattern;

        public Optional(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(StringView stringView)
        {
            return new SuccessMatch(pattern.Match(stringView).StringView());
        }
    }
}
