using Jason.Jason;
using System;

namespace Jason.Jason
{
    class FailedMatch : IMatch
    {
        readonly StringView stringView;
        readonly int failedIndex;

        public FailedMatch(StringView stringView, int failedIndex)
        {
            this.stringView = stringView;
            this.failedIndex = failedIndex;
        }

        public bool Success() => false;

        public int FailedIndex() => failedIndex;

        public StringView StringView() => stringView;
    }
}