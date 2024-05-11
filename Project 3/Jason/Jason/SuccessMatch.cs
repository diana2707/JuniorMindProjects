using Jason.Jason;
using System;

namespace Jason.Jason
{
    class SuccessMatch : IMatch
    {
        readonly StringView stringView;

        public SuccessMatch(StringView stringView)
        {
            this.stringView = stringView;
        }

        public bool Success() => true;

        public StringView StringView() => stringView;
    }
}
