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
            if (string.IsNullOrEmpty(text))
            {
                return new FailedMatch(text);
            }

            if (text.StartsWith(prefix))
            {
                return new SuccessMatch(text[prefix.Length..]);
            }

            return new FailedMatch(text);
        }
    }
}
