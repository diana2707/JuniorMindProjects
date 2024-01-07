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
            bool success = true;

            if (string.IsNullOrEmpty(text))
            {
                success = false;
            }
            else if (text[0] != pattern)
            {
                success = false;
            }

            if (!success)
            {
                return new FailedMatch(text);
            }

            return new SuccessMatch(text.Substring(1));
        }
    }
}