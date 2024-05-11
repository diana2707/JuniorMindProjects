using System;

namespace Jason.Jason
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var ws = new Many(new Any(" \n\r\t"));

            var value = new Choice(
                new Strings(),
                new Number(),
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var element = new Sequence(ws, value, ws);

            var elements = new List(element, new Character(','));

            var member = new Sequence(ws, new Strings(), ws, new Character(':'), element);

            var members = new List(member, new Character(','));

            var arr = new Sequence(
               new Character('['),
               ws,
               elements,
               new Character(']'));

            var obj = new Sequence(
               new Character('{'),
               ws,
               members,
               new Character('}'));

            value.Add(arr);
            value.Add(obj);

            pattern = element;
        }

        public IMatch Match(StringView stringView)
        {
            return pattern.Match(stringView);
        }
    }
}