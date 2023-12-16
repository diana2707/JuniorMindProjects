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

        public IMatch Match(string text)
        {
            IPattern pattern = patterns[0];
            string remainingText = text;
            for (int i = 0; i < patterns.Length; i++)
            {
                if (i == patterns.Length - 1 && patterns[i].Match(remainingText).Success())
                {
                    return patterns[i].Match(remainingText);
                }

                remainingText = patterns[i].Match(remainingText).Success() ? patterns[i].Match(text).RemainingText() : text;
            }

            return pattern.Match(remainingText);
        }
    }
}
