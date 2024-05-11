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

        public IMatch Match(StringView stringView)
        {
            IMatch match = pattern.Match(stringView);
            while (match.Success())
            {
                match = pattern.Match(match.StringView());
            }

            return new SuccessMatch(match.StringView());
        }
    }
}