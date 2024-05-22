using Jason.Jason;

namespace Jason
{
    class Range : IPattern
    {
        readonly char start;
        readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            return !currentIndex.IsEmpty() && (currentIndex.Peek() >= start && currentIndex.Peek() <= end)
                ? new SuccessMatch(currentIndex.Advance(), failedIndex)
                : new FailedMatch(currentIndex, currentIndex.IsIndexLargerThan(failedIndex) ? currentIndex : failedIndex);
        }
    }
}
