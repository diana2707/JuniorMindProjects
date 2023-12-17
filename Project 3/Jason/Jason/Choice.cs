using System;

namespace Jason.Jason
{
    internal class Choice : IPattern
    {
        readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IPattern invalidPattern = patterns[0];
            foreach (var pattern in patterns)
            {
                if (pattern.Match(text).Success())
                {
                    return pattern.Match(text);
                }
            }

            return invalidPattern.Match(text);
        }
    }
}
