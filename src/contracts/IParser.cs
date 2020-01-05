using System.Collections.Generic;
using dotnet_utils.src.enums;

namespace dotnet_utils.src.contracts
{
    public interface IParser
    {
        string Parse(skeletontypes skeletonType, IDictionary<string,string> parameters);
    }
}