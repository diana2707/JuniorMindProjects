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

        public IMatch Match(StringView stringView)
        {
            IMatch match = new SuccessMatch(stringView);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.StringView());
                if (!match.Success())
                {
                    return new FailedMatch(stringView);
                }
            }

            return match;
        }
    }
}
