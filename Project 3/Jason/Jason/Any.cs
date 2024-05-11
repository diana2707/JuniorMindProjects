using System;
using System.Text;

namespace Jason.Jason
{
    internal class Any : IPattern
    {
        readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(StringView stringView)
        {
            return !stringView.IsEmpty() && accepted.Contains(stringView.Peek())
                ? new SuccessMatch(stringView.Advance())
                : new FailedMatch(stringView);
        }
    }
}
