using System;

namespace Jason.Jason
{
    public interface IMatch
    {
        bool Success();

        int FailedIndex();

        StringView StringView();
    }
}
