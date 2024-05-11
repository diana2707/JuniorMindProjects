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

            var escape = new Choice(
                                new Any("\\\"/bfnrt"),
                                new Sequence(
                                    new Character('u'),
                                    hex,
                                    hex,
                                    hex,
                                    hex));

            var character = new Choice(
                        new Range('\x20', '\x21'),
                        new Range('\x23', '\x5B'),
                        new Range('\x5D', '\uFFFF'),
                        new Sequence(
                            new Character('\\'),
                            escape));

            pattern = new Sequence(
                new Character('\"'),
                new Many(character),
                new Character('\"'));
        }

        public IMatch Match(StringView stringView)
        {
            return pattern.Match(stringView);
        }
    }
}