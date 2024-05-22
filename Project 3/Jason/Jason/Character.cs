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

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            return !currentIndex.IsEmpty() && currentIndex.Peek() == pattern
                ? new SuccessMatch(currentIndex.Advance(), failedIndex)
                : new FailedMatch(currentIndex, currentIndex.IsIndexLargerThan(failedIndex) ? currentIndex : failedIndex);
        }
    }
}