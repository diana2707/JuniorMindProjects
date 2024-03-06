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
                IMatch match = patterns[i].Match(remainingText);
                bool success = match.Success();
                if (i == patterns.Length - 1 && success)
                {
                    return match;
                }

                if (success)
                {
                    remainingText = match.RemainingText();
                }
                else
                {
                    break;
                }
            }

            return new FailedMatch(text);
        }
    }
}
