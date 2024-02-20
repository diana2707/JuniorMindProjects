using System;

namespace Jason.Jason
{
    internal class Choice : IPattern
    {
        private IPattern[] patterns;

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

        public void Add(IPattern pattern)
        {
            Array.Resize(ref patterns, patterns.Length + 1);
            patterns[patterns.Length - 1] = pattern;
        }
    }
}
