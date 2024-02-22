using System;

namespace Jason.Jason
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var ws = new Any(" \\n\\r\\t");

            var choice = new Choice(
                new Strings(),
                new Number(),
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var value = new Sequence(ws, choice, ws);

            var arr = new Sequence(
               new Character('['),
               new Choice(
                   ws,
                   value,
                   new Many(
                       new Sequence(
                           new Character(','),
                           value))),
               new Character(']'));

            choice.Add(arr);

            pattern = value;
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