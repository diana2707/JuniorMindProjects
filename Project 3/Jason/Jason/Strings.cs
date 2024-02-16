using Jason.Jason;
using Jason;
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
                        new Range('#', '['),
                        new Range(']', '\uFFFF'),
                        new Sequence(
                            new Character('\\'),
                            new Any("\\\"/bfnrt")),
                        new Sequence(
                            new Character('\\'),
                            new Character('u'),
                            new Choice(
                                new Range('0', '9'),
                                new Range('a', 'f'),
                                new Range('A', 'F')),
                            new Choice(
                                new Range('0', '9'),
                                new Range('a', 'f'),
                                new Range('A', 'F')),
                            new Choice(
                                new Range('0', '9'),
                                new Range('a', 'f'),
                                new Range('A', 'F')),
                            new Choice(
                                new Range('0', '9'),
                                new Range('a', 'f'),
                                new Range('A', 'F'))))),
                new Character('\"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}


//new Sequence(
//    new Character('\\'),
//    new Character('u'),
//    new Choice(
//        new Range('0', '9'),
//        new Range('a', 'f'),
//        new Range('A', 'F'))))),

//DE CE NU FUNCTIONEAZA LA FEL CU VALORILE HEX????
//new Choice(
//                        new Range('\u2000', '\u2100'),
//                        new Range('\u2300', '\u5B00'),
//                        new Range('\u5D00', '\uFFFF'))),