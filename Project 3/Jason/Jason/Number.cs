using Jason.Jason;
using System;

namespace Jason.Jason
{
    internal class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            var digits = new OneOrMore(new Range('0', '9'));

            var integer = new Sequence(
                new Optional(new Character('-')),
                new Choice(
                    new Character('0'),
                    digits));

            var fraction = new Sequence(
                new Character('.'),
                new OneOrMore(new Range('0', '9')));

            var exponent = new Sequence(
                new Any("eE"),
                new Optional(new Any("+-")),
                new OneOrMore(new Range('0', '9')));

            pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}