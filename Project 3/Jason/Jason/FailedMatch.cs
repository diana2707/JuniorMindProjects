using Jason.Jason;
using System;

namespace Jason.Jason
{
    class FailedMatch : IMatch
    {
        readonly string text;

        public FailedMatch(string text)
        {
            this.text = text;
        }

        public bool Success() => false;

        public string RemainingText() => text;
    }
}