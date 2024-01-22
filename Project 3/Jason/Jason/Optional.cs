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
            return pattern.Match(text).Success() ? new SuccessMatch(pattern.Match(text).RemainingText()) : new SuccessMatch(text);
        }
    }
}
