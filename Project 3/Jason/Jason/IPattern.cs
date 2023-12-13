using Jason.Jason;
using System;

namespace Jason
{
    internal interface IPattern
    {
        IMatch Match(string text);
    }
}
