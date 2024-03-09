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
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(remainingText);
                if (!match.Success())
                {
                    return new FailedMatch(text);
                }

                remainingText = match.RemainingText();
            }

            return new SuccessMatch(remainingText);

        }
    }
}
