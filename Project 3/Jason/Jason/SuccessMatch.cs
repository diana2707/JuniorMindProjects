using Jason.Jason;
using System;

namespace Jason.Jason
{
    class SuccessMatch : IMatch
    {
        readonly StringView stringView;
        readonly int failedIndex;

        public SuccessMatch(StringView stringView, int failedIndex)
        {
            this.stringView = stringView;
            this.failedIndex = failedIndex;
        }

        public bool Success() => true;

        public int FailedIndex() => failedIndex;

        public StringView StringView() => stringView;
    }
}
