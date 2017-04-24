using System.Collections.Generic;

namespace Proffiling
{
    public interface IDataProvider
    {
        decimal[] GetData(int count);
    }
}