using System;

namespace Jason.Jason
{
    public interface IMatch
    {
        bool Success();

        string RemainingText();
    }
}
