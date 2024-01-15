using System;

namespace Jason.Jason
{
    internal class Text : IPattern
    {
        readonly string prefix;

        public Text(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            string remainingText = text;

            for (int i = 0; i < prefix.Length; i++)
            {
                var pattern = new Character(prefix[i]);
                if (pattern.Match(remainingText).Success())
                {
                    remainingText = pattern.Match(remainingText).RemainingText();
                }
                else
                {
                    return new FailedMatch(text);
                }
            }

            return new SuccessMatch(remainingText);
        }
    }
}
