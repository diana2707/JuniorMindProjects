using Jason.Jason;

namespace Jason
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            bool success = !string.IsNullOrEmpty(text) && text[0] == pattern;

            if (!success)
            {
                return new FailedMatch(text);
            }

            return new SuccessMatch(text.Substring(1));
        }
    }
}