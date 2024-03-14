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
            IMatch match = null;
            foreach (var pattern in patterns)
            {
                match = pattern.Match(remainingText);
                if (!match.Success())
                {
                    return new FailedMatch(text);
                }

                remainingText = match.RemainingText();
            }

            return match;
        }
    }
}
