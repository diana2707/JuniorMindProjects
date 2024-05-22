using Jason.Jason;
using System;

namespace Jason.Jason
{
    internal class Many : IPattern
    {
        readonly IPattern pattern;

        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            IMatch match = pattern.Match(currentIndex, failedIndex);
            while (match.Success())
            {
                match = pattern.Match(match.CurrentIndex(), match.FailedIndex());
            }

            return new SuccessMatch(match.CurrentIndex(), match.FailedIndex());
        }
    }
}