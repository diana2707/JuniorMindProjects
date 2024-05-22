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

        public IMatch Match(StringView currentIndex, StringView failedIndex)
        {
            return !currentIndex.IsEmpty() && accepted.Contains(currentIndex.Peek())
                ? new SuccessMatch(currentIndex.Advance(), failedIndex)
                : new FailedMatch(currentIndex, currentIndex.IsIndexLargerThan(failedIndex) ? currentIndex : failedIndex);
        }
    }
}
