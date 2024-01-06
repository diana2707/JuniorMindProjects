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

            if (text[0] != pattern)
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


/*internal class Match : IMatch
{
    readonly string text;
    readonly char start;
    readonly char end;

    public Match(string text, char start, char end)
    {
        this.text = text;
        this.start = start;
        this.end = end;
    }

    public bool Success()
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }
        else if (text[0] < start || text[0] > end)
        {
            return false;
        }

        return true;
    }

    public string RemainingText()
    {
        if (!Success())
        {
            return text;
        }

        return text.Substring(1);
    }*/

