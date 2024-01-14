using System;

namespace Jason.Jason
{
    internal class Text : IPattern
    {
        readonly string text;

        public Text(string text)
        {
            this.text = text;
        }

        public IMatch Match(string prefix)
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
