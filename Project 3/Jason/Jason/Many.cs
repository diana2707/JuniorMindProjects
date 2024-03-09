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

        public IMatch Match(string text)
        {
            IMatch match = pattern.Match(text);
            if (!match.Success())
            {
                return new SuccessMatch(text);
            }

            string remainingText = match.RemainingText();
            return Match(remainingText);
        }
    }
}