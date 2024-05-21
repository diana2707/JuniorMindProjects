using Jason.Jason;
using System;

namespace Jason.Jason
{
    class FailedMatch : IMatch
    {
        readonly StringView stringView;
        readonly StringView failedIndex;

        public FailedMatch(StringView stringView, StringView failedIndex)
        {
            this.stringView = stringView;
            this.failedIndex = failedIndex;
        }

        public bool Success() => false;

        public StringView FailedIndex() => failedIndex;

        public StringView StringView() => stringView;
    }
}