using System;

namespace Jason.Jason
{
    internal class Many : IPattern
    {
        readonly IPattern pattern;

        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            string remainingText = text;
            for (int i = 0; i < text.Length; i++)
            {
                if (pattern.Match(remainingText).Success())
                {
                    remainingText = pattern.Match(remainingText).RemainingText();
                }
            }

            return new SuccessMatch(remainingText);
        }
    }
}
