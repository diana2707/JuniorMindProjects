using Jason.Jason;
using System;

namespace Jason.Jason
{
    class SuccessMatch : IMatch
    {
        readonly StringView stringView;
        readonly StringView failedIndex;

        public SuccessMatch(StringView stringView, StringView failedIndex)
        {
            this.stringView = stringView;
            this.failedIndex = failedIndex;
        }

        public bool Success() => true;

        public StringView FailedIndex() => failedIndex;

        public StringView StringView() => stringView;
    }
}
