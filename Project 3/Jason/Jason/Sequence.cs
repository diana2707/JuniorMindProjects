using System;

namespace Jason.Jason
{
    class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            bool success = true;

            for (int i = 0; i < patterns.Length; i++)
            {
                if (!patterns[i].Match(text).Success())
                {
                    success = false;
                }
                else if (i < patterns.Length - 1)
                {
                    text = patterns[i].Match(text).RemainingText();
                }
            }

            return new Match(text, success);
        }
    }
}
