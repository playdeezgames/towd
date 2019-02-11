namespace Engine
{
    public class DialogChoiceCondition
    {
        public DialogConditionType ConditionType { get; set; }
        public string FlagName { get; set; }
        public string CounterName { get; set; }
        public int Value { get; set; }
    }
}