using System;

namespace Jason.Jason
{
    internal class Strings : IPattern
    {
        private readonly IPattern pattern;

        public Strings()
        {
            pattern = new Sequence(
                new Character('\"'),
                new Many(
                    new Choice(
                        new Range(' ', '!'),
                        new Range('#', 'ÿ'))),
                //new OneOrMore(new Range(' ', 'ÿ')),
                new Character('\"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
