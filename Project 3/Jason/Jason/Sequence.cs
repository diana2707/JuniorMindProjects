using System;

namespace Jason.Jason
{
    class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            IMatch match = new SuccessMatch(currentIndex, failedIndex);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.CurrentIndex(), match.FailedIndex());
                if (!match.Success())
                {
                    return new FailedMatch(currentIndex, match.FailedIndex());
                }
            }

            return match;
        }
    }
}
