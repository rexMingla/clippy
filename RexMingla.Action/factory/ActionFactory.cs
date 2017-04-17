using System.Linq;
using System.Collections.Generic;
using RexMingla.DataModel;
using RexMingla.Action.action;

namespace RexMingla.Action.factory
{
    public sealed class ActionFactory : IActionFactory 
    {
        private readonly IList<IAction> _potentialActions;

        public ActionFactory(IList<IAction> potentialActions)
        {
            _potentialActions = potentialActions;
        }

        public List<ActionDetail> CreateActionDetails(ClipboardContent content)
        {
            return _potentialActions.Select(a => a.PerformAction(content)).Where(a => a?.NewClipboardContent != null).ToList();
        }
    }
}
