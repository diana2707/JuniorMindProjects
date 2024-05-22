using System;

namespace Jason.Jason
{
    public interface IMatch
    {
        bool Success();

        StringView FailedIndex();

        StringView CurrentIndex();
    }
}
