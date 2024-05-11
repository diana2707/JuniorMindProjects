using Jason.Jason;
using System;

namespace Jason
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(StringView stringView)
        {
            return !stringView.IsEmpty() && stringView.Peek() == pattern
                ? new SuccessMatch(stringView.Advance())
                : new FailedMatch(stringView);
        }
    }
}