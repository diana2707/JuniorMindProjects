using Jason.Jason;
using System;

namespace Jason.Jason
{
    class FailedMatch : IMatch
    {
        readonly StringView stringView;

        public FailedMatch(StringView stringView)
        {
            this.stringView = stringView;
        }

        public bool Success() => false;

        public StringView StringView() => stringView;
    }
}