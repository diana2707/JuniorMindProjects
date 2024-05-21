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

        public IMatch Match(StringView stringView, StringView failedIndex)
        {
            return !stringView.IsEmpty() && stringView.Peek() == pattern
                ? new SuccessMatch(stringView.Advance(), failedIndex)
                : new FailedMatch(stringView, stringView.IsIndexLargerThan(failedIndex) ? stringView : failedIndex);
        }
    }
}