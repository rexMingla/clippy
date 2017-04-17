using RexMingla.DataModel;

namespace RexMingla.Action.action
{
    public interface IAction
    {
        ActionDetail PerformAction(ClipboardContent content);
    }
}
