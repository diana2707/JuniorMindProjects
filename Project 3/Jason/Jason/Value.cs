using System;

namespace Jason.Jason
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            // aici construiește patternul pentru
            // un JSON value
            // ai grijă să ți seama de `spații albe`
            pattern = ...;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}