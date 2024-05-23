using System;

namespace Jason.Jason
{
    public class StringView
    {
        readonly string text;
        readonly int index;

        public StringView(string text, int index)
        {
            this.text = text;
            this.index = index;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(text) || index > text.Length - 1;
        }

        public char Peek()
        {
            return !IsEmpty() ? text[index] : '\0';
        }

        public StringView Advance(int count = 1)
        {
            return new StringView(text, index + count);
        }

        public bool StartsWith(string prefix)
        {
            return string.Compare(text, index, prefix, 0, prefix.Length) == 0;
        }

        public (int row, int column) GetLocation()
        {
            string[] strings = text[.. index].Split(Environment.NewLine);

            return (strings.Length, strings[^1].Length + 1);
        }

        public bool IsIndexLargerThan(StringView stringView)
        {
            return index > stringView.index;
        }
    }
}
