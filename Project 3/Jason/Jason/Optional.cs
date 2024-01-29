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

        public IMatch Match(string text)
        {
            return new SuccessMatch(pattern.Match(text).Success() ? pattern.Match(text).RemainingText() : text);
        }
    }
}
