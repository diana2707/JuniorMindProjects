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

        public IMatch Match(StringView stringView, StringView failedIndex)
        {
            return !stringView.IsEmpty() && (stringView.Peek() >= start && stringView.Peek() <= end)
                ? new SuccessMatch(stringView.Advance(), failedIndex)
                : new FailedMatch(stringView, stringView.IsIndexLargerThan(failedIndex) ? stringView : failedIndex);
        }
    }
}
