using System;

namespace Jason.Jason
{
    internal class Text : IPattern
    {
        readonly string prefix;

        public Text(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            return !currentIndex.IsEmpty() && currentIndex.StartsWith(prefix)
                ? new SuccessMatch(currentIndex.Advance(prefix.Length), failedIndex)
                : new FailedMatch(currentIndex, currentIndex.IsIndexLargerThan(failedIndex) ? currentIndex : failedIndex);
        }
    }
}
