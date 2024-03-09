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
            if (!pattern.Match(text).Success())
            {
                return new SuccessMatch(text);
            }

            return Match(pattern.Match(text).RemainingText());
        }
    }
}
