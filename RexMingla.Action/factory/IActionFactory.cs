using RexMingla.DataModel;
using System.Collections.Generic;

namespace RexMingla.Action.factory
{
    public interface IActionFactory
    {
        List<ActionDetail> CreateActionDetails(ClipboardContent content);
    }
}
