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
            string remainingText = text;
            for (int i = 0; i < patterns.Length; i++)
            {
                bool success = patterns[i].Match(remainingText).Success();
                if (i == patterns.Length - 1 && success)
                {
                    return patterns[i].Match(remainingText);
                }

                if (success)
                {
                    remainingText = patterns[i].Match(remainingText).RemainingText();
                }
                else
                {
                    remainingText = text;
                    break;
                }
            }

            return new FailedMatch(remainingText);
        }
    }
}
