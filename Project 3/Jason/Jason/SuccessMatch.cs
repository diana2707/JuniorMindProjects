using Jason.Jason;
using System;

namespace Jason.Jason
{
    class SuccessMatch : IMatch
    {
        readonly string text;

        public SuccessMatch(string text)
        {
            this.text = text;
        }

        public bool Success()
        {
            return true;
        }

        public string RemainingText()
        {
            return text;
        }
    }
}
