namespace RexMingla.Action.action
{
    public sealed class LowerCaseAction : AbstractTextAction
    {
        protected override string Label { get { return "lowercase"; } }

        protected override bool PerformTextAction(string oldValue, out string newValue)
        {
            newValue = oldValue.ToLower();
            return true;
        }
    }
}
