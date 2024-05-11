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

        public IMatch Match(StringView stringView)
        {
            return !stringView.IsEmpty() && (stringView.Peek() >= start && stringView.Peek() <= end)
                ? new SuccessMatch(stringView.Advance())
                : new FailedMatch(stringView);
        }
    }
}
