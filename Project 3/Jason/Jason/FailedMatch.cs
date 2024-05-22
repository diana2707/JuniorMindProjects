using Jason.Jason;
using System;

namespace Jason.Jason
{
    class FailedMatch : IMatch
    {
        readonly StringView currentIndex;
        readonly StringView failedIndex;

        public FailedMatch(StringView currentIndex, StringView failedIndex)
        {
            this.currentIndex = currentIndex;
            this.failedIndex = failedIndex;
        }

        public bool Success() => false;

        public StringView FailedIndex() => failedIndex;

        public StringView CurrentIndex() => currentIndex;
    }
}