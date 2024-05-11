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

        public IMatch Match(StringView stringView)
        {
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(stringView);
                if (match.Success())
                {
                    return match;
                }
            }

            return new FailedMatch(stringView);
        }

        public void Add(IPattern pattern)
        {
            Array.Resize(ref patterns, patterns.Length + 1);
            patterns[^1] = pattern;
        }
    }
}
