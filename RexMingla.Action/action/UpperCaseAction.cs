namespace RexMingla.Action.action
{
    public sealed class UpperCaseAction : AbstractTextAction
    {
        protected override string Label { get { return "UPPERCASE"; } }

        protected override bool PerformTextAction(string oldValue, out string newValue)
        {
            newValue = oldValue.ToUpper();
            return true;
        }
    }
}
