using System;

namespace Jason.Jason
{
    internal class Text : IPattern
    {
        readonly string prefix;

        public Text(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(StringView stringView)
        {
            return !stringView.IsEmpty() && stringView.StartsWith(prefix)
                ? new SuccessMatch(stringView.Advance(prefix.Length))
                : new FailedMatch(stringView);
        }
    }
}
