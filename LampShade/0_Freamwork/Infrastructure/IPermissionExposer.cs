

using System.Collections.Generic;

namespace _0_Freamwork.Infrastructure
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
