using System;

namespace Jason.Jason
{
    internal class Strings : IPattern
    {
        private readonly IPattern pattern;

        public Strings()
        {
            var hex = new Choice(
                        new Range('0', '9'),
                        new Range('a', 'f'),
                        new Range('A', 'F'));

            pattern = new Sequence(
                new Character('\"'),
                new Many(
                    new Choice(
                        new Range('\x20', '\x21'),
                        new Range('\x23', '\x5B'),
                        new Range('\x5D', '\xFFFF'),
                        new Sequence(
                            new Character('\\'),
                            new Any("\\\"/bfnrt")),
                        new Sequence(
                            new Character('\\'),
                            new Character('u'),
                            hex,
                            hex,
                            hex,
                            hex))),
                new Character('\"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}