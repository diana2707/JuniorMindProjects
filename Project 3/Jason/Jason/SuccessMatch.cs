using Jason.Jason;
using System;

namespace Jason.Jason
{
    class SuccessMatch : IMatch
    {
        readonly StringView currentIndex;
        readonly StringView failedIndex;

        public SuccessMatch(StringView currentIndex, StringView failedIndex)
        {
            this.currentIndex = currentIndex;
            this.failedIndex = failedIndex;
        }

        public bool Success() => true;

        public StringView FailedIndex() => failedIndex;

        public StringView CurrentIndex() => currentIndex;
    }
}
