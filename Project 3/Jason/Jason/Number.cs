using System;

namespace Jason.Jason
{
    internal class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            var integer = new Sequence(
                new Optional(new Character('-')),
                new Choice(
                    new Character('0'),
                    new Sequence(
                        new Range('1', '9'),
                        new Many(
                            new Range('0', '9')))));

            var fractional = new Sequence(
                new Character('.'),
                new OneOrMore(new Range('0', '9')));

            var exponent = new Sequence(
                new Any("eE"),
                new Optional(new Any("+-")),
                new OneOrMore(new Range('0', '9')));

            pattern = new Sequence(integer, new Optional(fractional), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
