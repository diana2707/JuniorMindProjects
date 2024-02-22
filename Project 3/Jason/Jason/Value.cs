using System;

namespace Jason.Jason
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var ws = new Any(" \\n\\r\\t");

            var value = new Choice(
                new Strings(),
                new Number(),
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var arr = new Sequence(
               new Character('['),
               new Choice(
                   ws,
                   new Sequence(
                       ws,
                       value,
                       ws),
                   new Many(
                       new Sequence(
                           new Character(','),
                           new Sequence(
                               ws,
                               value,
                               ws)))),
               new Character(']'));

            value.Add(arr);

            pattern = new Sequence(
                ws,
                value,
                ws);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}

// obj,
// arr,




// var obj = new Sequence(
//    new Character('{'),
//    new Choice(
//        ws,
//        new Sequence(
//           ws,
//           new Strings(),
//           ws,
//           new Character(':'),
//           new Value(),
//           new Many(
//               new Sequence(
//                   new Character(','),
//                   ws,
//                   new Strings(),
//                   ws,
//                   new Character(':'),
//                   new Value())))),
//    new Character('}'));