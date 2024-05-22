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

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            IMatch match = pattern.Match(currentIndex, failedIndex);
            return new SuccessMatch(match.CurrentIndex(), match.FailedIndex());
        }
    }
}
