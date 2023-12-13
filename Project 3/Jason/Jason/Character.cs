using Jason.Jason;
using System;

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

            return new Match(text, success);
        }
    }
}

}