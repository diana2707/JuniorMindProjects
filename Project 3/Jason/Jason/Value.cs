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
                   new Sequence(
                       value,
                       new Many(
                           new Sequence(
                               new Character(','),
                               value))),
                   ws),
               new Character(']'));

            var keyValue = new Sequence(
                ws,
                new Strings(),
                ws,
                new Character(':'),
                value);

            var obj = new Sequence(
               new Character('{'),
               new Choice(
                   new Sequence(
                      keyValue,
                      new Many(
                          new Sequence(
                              new Character(','),
                              keyValue))),
                   ws),
               new Character('}'));

            choice.Add(arr);
            choice.Add(obj);

            pattern = value;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}