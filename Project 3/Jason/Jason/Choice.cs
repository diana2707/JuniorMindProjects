using System;

namespace Jason.Jason
{
    internal class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            IMatch match = new SuccessMatch(currentIndex, failedIndex);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(currentIndex, failedIndex);
                if (match.Success())
                {
                    return match;
                }
            }

            return new FailedMatch(currentIndex, match.FailedIndex());
        }

        public void Add(IPattern pattern)
        {
            Array.Resize(ref patterns, patterns.Length + 1);
            patterns[^1] = pattern;
        }
    }
}
